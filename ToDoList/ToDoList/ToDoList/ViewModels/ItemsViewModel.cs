using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Linq;
using Xamarin.Forms;
using ToDoList.Models;
using ToDoList.Views;
using ToDoList.Services;
using System.Collections.Generic;

namespace ToDoList.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<Item> Items { get; set; }
        public Command LoadTodayItemsCommand { get; set; }
        public DateTime Today = DateTime.Now;
        
        public ItemsViewModel()
        {
            Title = string.Format("{0:d} {1}", Today, Today.DayOfWeek);
            Items = new ObservableCollection<Item>();
            LoadTodayItemsCommand = new Command(() => ExecuteLoadTodayItemsCommand());
            Date = DateTime.Now;
            
            MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddRepeatItem", (obj, item) =>
            {
                RepeatItem newRepeatItem = new RepeatItem
                {
                    CreateDate = item.CreateDate,
                    Text = item.Text,
                    Description = item.Description,
                    RepeatDay = item.RepeatDays,
                    Itemtype = item.Itemtype
                };
                DB.conn.Insert(newRepeatItem);
                DB.ShowAllRepeatitems();
            });

            MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddNonRepeatItem", (obj, item) =>
            {
                NonRepeatItem newNonRepeatItem = new NonRepeatItem
                {
                    CreateDate = item.CreateDate,
                    Text = item.Text,
                    Description = item.Description,
                    Itemtype = item.Itemtype
                };
                DB.conn.Insert(newNonRepeatItem);
                DB.ShowAllNonRepeatitems();
            });

            
        }

        private void ExecuteLoadTodayItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                if (DB.conn.Table<RepeatItem>() != null)
                {
                    var todayResult1 = from repeatItems in DB.conn.Table<RepeatItem>().ToList()
                                       where repeatItems.RepeatDay[(int)Today.DayOfWeek] == 1 &&
                                       repeatItems.CreateDate.ToShortDateString().CompareTo(Date.ToShortDateString()) <= 0
                                       select new Item
                                       {
                                           Text = repeatItems.Text,
                                           Description = repeatItems.Description,
                                           IsRepeat = true,
                                           IsFinished = false,
                                           ItemId = repeatItems.Id,
                                           RepeatDays = repeatItems.RepeatDay,
                                           CreateDate = repeatItems.CreateDate,
                                           Itemtype = repeatItems.Itemtype
                                       };
                    foreach (Item i in todayResult1)
                    {
                        var isFinished = from finisheditem in DB.conn.Table<FinishedItems>().ToList()
                                         where finisheditem.IsRepeatItem && finisheditem.ItemId == i.ItemId &&
                                         finisheditem.CreateDate.ToShortDateString() == Today.ToShortDateString()
                                         select finisheditem;
                        if (isFinished.Count() != 0)
                        {
                            i.IsFinished = true;
                        }
                        else if (isFinished.Count() > 1)
                        {
                            throw new Exception("Error in itemPage");
                            
                        }

                        Items.Add(i);
                    }
                }
                if (DB.conn.Table<NonRepeatItem>() != null)
                {
                    var todayResult2 = from nonRepeatItems in DB.conn.Table<NonRepeatItem>().ToList()
                                       where nonRepeatItems.CreateDate.ToShortDateString() == Today.ToShortDateString()
                                       select new Item
                                       {
                                           Text = nonRepeatItems.Text,
                                           Description = nonRepeatItems.Description,
                                           IsRepeat = false,
                                           IsFinished = false,
                                           ItemId = nonRepeatItems.Id,
                                           CreateDate = nonRepeatItems.CreateDate,
                                           Itemtype = nonRepeatItems.Itemtype
                                       };

                    foreach (Item i in todayResult2)
                    {
                        var isFinished = from finisheditem in DB.conn.Table<FinishedItems>().ToList()
                                         where !finisheditem.IsRepeatItem && finisheditem.ItemId == i.ItemId &&
                                         finisheditem.CreateDate.ToShortDateString() == Today.ToShortDateString()
                                         select finisheditem;
                        if (isFinished.Count() == 1)
                        {
                            i.IsFinished = true;
                        }
                        else if (isFinished.Count() > 1)
                        {

                            Debug.WriteLine("Error in 2");
                            throw new Exception("Error in itemPage");

                        }

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