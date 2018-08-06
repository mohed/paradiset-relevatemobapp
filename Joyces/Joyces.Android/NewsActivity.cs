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
using Newtonsoft.Json;
using FFImageLoading.Views;
using FFImageLoading;
using Android.Graphics.Drawables;
using Android.Graphics;

namespace Joyces.Droid
{
    [Activity(Label = "News", Theme = "@style/CustomActionBarTheme")]
    public class NewsActivity : Activity
    {
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            base.OnBackPressed();

            return true;
        }


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.NewsView);

            ActionBar.SetHomeButtonEnabled(true);
            ActionBar.SetDisplayHomeAsUpEnabled(true);

            var obj = JsonConvert.DeserializeObject<News>(Intent.GetStringExtra("NewsActivity"));

            ImageViewAsync imageViewNews2 = FindViewById<ImageViewAsync>(Resource.Id.imageViewNews2);
            TextView textViewHeadline2 = FindViewById<TextView>(Resource.Id.textViewHeadline2);
            TextView textViewDescription2 = FindViewById<TextView>(Resource.Id.textViewDescription2);
            Typeface tf = Typeface.CreateFromAsset(Assets, Joyces.Helpers.Settings.MainFont);
            textViewHeadline2.SetTypeface(tf, Android.Graphics.TypefaceStyle.Normal);
            textViewDescription2.SetTypeface(tf, Android.Graphics.TypefaceStyle.Normal);

            if (!string.IsNullOrEmpty(obj.imageUrl))
                ImageService.Instance.LoadUrl(obj.imageUrl).Into(imageViewNews2);

            if (!string.IsNullOrEmpty(obj.name))
            {
                textViewHeadline2.Text = obj.name;
                ActionBar.Title = obj.name;
            }
            else
                textViewHeadline2.Text = string.Empty;

            if (!string.IsNullOrEmpty(obj.note))
                textViewDescription2.Text = obj.note;
            else
                textViewDescription2.Text = string.Empty;

            ActionBar.SetBackgroundDrawable(new ColorDrawable(Android.Graphics.Color.ParseColor(GeneralSettings.AndroidActionBarColor)));
        }
    }
}