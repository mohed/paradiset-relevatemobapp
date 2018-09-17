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
using System.Net;
using System.Threading.Tasks;
using System.IO;
using FFImageLoading.Views;
using FFImageLoading;
using Joyces.Repository;

namespace Joyces.Droid
{
    class CustomListViewOffersAdapter : BaseAdapter<Offer>
    {
        private int iMaxChars = 95;
        private List<Offer> mItem;
        private Context mContext;
        private TabMenuActivity parentTabView;

        // constructor for development purposes
        public CustomListViewOffersAdapter(Context context, List<Offer> items) //, TabMenuActivity parent)
        {
            mItem = items;
            mContext = context;
            //parentTabView = parent;
        }

        public CustomListViewOffersAdapter(Context context, List<Offer> items, TabMenuActivity parent)
        {
            mItem = items;
            mContext = context;
            parentTabView = parent;
        }

        public override int Count
        {
            get { return mItem.Count; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override Offer this[int position]
        {
            get { return mItem[position]; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View row = convertView;

            if (String.Equals(mItem[position].type, "Punch Card"))
            {
                row = buildStampCardView(position, convertView, parent);
            }
            else
            {
                if (row == null)
                    row = LayoutInflater.From(mContext).Inflate(Resource.Layout.CustomListViewRowOffers, null, false);

                ImageViewAsync imgOfferImage = row.FindViewById<ImageViewAsync>(Resource.Id.imageViewOffer);
                TextView txtHeadline = row.FindViewById<TextView>(Resource.Id.textViewHeadline);
                TextView txtDescription = row.FindViewById<TextView>(Resource.Id.textViewNote);
                TextView txtValue = row.FindViewById<TextView>(Resource.Id.textViewValue);
                TextView txtValidDateTime = row.FindViewById<TextView>(Resource.Id.textViewValidDateTime);

                //Typeface tf = Typeface.CreateFromAsset(parentTabView.Assets, Joyces.Helpers.Settings.MainFont);
                //txtHeadline.SetTypeface(tf, Android.Graphics.TypefaceStyle.Normal);
                //txtDescription.SetTypeface(tf, Android.Graphics.TypefaceStyle.Normal);
                //txtValue.SetTypeface(tf, Android.Graphics.TypefaceStyle.Normal);
                //txtValidDateTime.SetTypeface(tf, Android.Graphics.TypefaceStyle.Normal);


                if (!string.IsNullOrEmpty(mItem[position].imageUrl))
                    ImageService.Instance.LoadUrl(mItem[position].imageUrl).Into(imgOfferImage);

                if (!string.IsNullOrEmpty(mItem[position].name))
                    txtHeadline.Text = mItem[position].name;
                else
                    txtHeadline.Text = "";

                if (!string.IsNullOrEmpty(mItem[position].note))
                {
                    if (mItem[position].note.Length > iMaxChars)
                        txtDescription.Text = mItem[position].note.Substring(0, iMaxChars) + "...";
                    else
                        txtDescription.Text = mItem[position].note;
                }
                else
                    txtDescription.Text = "";


                txtValue.Text = ObjectRepository.parseOfferValue(mItem[position]);
                txtValidDateTime.Text = "Valid until " + ObjectRepository.ParseDateTimeToCulture(mItem[position].validityDate);

                //parentTabView.DismissProgressbar();
            }

            return row;
        }

        public View buildStampCardView(int position, View view, ViewGroup parent)
        {
            View row = view;
            if (row == null)
            {
                row = LayoutInflater.From(mContext).Inflate(Resource.Layout.CustomListViewRowPunchCard, null, false);
            }

            TextView txtHeadline = row.FindViewById<TextView>(Resource.Id.textViewHeadline);
            TextView txtName = row.FindViewById<TextView>(Resource.Id.textViewName);
            ImageViewAsync imgOfferImage = row.FindViewById<ImageViewAsync>(Resource.Id.imageViewOffer);
            txtHeadline.Text = mItem[position].type;
            txtName.Text = mItem[position].name;
            ImageService.Instance.LoadUrl(mItem[position].imageUrl).Into(imgOfferImage);
            return row;
        }
    }    
}