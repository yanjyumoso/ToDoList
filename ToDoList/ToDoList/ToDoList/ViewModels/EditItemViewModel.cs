using System;
using System.Collections.Generic;
using System.Text;
using ToDoList.Models;
using ToDoList.Services;
using ToDoList.Views;
using Xamarin.Forms;
using System.Linq;
using System.Diagnostics;

namespace ToDoList.ViewModels
{
    public class EditItemViewModel : BaseViewModel
    {

        public Item Item { get; set; }

        public EditItemViewModel(Item item = null)
        {
            Title = item?.Text;
            Item = item;

        }

        public void UpdateItem()
        {
            if (Item.IsRepeat)
            {
                RepeatItem newRepeatItem = new RepeatItem
                {
                    CreateDate = Item.CreateDate,
                    Text = Item.Text,
                    Description = Item.Description,
                    RepeatDay = Item.RepeatDays,
                    Id = Item.ItemId,
                    Itemtype = Item.Itemtype
                };
                DB.conn.Update(newRepeatItem);
                DB.ShowAllRepeatitems();

            }
            else
            {
                NonRepeatItem newNonRepeatItem = new NonRepeatItem
                {
                    CreateDate = Item.CreateDate,
                    Text = Item.Text,
                    Description = Item.Description,
                    Id = Item.ItemId,
                    Itemtype = Item.Itemtype
                };
                DB.conn.Update(newNonRepeatItem);
                DB.ShowAllNonRepeatitems();
            }
        }

        public void DeleteItem()
        {
            try
            {
                if (Item.IsRepeat)
                {
                    var items = from repeats in DB.conn.Table<RepeatItem>().ToList()
                                where repeats.Id == Item.ItemId
                                select repeats;
                    if (items.Count() != 1)
                    {

                        throw new Exception("delete item error1");
                    }
                    else
                    {
                        foreach (var i in items)
                        {
                            DB.conn.Delete(i);
                            DB.ShowAllRepeatitems();
                        }
                    }

                }
                else
                {
                    var items = from nonrepeats in DB.conn.Table<NonRepeatItem>().ToList()
                                where nonrepeats.Id == Item.ItemId
                                select nonrepeats;
                    if (items.Count() != 1)
                    {

                        throw new Exception("delete item error2");
                    }
                    else
                    {
                        foreach (var i in items)
                        {
                            DB.conn.Delete(i);
                            DB.ShowAllNonRepeatitems();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }


        }
    }
}
