using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AyikSurucu.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AyikSurucu
{
    public class ViewHolder : Java.Lang.Object
    { 
        public TextView txtTc { get; set; }
        public TextView txtAd { get; set; }
        public TextView txtSoyad { get; set; }
        public TextView txtMail { get; set; }
        public TextView txtPass { get; set; }
    }
    internal class ListViewAdapter : BaseAdapter
    {
        private Activity activity;
        private List<Personal> lstpersonals;
        private MainActivity mainActivity;
        private List<Helpers.Personal> lstSource;

        public ListViewAdapter(Activity activity, List<Personal> lstPersonal, MainActivity mainActivity, List<Personal> lstSource)
        {
            this.activity = activity;
            this.lstpersonals = lstPersonal;
            this.mainActivity = mainActivity;
            this.lstSource = lstSource;
        }

        public override int Count
        {
            get
            {
                return lstpersonals.Count;
            }
        }
        public override Java.Lang.Object GetItem(int position)
        {
            return position;
        }

        public override long GetItemId(int position)
        {
            return lstpersonals[position].id;
        }

        Context context;

        public ListViewAdapter(Context context)
        {
            this.context = context;
        }

        public ListViewAdapter(Context context, List<Personal> lstSource) : this(context)
        {
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ?? activity.LayoutInflater.Inflate(Resource.Layout.uyeol,parent,false);
            TextView txtTc = view.FindViewById<EditText>(Resource.Id.textView1);
            TextView txtAd = view.FindViewById<TextView>(Resource.Id.textView1);
            TextView txtSoyad = view.FindViewById<TextView>(Resource.Id.textView1);
            TextView txtMail = view.FindViewById<TextView>(Resource.Id.textView1);
            TextView txtPass = view.FindViewById<TextView>(Resource.Id.textView1);

            return view;
        }

        //Fill in cound here, currently 0
        

    }

    internal class ListViewAdapterViewHolder : Java.Lang.Object
    {
        //Your adapter views to re-use
        //public TextView Title { get; set; }
    }
}