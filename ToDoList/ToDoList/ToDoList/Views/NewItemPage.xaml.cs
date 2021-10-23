using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using ToDoList.Models;
using System.Threading;

namespace ToDoList.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewItemPage : ContentPage
    {

        private bool? currentOrientationLandscape;
        public Item Item { get; set; }
        public NewItemPage()
        {
            InitializeComponent();

            Item = new Item
            {
                Text = "Item name",
                Description = "This is an item description.",
                IsRepeat = false,               
                RepeatDays = new byte[7],
                CreateDate = DateTime.Now,
                Itemtype = 4
            };



            BindingContext = this;
            
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            if (Item.IsRepeat)
            {
                MessagingCenter.Send(this, "AddRepeatItem", Item);
            }
            else
            {
                MessagingCenter.Send(this, "AddNonRepeatItem", Item);
            }
            
           
            await Navigation.PopModalAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
       
        private void isRepeat_Toggled(object sender, ToggledEventArgs e)
        {
            Item.IsRepeat = isRepeat.IsToggled;
            if (isRepeat.IsToggled)
            {
                datePicker.IsVisible = false;
                everydayLayout.IsVisible = true;
                weekday.IsVisible = everyday.IsChecked ? false : true;
                
            }
            else
            {
                datePicker.IsVisible = true;
                everyday.IsChecked = false;
                everydayLayout.IsVisible = false;
                weekday.IsVisible = false;
                Item.RepeatDays = new byte[7];
            }

           
            
            
        }

        private void checkbox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            byte check = Convert.ToByte(cb.IsChecked);
            if (cb == sun) Item.RepeatDays[0] = check;
            if (cb == mon) Item.RepeatDays[1] = check;
            if (cb == tue) Item.RepeatDays[2] = check;
            if (cb == wed) Item.RepeatDays[3] = check;
            if (cb == thu) Item.RepeatDays[4] = check;
            if (cb == fri) Item.RepeatDays[5] = check;
            if (cb == sat) Item.RepeatDays[6] = check;
        }


        private void everyday_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            weekday.IsVisible = isRepeat.IsToggled&&!everyday.IsChecked ? true : false;
            if (everyday.IsChecked)
            {
                for(int i = 0; i < 7; i++)
                {
                    Item.RepeatDays[i] = 1;
                    
                }
            }
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            bool isNowLandscape = width > height;
            if (currentOrientationLandscape.HasValue && isNowLandscape == currentOrientationLandscape)
                return;

            currentOrientationLandscape = isNowLandscape;

            RelativedGrid rg = new RelativedGrid(grid, layout, isNowLandscape);

        }
    }
}