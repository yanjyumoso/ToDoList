using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace ToDoList.Models
{
    [Table("NonRepeatItem")]
    public class NonRepeatItem
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
        public int Itemtype { get; set; }
        public override string ToString()
        {
            return string.Format("Nonrepeated items: Id: {0}, CreateDate: {1}, Text: {2}", Id, CreateDate.ToShortDateString(), Text);

        }
    }
}
