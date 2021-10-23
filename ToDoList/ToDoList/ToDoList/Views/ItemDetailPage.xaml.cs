using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using ToDoList.Models;
using ToDoList.ViewModels;
using System.Diagnostics;

namespace ToDoList.Views
{


    [DesignTimeVisible(false)]


    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailViewModel viewModel;
        private bool? currentOrientationLandscape;

        public ItemDetailPage(ItemDetailViewModel viewModel)
        {

            InitializeComponent();
            BindingContext = this.viewModel = viewModel;
            fin.IsVisible = !viewModel.Item.IsFinished;
            unfin.IsVisible = viewModel.Item.IsFinished;
            //type.Text = "Item type: " + viewModel.itemtype.Type;
            image.Source = viewModel.Itemtype.Image;
        }

        

        private void FinishBtn_Clicked(object sender, EventArgs e)
        {
            viewModel.Item.IsFinished = true;
            Item item = viewModel.Item;

            //MessagingCenter.Send(this, "AddFinishedItem", item);
            viewModel.AddFinishedItem();
            fin.IsVisible = !viewModel.Item.IsFinished;
            unfin.IsVisible = viewModel.Item.IsFinished;
            

        }
        private void UnfinishBtn_Clicked(object sender, EventArgs e)
        {
            viewModel.Item.IsFinished = false;

            Item item = viewModel.Item;
            viewModel.DeleteFinishedItem();
            fin.IsVisible = !viewModel.Item.IsFinished;
            unfin.IsVisible = viewModel.Item.IsFinished;


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