using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ToDoList.Models
{
    public class Itemtype
    {

        public string[] AllTypes = {"Study", "Entertainment", "Shopping", "Food", "None" };

        private string[] Images = {"study.png","play.png","shopping.png","food.png","today.png" };
        public string Type { get; set; }
        public string Image { get; set; }
        public Itemtype(int index)
        {
            Type = AllTypes[index];
            Image = Images[index];
        }

    }
}
