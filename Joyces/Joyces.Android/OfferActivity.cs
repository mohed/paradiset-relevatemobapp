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
using Joyces.Droid.Model;
using Joyces.Repository;
using Android.Graphics.Drawables;
using Android.Graphics;

namespace Joyces.Droid
{
    [Activity(Label = "Erbjudande", Theme = "@style/CustomActionBarTheme")]
    public class OfferActivity : Activity
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
            SetContentView(Resource.Layout.OfferView);

            ActionBar.SetHomeButtonEnabled(true);
            ActionBar.SetDisplayHomeAsUpEnabled(true);

            var obj = JsonConvert.DeserializeObject<Offer>(Intent.GetStringExtra("OfferActivity"));

            ImageViewAsync imageViewOffer2 = FindViewById<ImageViewAsync>(Resource.Id.imageViewNews2);
            TextView textViewHeadline2 = FindViewById<TextView>(Resource.Id.textViewHeadline2);
            TextView textViewDescription2 = FindViewById<TextView>(Resource.Id.textViewDescription2);
            TextView textViewDutyText2 = FindViewById<TextView>(Resource.Id.textViewDutyText2);
            TextView textViewValidDate2 = FindViewById<TextView>(Resource.Id.textViewValidDate2);
            TextView textViewOfferValue2 = FindViewById<TextView>(Resource.Id.textViewOfferValue2);
            ImageView imageViewNewsQRCode = FindViewById<ImageView>(Resource.Id.imageViewNewsQRCode);
            Typeface tf = Typeface.CreateFromAsset(Assets, Joyces.Helpers.Settings.MainFont);
            textViewHeadline2.SetTypeface(tf, Android.Graphics.TypefaceStyle.Normal);
            textViewDescription2.SetTypeface(tf, Android.Graphics.TypefaceStyle.Normal);
            textViewDutyText2.SetTypeface(tf, Android.Graphics.TypefaceStyle.Normal);
            textViewValidDate2.SetTypeface(tf, Android.Graphics.TypefaceStyle.Normal);
            textViewOfferValue2.SetTypeface(tf, Android.Graphics.TypefaceStyle.Normal);


            if (!string.IsNullOrEmpty(obj.imageUrl))
                ImageService.Instance.LoadUrl(obj.imageUrl).Into(imageViewOffer2);


            if (!string.IsNullOrEmpty(obj.imageUrl))
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

            if (!string.IsNullOrEmpty(obj.dutyText))
                textViewDutyText2.Text = obj.dutyText;
            else
                textViewDutyText2.Text = string.Empty;


            textViewValidDate2.Text = Lang.VALID_UNTIL + " " + ObjectRepository.ParseDateTimeToCulture(obj.validityDate);
            textViewOfferValue2.Text = ObjectRepository.parseOfferValue(obj);

            if (!string.IsNullOrEmpty(obj.code))
                imageViewNewsQRCode.SetImageBitmap(AndroidHelper.GetQrCode(obj.code, 300, 300, 0));
            else
                imageViewNewsQRCode.SetImageBitmap(AndroidHelper.GetQrCode("0000", 300, 300, 0));

            imageViewNewsQRCode.Visibility = ViewStates.Gone;

            ActionBar.SetBackgroundDrawable(new ColorDrawable(Android.Graphics.Color.ParseColor(GeneralSettings.AndroidActionBarColor)));
        }
    }
}