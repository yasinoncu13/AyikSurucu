using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Webkit;
using Android.Widget;
using AndroidX.AppCompat.App;
using System;
using System.Collections.Generic;
using Android.Gms.Maps;
using System.Net.Http;
using System.Threading.Tasks;
using Android.Gms.Maps.Model;
using Android.Graphics;
using Java.Util;
using Android.Support.V7.App;
using Android.Gms.Location;
using Google.Places;
using Android.Content;
using AppCompatActivity = Android.Support.V7.App.AppCompatActivity;
using AyikSurucu.Helpers;
using Android;
using Result = Android.App.Result;
using AyikSurucu.Models;
using Android.Util;
using Felipecsl.GifImageViewLibrary;
using System.IO;
using Timer=System.Timers.Timer;

namespace AyikSurucu
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    

    public class MainActivity : AppCompatActivity, IOnMapReadyCallback
    {
        readonly string[] permissionGroup = { Manifest.Permission.AccessFineLocation, Manifest.Permission.AccessCoarseLocation };

        TextView txtView,adminText;
        EditText user, pass, tctxt, adtxt, soyadtxt, mailtxt, passtxt,admuser,admpass,admAd,admSyd,admTc,admPss,admMail;
        Button login, logdrive,gecButon,uyeol,uyeolun, secist, secank, secizm,admbuton,addBtn,editBtn,removeBtn;
        WebView sifreUnut;
        ListView otoListe, sehirListe,lstData ;
        ImageView centerMarker;
        RelativeLayout placeLayout;
        ImageButton locationButton;
        List<Personal> lstSource = new List<Personal>();
        Database db;
        GoogleMap mapgoogleist,mapgoogleank,mapgoogleizm;
        RadioButton ayikSur;

        GifImageView gifImageView;
        ProgressBar progressBar;

        public override void OnBackPressed()
        {

        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            txtView = FindViewById<TextView>(Resource.Id.textView1);
            user = FindViewById<EditText>(Resource.Id.editText1);
            pass = FindViewById<EditText>(Resource.Id.editText2);
            login = FindViewById<Button>(Resource.Id.giris);
            logdrive = FindViewById<Button>(Resource.Id.girsurucu);
            adminText = FindViewById<TextView>(Resource.Id.txtAdmin);

            //gifImageView = FindViewById<GifImageView>(Resource.Id.giflogo);
            //progressBar = FindViewById<ProgressBar>(Resource.Id.progressBar1);

            //Stream input = Assets.Open("yenilogo1.gif");
            //byte[] bytes = ConvertFileToByteArray(input);
            //gifImageView.SetBytes(bytes);
            //gifImageView.StartAnimation();

            //Timer timer = new Timer();
            //timer.Interval = 3000;
            //timer.AutoReset = true;
            //timer.Elapsed += Timer_Elapsed;


            uyeol = FindViewById<Button>(Resource.Id.uyeol2);

            


            login.Click += Giris_Click;
            txtView.Click += TxtView_Click;
            logdrive.Click += GirSurucu_Click;
            uyeol.Click += Uyeol_Click;
            adminText.Click += AdminText_Click;
        }

        private void AdminText_Click(object sender, EventArgs e)
        {
            SetContentView(Resource.Layout.adminGiris);
            admuser = FindViewById<EditText>(Resource.Id.UsName);
            admpass = FindViewById<EditText>(Resource.Id.PssW);
            admbuton = FindViewById<Button>(Resource.Id.AdmBtn);
            admbuton.Click += Admbuton_Click;

            string usr1 = Convert.ToString(admuser.Text);
            string pss1 = Convert.ToString(admpass.Text);

            string usr2 = "admin";
            string pss2 = "!1qaz2WSX3edc4RFV%56";

            if (usr1==usr2 && pss1==pss2)
            {
                Admbuton_Click(sender, e);
            }

        }

        private void Admbuton_Click(object sender, EventArgs e)
        {
            SetContentView(Resource.Layout.adminpanel);
            db = new Database();
            db.createDatabase();
            string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            Log.Info("DB_PATH", folder);

            lstData = FindViewById<ListView>(Resource.Id.uyeList);

            admAd = FindViewById<EditText>(Resource.Id.ekAd);
            admSyd = FindViewById<EditText>(Resource.Id.ekSoyad);
            admTc = FindViewById<EditText>(Resource.Id.ekTc);
            admMail = FindViewById<EditText>(Resource.Id.ekMail);
            admPss = FindViewById<EditText>(Resource.Id.ekPass);

            addBtn = FindViewById<Button>(Resource.Id.btnAdd);
            editBtn = FindViewById<Button>(Resource.Id.btnEdit);
            removeBtn = FindViewById<Button>(Resource.Id.btnRemove);

            addBtn.Click += AddBtn_Click;
            editBtn.Click += EditBtn_Click;
            removeBtn.Click += RemoveBtn_Click;
            lstData.ItemClick += LstData_ItemClick;
        }

        private void LstData_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            for (int i = 0; i < lstData.Count; i++)
            {
                if (e.Position == i)
                {
                    lstData.GetChildAt(i).SetBackgroundColor(Android.Graphics.Color.Red);
                }
                else
                {
                    lstData.GetChildAt(i).SetBackgroundColor(Android.Graphics.Color.Transparent);
                }
            }
        }

        private void RemoveBtn_Click(object sender, EventArgs e)
        {
            Personal personal = new Personal()
            {
                tckn = tctxt.Text,
                ad = adtxt.Text,
                soyad = soyadtxt.Text,
                mail = mailtxt.Text,
                pass = passtxt.Text
            };
            db.updateTablePerson(personal);
            LoadData();
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            Personal personal = new Personal()
            {
                tckn = tctxt.Text,
                ad = adtxt.Text,
                soyad = soyadtxt.Text,
                mail = mailtxt.Text,
                pass = passtxt.Text
            };
            db.updateTablePerson(personal);
            LoadData();
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            Personal personal = new Personal()
            {
                tckn = tctxt.Text,
                ad = adtxt.Text,
                soyad = soyadtxt.Text,
                mail = mailtxt.Text,
                pass = passtxt.Text
            };
            db.InsertIntoTablePerson(personal);
            LoadData();
        }


        private void LoadData()
        {
            lstSource = db.selectTablePerson();

            var adapter = new ListViewAdapter(this, lstSource);
            lstData.Adapter = adapter;
        }


        //private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        //{
        //    StartActivity(new Intent(this, typeof(MainActivity)));
        //}

        //private byte[] ConvertFileToByteArray(Stream input)
        //{
        //    byte[] buffer = new byte[16 * 1024];
        //    using (MemoryStream ms=new MemoryStream())
        //    {
        //        int read;
        //        while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
        //            ms.Write(buffer, 0, read);
        //        return ms.ToArray();
        //    }
        //}

        void Son()
        {
            //string usercon = Convert.ToString(user.Text);
            //int passcon = Convert.ToInt32(pass.Text);

            //int pss = 1234;
            //string usr = "emredandil";

            //Personal personal = new Personal()
            //{
            //    tckn = tctxt.Text,
            //    ad = adtxt.Text,
            //    soyad = soyadtxt.Text,
            //    mail = mailtxt.Text,
            //    pass = passtxt.Text
            //};
            //db.selectQueryTablePerson();

            //var personal = db.selectQueryTablePerson(bool pers);
            //var control = false;

            var username = user.Text;
            var password = pass.Text;

            //for (int i = 0; i < persons.Count; i++)
            //{
            //    if (persons[i].mail==username && persons[i].pass==password)
            //    {
            //        control = true;
            //    }
            //}

            //if (control)//pss == passcon && usr == usercon
            //{

            //    Toast.MakeText(this, "Hoşgeldiniz.", ToastLength.Long).Show();
            //    SetContentView(Resource.Layout.otolistesi);
            //    otoListe = FindViewById<ListView>(Resource.Id.otoList);
            //    otoListe.ItemClick += OtoListe_ItemClick;

            //}
            //else
            //{
            //    Toast.MakeText(this, "Tekrar Deneyiniz...", ToastLength.Long).Show();
            //}

            //db.InsertIntoTablePerson(personal);

            //db.updateTablePerson(personal);
            //LoadData();

            //string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            //Log.Info("DB_PATH", folder);





        }
        void Sofor()
        {
            //Personal personal = new Personal()
            //{
            //    tckn = int.Parse(tctxt.Text),
            //    ad = adtxt.Text,
            //    soyad = soyadtxt.Text,
            //    mail = mailtxt.Text,
            //    pass = passtxt.Text

            //};
            //db.selectTablePerson();
            //db.InsertIntoTablePerson(personal);
            //LoadData();
            //string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            //Log.Info("DB_PATH", folder);


            //var persons = db.selectQueryTablePerson();
            //var control = false;

            //var username = user.Text;
            //var password = pass.Text;

            //for (int i = 0; i < persons.Count; i++)
            //{
            //    if (persons[i].mail == username && persons[i].pass == password)
            //    {
            //        control = true;
            //    }
            //}

            //string usercon = Convert.ToString(user.Text);
            string passcon = Convert.ToString(pass.Text);

            string pss = "1234";
            //string usr = "faig";
            //if (persons !=null)
            //{
            //    Toast.MakeText(this, "Hoşgeldiniz.", ToastLength.Long).Show();
            //    SetContentView(Resource.Layout.otolistesi);
            //    otoListe = FindViewById<ListView>(Resource.Id.otoList);
            //    otoListe.ItemClick += OtoListe_ItemClick;
            //}
            //else
            //{
            //    Toast.MakeText(this, "Tekrar Deneyiniz...", ToastLength.Long).Show();
            //}
        }

        private void Uyeol_Click(object sender, EventArgs e)
        {
            SetContentView(Resource.Layout.uyeol);

            uyeolun = FindViewById<Button>(Resource.Id.uyeol1);

            uyeolun.Click += Uyeolun_Click;
        }

        private void Uyeolun_Click(object sender, EventArgs e)
        {
            tctxt = FindViewById<EditText>(Resource.Id.edTc);
            adtxt = FindViewById<EditText>(Resource.Id.edAd);
            soyadtxt = FindViewById<EditText>(Resource.Id.edSoyad);
            mailtxt = FindViewById<EditText>(Resource.Id.edMail);
            passtxt = FindViewById<EditText>(Resource.Id.edPass);
            ayikSur = FindViewById<RadioButton>(Resource.Id.aykSur);

            Personal personal = new Personal()
            {
                tckn = tctxt.Text,
                ad = adtxt.Text,
                soyad = soyadtxt.Text,
                mail = mailtxt.Text,
                pass = passtxt.Text,
            };
            db.InsertIntoTablePerson(personal);
            SetContentView(Resource.Layout.activity_main);
            //LoadData();
        }

        
        private void Giris_Click(object sender, EventArgs e)
        {
            //LoadData();
            Son();
        }
        private void GirSurucu_Click(object sender, EventArgs e)
        {
            //LoadData();
            Sofor();
            //throw new NotImplementedException();
        }
        public class HelloWebViewClient : WebViewClient
        {
            // For API level 24 and later
            public override bool ShouldOverrideUrlLoading(WebView view, IWebResourceRequest request)
            {
                view.LoadUrl(request.Url.ToString());
                return false;
            }
            public override bool ShouldOverrideUrlLoading(WebView view, string url)
            {
                view.LoadUrl(url);
                return false;
            }
        }
        private void TxtView_Click(object sender, EventArgs e)
        {
            SetContentView(Resource.Layout.sifreUnut);
            sifreUnut = FindViewById<WebView>(Resource.Id.webView1);
            sifreUnut.Settings.JavaScriptEnabled = true;
            sifreUnut.SetWebViewClient(new HelloWebViewClient());
            sifreUnut.LoadUrl("http://geriliyom.epizy.com/%c5%9fifre.html");
            //throw new NotImplementedException();
        }
        
        
        private void OtoListe_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            SetContentView(Resource.Layout.sehirEkrani);
            sehirListe = FindViewById<ListView>(Resource.Id.sehirler);
            sehirListe.ItemClick += SehirListe_ItemClick;
        }
        private void SehirListe_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            // Set our view from the "main" layout resource

            switch (e.Position)
            {
                case 0:
                    SetContentView(Resource.Layout.mapist);
                    secist = FindViewById<Button>(Resource.Id.secIst);
                    secist.Click += Secist_Click;
                    break;
                case 1:
                    SetContentView(Resource.Layout.mapankara);
                    secank = FindViewById<Button>(Resource.Id.secAnk);
                    secank.Click += Secank_Click;
                    break;
                case 2:
                    SetContentView(Resource.Layout.mapizmir);
                    secizm = FindViewById<Button>(Resource.Id.secIzm);
                    secizm.Click += Secizm_Click;
                    break;
                default:
                    break;

                SetUpMap();
            }
            

        }
        private void Secizm_Click(object sender, EventArgs e)
        {
            SetContentView(Resource.Layout.yildiz);
            RatingBar ratingbar = FindViewById<RatingBar>(Resource.Id.ratingbar);
            ratingbar.RatingBarChange += (o, e) =>
            {
                Toast.MakeText(this, "New Rating: " + ratingbar.Rating.ToString(), ToastLength.Short).Show();
                Finish();
            };
        }
        private void Secank_Click(object sender, EventArgs e)
        {
            SetContentView(Resource.Layout.yildiz);
            RatingBar ratingBar = FindViewById<RatingBar>(Resource.Id.ratingbar);
            ratingBar.RatingBarChange += (o, e) =>
            {
                Toast.MakeText(this, "New Rating: " + ratingBar.Rating.ToString(), ToastLength.Short).Show();
                Finish();
            };
        }

        private void Secist_Click(object sender, EventArgs e)
        {
            SetContentView(Resource.Layout.yildiz);
            RatingBar ratingBar = FindViewById<RatingBar>(Resource.Id.ratingbar);
            ratingBar.RatingBarChange += (o, e) =>
            {
                Toast.MakeText(this, "New Rating: " + ratingBar.Rating.ToString(), ToastLength.Short).Show();
                Finish();
            };
        }

        private void SetUpMap()
        {
            if (mapgoogleist == null && mapgoogleank == null && mapgoogleizm == null)
            {
                FragmentManager.FindFragmentById<MapFragment>(Resource.Id.map).GetMapAsync(this);
            }
        }
        public void OnMapReady(GoogleMap googleMap)
        {
            mapgoogleist = googleMap;
            mapgoogleank = googleMap;
            mapgoogleizm = googleMap;

            LatLng latLngIst = new LatLng(41.0122, 28.976);
            MarkerOptions optionsIst = new MarkerOptions().SetPosition(latLngIst).SetTitle("İstanbul").SetSnippet("İstanbul").Draggable(true);

            LatLng latLngAnk = new LatLng(39.933365, 32.859741);
            MarkerOptions optionsAnk = new MarkerOptions().SetPosition(latLngAnk).SetTitle("Ankara").SetSnippet("Ankara").Draggable(true);

            LatLng latLngIzm = new LatLng(38.4127, 27.1384);
            MarkerOptions optionsIzm = new MarkerOptions().SetPosition(latLngIzm).SetTitle("İzmir").SetSnippet("İzmir").Draggable(true);

            CameraUpdate cameraist = CameraUpdateFactory.NewLatLngZoom(latLngIst, 20);
            CameraUpdate cameraank = CameraUpdateFactory.NewLatLngZoom(latLngAnk, 20);
            CameraUpdate cameraizm = CameraUpdateFactory.NewLatLngZoom(latLngIzm, 20);

            mapgoogleist.MoveCamera(cameraist);
            mapgoogleank.MoveCamera(cameraank);
            mapgoogleizm.MoveCamera(cameraizm);

            mapgoogleist.AddMarker(new MarkerOptions().SetPosition(latLngIst).SetTitle("İstanbul"));
            mapgoogleank.AddMarker(new MarkerOptions().SetPosition(latLngAnk).SetTitle("Ankara"));
            mapgoogleizm.AddMarker(new MarkerOptions().SetPosition(latLngIzm).SetTitle("İzmir"));

            mapgoogleist.MarkerDragEnd += Mapgoogleist_MarkerDragEnd;
            mapgoogleank.MarkerDragEnd += Mapgoogleank_MarkerDragEnd;
            mapgoogleizm.MarkerDragEnd += Mapgoogleizm_MarkerDragEnd;
        }
        private void Mapgoogleist_MarkerDragEnd(object sender, GoogleMap.MarkerDragEndEventArgs e)
        {
            LatLng pos = e.Marker.Position;
            Console.WriteLine(pos.ToString());
        }
        private void Mapgoogleank_MarkerDragEnd(object sender, GoogleMap.MarkerDragEndEventArgs e)
        {
            LatLng pos = e.Marker.Position;
            Console.WriteLine(pos.ToString());
        }
        private void Mapgoogleizm_MarkerDragEnd(object sender, GoogleMap.MarkerDragEndEventArgs e)
        {
            LatLng pos = e.Marker.Position;
            Console.WriteLine(pos.ToString());
        }



    }
}