using System;

using ToDoList.Models;
using ToDoList.Services;
using ToDoList.Views;
using Xamarin.Forms;
using System.Linq;
using System.Diagnostics;

namespace ToDoList.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Item Item { get; set; }
        public DateTime Today { get; set; }

        public Itemtype Itemtype { get; set; }
        public ItemDetailViewModel(Item newItem = null)
        {
            Title = newItem?.Text;
            Item = newItem;
            Today = DateTime.Now;
            Itemtype = new Itemtype(Item.Itemtype);
        }

        public void AddFinishedItem()
        {
            FinishedItems newFinishedItem = new FinishedItems
            {
                CreateDate = Today,
                Text = Item.Text,
                Description = Item.Description,
                ItemId = Item.ItemId,
                IsRepeatItem = Item.IsRepeat,
                Itemtype = Item.Itemtype
            };
            

           
            DB.ShowAllFinisheditems();
            DB.conn.Insert(newFinishedItem);
            DB.ShowAllFinisheditems();
            
        }

        public void DeleteFinishedItem()
        {
            if (Item.IsRepeat)
            {
                var deleteFinishedItem = from all in DB.conn.Table<FinishedItems>().ToList()
                                         where all.IsRepeatItem && all.ItemId == Item.ItemId &&
                                         all.CreateDate.ToShortDateString() == Today.ToShortDateString()
                                         select all;
                if (deleteFinishedItem.Count() == 1)
                {
                    foreach (var i in deleteFinishedItem)
                    {
                        DB.ShowAllFinisheditems();
                        DB.conn.Delete(i);
                        DB.ShowAllFinisheditems();
                    }
                }
                else
                {
                    throw new Exception("error in delete finished repeatitem");
                }
            }
            else
            {
                var deleteFinishedItem = from all in DB.conn.Table<FinishedItems>().ToList()
                                         where !all.IsRepeatItem && all.ItemId == Item.ItemId &&
                                         all.CreateDate.ToShortDateString() == Today.ToShortDateString()
                                         select all;
                if (deleteFinishedItem.Count() == 1)
                {
                    foreach (var i in deleteFinishedItem)
                    {
                        DB.ShowAllFinisheditems();
                        DB.conn.Delete(i);
                        DB.ShowAllFinisheditems();
                    }
                }
                else
                {
                    throw new Exception("error in delete finished nonrepeatitem");
                }
            }
        }


    }
}
