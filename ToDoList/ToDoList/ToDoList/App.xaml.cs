using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ToDoList.Services;
using ToDoList.Views;

namespace ToDoList
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            DB.OpenConnection();
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
