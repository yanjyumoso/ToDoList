using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using ToDoList.Models;
using Xamarin.Essentials;
using System.Linq;
using System.Diagnostics;

namespace ToDoList.Services
{
    public class DB
    {
        private static string DBName = "log.db";
        public static SQLiteConnection conn;


        public DB()
        {

        }
        public static void OpenConnection()
        {
            string libFolder = FileSystem.AppDataDirectory;
            string fname = System.IO.Path.Combine(libFolder, DBName);
            conn = new SQLiteConnection(fname);
            conn.CreateTable<RepeatItem>();
            conn.CreateTable<NonRepeatItem>();
            conn.CreateTable<FinishedItems>();
            ShowAllFinisheditems();
            ShowAllNonRepeatitems();
            ShowAllRepeatitems();
           
        }

        public static void ShowAllRepeatitems()
        {
            var allItems = from total in DB.conn.Table<RepeatItem>().ToList()
                           select total;
            foreach(RepeatItem i in allItems)
            {
                Debug.WriteLine(i);
         
            }
            Debug.WriteLine("===========================");
        }
        public static void ShowAllNonRepeatitems()
        {
            var allItems = from dailyTotal in DB.conn.Table<NonRepeatItem>().ToList()
                           select dailyTotal;
            foreach (NonRepeatItem i in allItems)
            {
                Debug.WriteLine(i);
            }
            Debug.WriteLine("===========================");
        }

        public static void ShowAllFinisheditems()
        {
            var allItems = from dailyTotal in DB.conn.Table<FinishedItems>().ToList()
                           select dailyTotal;
            foreach (FinishedItems i in allItems)
            {
                Debug.WriteLine(i);
            }
            Debug.WriteLine("===========================");
        }

        public static void GenerateMockData()
        {
           
        }
    }

    
}
