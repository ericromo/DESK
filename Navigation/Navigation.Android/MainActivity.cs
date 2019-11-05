using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Plugin.CurrentActivity;
using Plugin.Fingerprint;
//using MySql.Data.MySqlClient;
using System.Collections;
using System.Timers;
using System.IO;
using Navigation.Interfaces;

namespace Navigation.Droid
{

    [Activity(Label = "Navigation", Icon = "@mipmap/icon", Theme = "@style/MainTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);
            CrossCurrentActivity.Current.Init(this, bundle);


            CrossFingerprint.SetCurrentActivityResolver(() => CrossCurrentActivity.Current.Activity);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            var width = Resources.DisplayMetrics.WidthPixels;
            var height = Resources.DisplayMetrics.HeightPixels;
            var density = Resources.DisplayMetrics.Density;

            App.ScreenWidth = (width - 0.5f) / density;
            App.ScreenHeight = (height - 0.5f) / density;

            LoadApplication(new App());





        }

        public class CloseApplication : ICloseApplication
        {
            public void closeApplication()
            {
                Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
            }
        }
    }
}