using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ToDoList.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HistoryPage : ContentPage
    {
        HistoryViewModel viewModel;
        public HistoryPage()
        {
            
            InitializeComponent();
            BindingContext = viewModel = new HistoryViewModel();
            viewModel.IsBusy = true;

        }


        protected override void OnAppearing()
        {
            viewModel.IsBusy = true;
            base.OnAppearing();          
        }

        private void DatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            viewModel.IsBusy = true;
        }
    }
}