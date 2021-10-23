using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using SQLite;
namespace ToDoList.Models
{
    [Table("FinishedItems")]
    public class FinishedItems
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }       
        public string Text { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public int ItemId { get; set; }        
        public bool IsRepeatItem { get; set; }
        public int Itemtype { get; set; }
        public override string ToString()
        {
            return string.Format("Finished Items: ItemId: {0}, CreateDate: {1}, Isrepeat: {2}", ItemId, CreateDate.ToShortDateString(), IsRepeatItem );
        }
    }
}
