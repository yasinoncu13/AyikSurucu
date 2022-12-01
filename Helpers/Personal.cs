using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AyikSurucu.Helpers
{
    class Personal
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public string tckn { get; set; }
        public string ad { get; set; }
        public string soyad { get; set; }
        public string mail { get; set; }
        public string pass { get; set; }
    }
}