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
using Joyces;
using Android.Graphics;
using System.Net;
using FFImageLoading.Views;
using FFImageLoading;

namespace Joyces.Droid
{
    class CustomListViewMoreAdapter : BaseAdapter<More>
    {
        private List<More> mItem;
        private Context mContext;
        private TabMenuActivity parentTabView;

        public CustomListViewMoreAdapter(Context context, List<More> items, TabMenuActivity parent)
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

        public override More this[int position]
        {
            get { return mItem[position]; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            try
            {
                View row = convertView;

                if (row == null)
                    row = LayoutInflater.From(mContext).Inflate(Resource.Layout.MoreCellView, null, false);

                //ImageView imgNewsImage = row.FindViewById<ImageView>(Resource.Id.imageViewMore);
                //var imageBitmap = AndroidHelper.GetImageFromUrl(mItem[position].imageUrl);
                //imgNewsImage.SetImageBitmap(imageBitmap);

                ImageViewAsync imgMoreImage = row.FindViewById<ImageViewAsync>(Resource.Id.imageViewMore);
                TextView txtHeadline = row.FindViewById<TextView>(Resource.Id.textViewMoreHeadline);
                TextView txtDescription = row.FindViewById<TextView>(Resource.Id.textViewMoreDescription);

                Typeface tf = Typeface.CreateFromAsset(parentTabView.Assets, Joyces.Helpers.Settings.MainFont);
                txtHeadline.SetTypeface(tf, Android.Graphics.TypefaceStyle.Normal);
                txtDescription.SetTypeface(tf, Android.Graphics.TypefaceStyle.Normal);

                if (!string.IsNullOrEmpty(mItem[position].imageUrl))
                    ImageService.Instance.LoadUrl(mItem[position].imageUrl).Into(imgMoreImage);


                if (!string.IsNullOrEmpty(mItem[position].desc))
                    txtHeadline.Text = mItem[position].desc;
                else
                    txtHeadline.Text = "";

                if (!string.IsNullOrEmpty(mItem[position].note))
                    txtDescription.Text = mItem[position].note;
                else
                    txtDescription.Text = "";

                parentTabView.DismissProgressbar();

                return row;
            }
            catch (Exception ee)
            {
                throw ee;
            }
        }


    }
}