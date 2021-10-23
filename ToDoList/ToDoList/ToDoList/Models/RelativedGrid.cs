using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ToDoList.Models
{
    class RelativedGrid
    {
        public RelativedGrid(Grid grid, View item, bool isNowLandscape)
        {
            if (isNowLandscape)
            {
                grid.ColumnDefinitions.Clear();
                grid.RowDefinitions.Clear();
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                grid.Children.Remove(item);
                grid.Children.Add(item, 1, 0);

            }
            else
            {
                grid.ColumnDefinitions.Clear();
                grid.RowDefinitions.Clear();
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                grid.Children.Remove(item);
                grid.Children.Add(item, 0, 1);

            }
        }
    }
}
