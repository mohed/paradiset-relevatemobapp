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
using Android.Graphics;

namespace Joyces.Droid
{
    [Activity(Label = "IdActivity")]
    public class IdActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            

            base.OnCreate(savedInstanceState);

            

            // Create your application here
            SetContentView(Resource.Layout.IdView);

            setCurrentClientTheme();
            //var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);

            ////Toolbar will now take on default actionbar characteristics
            //SetActionBar(toolbar);

            //ActionBar.Title = "Abalon";
        }


        private void setCurrentClientTheme()
        {
                Typeface tf = Typeface.CreateFromAsset(Assets, Joyces.Helpers.Settings.MainFont);
                TextView txtHeader = FindViewById<TextView>(Resource.Id.textView1);
                txtHeader.SetTypeface(tf, Android.Graphics.TypefaceStyle.Normal);
                txtHeader.Text = Lang.ID_HEADER;

                TextView txtInfo = FindViewById<TextView>(Resource.Id.textView2);
                txtInfo.SetTypeface(tf, Android.Graphics.TypefaceStyle.Normal);
                txtInfo.Text = Lang.ID_DESCRIPTION;

                TextView txtInfoOffer = FindViewById<TextView>(Resource.Id.textView3);
                txtInfoOffer.SetTypeface(tf, Android.Graphics.TypefaceStyle.Normal);
                txtInfoOffer.Text = Lang.ID_DESCRIPTION_NEWS;

               LinearLayout layoutBackCode = FindViewById<LinearLayout>(Resource.Id.idviewLinearBack);
                layoutBackCode.SetBackgroundColor(Android.Graphics.Color.ParseColor(GeneralSettings.BackgroundColor));

               
        }
    }
}