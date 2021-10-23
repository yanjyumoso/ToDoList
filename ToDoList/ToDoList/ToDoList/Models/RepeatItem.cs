using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace ToDoList.Models
{
    [Table("RepeatItem")]
    public class RepeatItem
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
        public byte[] RepeatDay { get; set; }
        public int Itemtype { get; set; }
        public override string ToString()
        {
            return string.Format("Repeated Items:Id: {0}, CreateDate: {1}, Text: {2}", Id, CreateDate.ToShortDateString(), Text);
        }
    }
}
