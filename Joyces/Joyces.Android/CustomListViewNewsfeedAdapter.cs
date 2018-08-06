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
using FFImageLoading;
using FFImageLoading.Views;

namespace Joyces.Droid
{
    class CustomListViewNewsfeedAdapter : BaseAdapter<News>
    {
        private int iMaxChars = 95;
        private List<News> mItem;
        private Context mContext;
        private TabMenuActivity parentTabView;

        public CustomListViewNewsfeedAdapter(Context context, List<News> items, TabMenuActivity parent)
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

        public override News this[int position]
        {
            get { return mItem[position]; }
        }

        private async Task saveImage(Bitmap image)
        {
            //Spara bilden
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View row = convertView;

            if (row == null)
                row = LayoutInflater.From(mContext).Inflate(Resource.Layout.CustomListViewRowNewsfeed, null, false);

            ImageViewAsync imgNewsImage = row.FindViewById<ImageViewAsync>(Resource.Id.imageViewNews);
            TextView txtHeadline = row.FindViewById<TextView>(Resource.Id.textViewHeadline);
            TextView txtDescription = row.FindViewById<TextView>(Resource.Id.textViewDescription);

            Typeface tf = Typeface.CreateFromAsset(parentTabView.Assets, Joyces.Helpers.Settings.MainFont);
            txtHeadline.SetTypeface(tf, Android.Graphics.TypefaceStyle.Normal);
            txtDescription.SetTypeface(tf, Android.Graphics.TypefaceStyle.Normal);
          
            if (!string.IsNullOrEmpty(mItem[position].imageUrl))
                ImageService.Instance.LoadUrl(mItem[position].imageUrl).Into(imgNewsImage);


            if (!string.IsNullOrEmpty(mItem[position].name))
                txtHeadline.Text = mItem[position].name;
            else
                txtHeadline.Text = "";

            if (!string.IsNullOrEmpty(mItem[position].name))
            {
                if (mItem[position].note.Length > iMaxChars)
                    txtDescription.Text = mItem[position].note.Substring(0, iMaxChars) + "...";
                else
                    txtDescription.Text = mItem[position].note;
            }
            else
                txtDescription.Text = "";

            parentTabView.DismissProgressbar();

            return row;
        }
    }
}