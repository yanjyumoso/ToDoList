using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using ToDoList.Models;
using ToDoList.Services;
using Xamarin.Forms;
using System.Linq;
using System.Diagnostics;

namespace ToDoList.ViewModels
{

    public class HistoryViewModel : BaseViewModel
    {
        public ObservableCollection<Item> Items { get; set; }
        public Command LoadDayItemsCommand { get; set; }

        public HistoryViewModel()
        {
            Title = "History";
            Items = new ObservableCollection<Item>();
            LoadDayItemsCommand = new Command(() => ExecuteLoadDayItemsCommand());
            Date = DateTime.Now;
        }

        private void ExecuteLoadDayItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();

                if (DB.conn.Table<RepeatItem>().ToList() != null)
                {

                    var todayResult1 = from repeatItems in DB.conn.Table<RepeatItem>().ToList()
                                       where repeatItems.RepeatDay[(int)Date.DayOfWeek] == 1 &&
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
                                         finisheditem.CreateDate.ToShortDateString() == Date.ToShortDateString()
                                         select finisheditem;
                        if (isFinished.Count() == 1)
                        {
                            i.IsFinished = true;
                        }
                        else if (isFinished.Count() > 1)
                        {
                            Debug.WriteLine("Error in 1");
                            throw new Exception("Error in HistoryViewModel (repeatitem): mlutiple results: " + i);
                        }

                        Items.Add(i);
                    }
                }
                if (DB.conn.Table<NonRepeatItem>() != null)
                {
                    var todayResult2 = from nonRepeatItems in DB.conn.Table<NonRepeatItem>().ToList()
                                       where nonRepeatItems.CreateDate.ToShortDateString() == Date.ToShortDateString()
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
                                         where !finisheditem.IsRepeatItem && finisheditem.ItemId == i.ItemId
                                               && finisheditem.CreateDate.ToShortDateString() == Date.ToShortDateString()
                                         select finisheditem;
                        if (isFinished.Count() == 1)
                        {
                            i.IsFinished = true;
                        }
                        else if (isFinished.Count() > 1)
                        {
                            Debug.WriteLine("Error in 2");
                            throw new Exception("Error in HistoryViewModel (nonrepeatitem): mlutiple results: " + i);
                        }

                        Items.Add(i);
                    }
                }
                if (DB.conn.Table<FinishedItems>().ToList() != null)
                {
                    var todayResult3 = from finishedItems in DB.conn.Table<FinishedItems>().ToList()
                                       where finishedItems.CreateDate.ToShortDateString() == Date.ToShortDateString()
                                       select new Item
                                       {
                                           Text = finishedItems.Text,
                                           Description = finishedItems.Description,
                                           IsRepeat = finishedItems.IsRepeatItem,
                                           IsFinished = true,
                                           ItemId = finishedItems.ItemId,
                                           CreateDate = finishedItems.CreateDate,
                                           Itemtype = finishedItems.Itemtype
                                       };
                    foreach (Item i in todayResult3)
                    {
                        if (i.IsRepeat)
                        {
                            DB.ShowAllFinisheditems();
                            var result = (from f in DB.conn.Table<RepeatItem>()
                                          where f.Id == i.ItemId
                                          select f).Any();
                            
                            if (!result) Items.Add(i);

                        }
                        else
                        {
                            var result = (from f in DB.conn.Table<NonRepeatItem>().ToList()
                                          where f.Id == i.ItemId
                                          select f).Any();
                            
                            if (!result) Items.Add(i);
                        }
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
