using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using AyikSurucu.Helpers;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AyikSurucu.Models
{
    class Database
    {
        string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

        public bool createDatabase()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder,"Personal.db")))
                {
                    connection.CreateTable<Personal>();
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return true;
            }
        }
        public bool InsertIntoTablePerson(Personal personal)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Personal.db")))
                {
                    connection.Insert(personal);
                    return false;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return false;
            }
        }
        public List<Personal> selectTablePerson()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Personal.db")))
                {
                    return connection.Table<Personal>().ToList();
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return null;
            }
        }
        public bool updateTablePerson(Personal personal)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder,"Personal.db")))
                {
                    connection.Query<Personal>("UPDATE personal set ad=?,soyad=?,mail=?, tckn=? where id=?", personal.ad, personal.soyad, personal.mail, personal.tckn,personal.pass,personal.id);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return false;
            }
        }
        public bool deleteTablePerson(Personal personal)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Personal.db")))
                {
                    connection.Delete(personal);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return false;
            }
        }
        public bool selectQueryTablePerson(int id)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Personal.db")))
                {
                    connection.Query<Personal>("SELECT * FROM Personal WHERE id=?",id);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return false;
            }
        }
        
    }
}