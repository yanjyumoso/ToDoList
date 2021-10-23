using System;
using Xamarin.Forms;

namespace ToDoList.Models
{
   
    public class Item
    {
        public string Text { get; set; }
        public string Description { get; set; }
        public bool IsFinished { get; set; }
        public bool IsRepeat { get; set; }
        public byte[] RepeatDays { get; set; }
        public int ItemId { get; set; }
        public DateTime CreateDate { get; set; }
        public int Itemtype { get; set; }
    }
}