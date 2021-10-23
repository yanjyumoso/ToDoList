using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using ToDoList.Models;
using ToDoList.Services;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.Linq;

namespace ToDoList.ViewModels
{
    public class AllItemsViewModel : BaseViewModel
    {
        public ObservableCollection<Item> Items { get; set; }
        public Command LoadItemsCommand { get; set; }
        public DateTime SelectDate { get; set; }
        public AllItemsViewModel()
        {
            Title = "All Items";
            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(() => ExecuteLoadItemsCommand());
            Date = SelectDate;
        }
        private void ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();

                if (DB.conn.Table<RepeatItem>() != null)
                {

                    var todayResult1 = from repeatItems in DB.conn.Table<RepeatItem>().ToList()                                      
                                       select new Item
                                       {
                                           Text = repeatItems.Text,
                                           Description = repeatItems.Description,
                                           IsRepeat = true,                                          
                                           ItemId = repeatItems.Id,
                                           RepeatDays = repeatItems.RepeatDay,
                                           CreateDate = repeatItems.CreateDate,
                                           Itemtype = repeatItems.Itemtype
                                       };
                    foreach (Item i in todayResult1)
                    {                     
                        Items.Add(i);
                    }
                }
                if (DB.conn.Table<NonRepeatItem>() != null)
                {
                    var todayResult2 = from nonRepeatItems in DB.conn.Table<NonRepeatItem>().ToList()
                                       select new Item
                                       {
                                           Text = nonRepeatItems.Text,
                                           Description = nonRepeatItems.Description,
                                           IsRepeat = false,                                         
                                           ItemId = nonRepeatItems.Id,
                                           CreateDate = nonRepeatItems.CreateDate,
                                           Itemtype = nonRepeatItems.Itemtype
                                       };

                    foreach (Item i in todayResult2)
                    {                      
                        Items.Add(i);
                    }
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }

        }

    }
}