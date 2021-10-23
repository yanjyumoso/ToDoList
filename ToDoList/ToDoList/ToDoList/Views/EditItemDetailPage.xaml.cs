using System;

using ToDoList.Models;
using ToDoList.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ToDoList.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditItemDetailPage : ContentPage
    {
        public EditItemViewModel viewModel;
        private bool? currentOrientationLandscape;

        public EditItemDetailPage(EditItemViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }
      
        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (viewModel.Item.IsRepeat)
            {
                everyday.IsEnabled = true;
                weekday.IsVisible = everyday.IsChecked ? false : true;

            }
            else
            {
                weekday.IsVisible = false;
                everydayLayout.IsVisible = false;
            }

            if (viewModel.Item.IsRepeat)
            {
                sun.IsChecked = viewModel.Item.RepeatDays[0] == 1 ? true : false;
                mon.IsChecked = viewModel.Item.RepeatDays[1] == 1 ? true : false;
                tue.IsChecked = viewModel.Item.RepeatDays[2] == 1 ? true : false;
                wed.IsChecked = viewModel.Item.RepeatDays[3] == 1 ? true : false;
                thu.IsChecked = viewModel.Item.RepeatDays[4] == 1 ? true : false;
                fri.IsChecked = viewModel.Item.RepeatDays[5] == 1 ? true : false;
                sat.IsChecked = viewModel.Item.RepeatDays[6] == 1 ? true : false;


                for (int i = 0; i < 7; i++)
                {

                    if (viewModel.Item.RepeatDays[i] == 0)
                    {

                        everyday.IsChecked = false;
                        break;
                    }
                    if (i == 7 && viewModel.Item.RepeatDays[i] == 1)
                    {
                        everyday.IsChecked = true;
                    }

                }
            }
        }



        private void everyday_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            weekday.IsVisible = viewModel.Item.IsRepeat && !everyday.IsChecked ? true : false;
            if (everyday.IsChecked)
            {
                for (int i = 0; i < 7; i++)
                {
                    viewModel.Item.RepeatDays[i] = 1;

                }
            }
        }

        private void checkbox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            byte check = Convert.ToByte(cb.IsChecked);
            if (cb == sun) viewModel.Item.RepeatDays[0] = check;
            if (cb == mon) viewModel.Item.RepeatDays[1] = check;
            if (cb == tue) viewModel.Item.RepeatDays[2] = check;
            if (cb == wed) viewModel.Item.RepeatDays[3] = check;
            if (cb == thu) viewModel.Item.RepeatDays[4] = check;
            if (cb == fri) viewModel.Item.RepeatDays[5] = check;
            if (cb == sat) viewModel.Item.RepeatDays[6] = check;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
           
            viewModel.UpdateItem();
            await Navigation.PopModalAsync();
        }

        async void Delete_Clicked(object sender, EventArgs e)
        {          
            viewModel.DeleteItem();
            await Navigation.PopModalAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
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