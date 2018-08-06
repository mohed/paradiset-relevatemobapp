using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V4.View;
using TreePager;
using Android.Graphics.Drawables;

namespace Joyces.Droid
{
    [Activity(Label = "Erbjudanden", Theme = "@style/CustomActionBarTheme")]
    public class OffersActivity : Activity
    {
       


        

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.OffersView);

            ActionBar.SetBackgroundDrawable(new ColorDrawable(Android.Graphics.Color.ParseColor(GeneralSettings.AndroidActionBarColor)));
        }
    }
}