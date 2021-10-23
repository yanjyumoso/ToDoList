using System;
using System.ComponentModel;
using ToDoList.Models;
using ToDoList.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ToDoList.Views
{

    [DesignTimeVisible(false)]
    public partial class AllItemsPage : ContentPage
    {
        AllItemsViewModel viewModel;
        //EditItemDetailPage EditItemDetail;
        //EditItemViewModel vm;
        public AllItemsPage()
        {
           
            InitializeComponent();
            BindingContext = viewModel = new AllItemsViewModel();
        }


        protected override void OnAppearing()
        {
            viewModel.IsBusy = true;
            base.OnAppearing();
        }

        async void OnItemSelected(object sender, EventArgs args)
        {
            var layout = (BindableObject)sender;
            var Item = (Item)layout.BindingContext;           
            await Navigation.PushModalAsync(new NavigationPage(new EditItemDetailPage(new EditItemViewModel(Item))));

        }
    }
}