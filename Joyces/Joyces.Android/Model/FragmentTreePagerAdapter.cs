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
using System.Threading.Tasks;
using Newtonsoft.Json;
using Joyces;
using Joyces.Droid;

namespace FragmentTreePager
{
    class FragmentTreePagerAdapter : PagerAdapter
    {
        Context context;
        TreeCatalog treeCatalog;
        List<Offer> offerList;

        public FragmentTreePagerAdapter(Context context, TreeCatalog treeCatalog, List<Offer> OfferList)
        {
            this.context = context;
            this.treeCatalog = treeCatalog;
            this.offerList = OfferList;
         }

        public override int Count
        {
            get { return treeCatalog.NumTrees; }
        }

        public override bool IsViewFromObject(View view, Java.Lang.Object obj)
        {
            return view == obj;
        }

        public override Java.Lang.Object InstantiateItem(View container, int position)
        {
            var viewPager = container.JavaCast<ViewPager>();

            if (position == 0)
            {
                //var imageView = new ImageView(context);
                //imageView.SetImageResource(treeCatalog[position].imageId);
                //viewPager.AddView(imageView);
                //return imageView;

                var listViewOffers = new ListView(context);
                ViewGroup.LayoutParams layoutparams = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);
                listViewOffers.SetBackgroundColor(Android.Graphics.Color.White);

                CustomListViewOffersAdapter adapter = new CustomListViewOffersAdapter(context, offerList);
                listViewOffers.Adapter = adapter;
                listViewOffers.ItemClick += ListViewOffers_ItemClick;

                viewPager.AddView(listViewOffers, layoutparams);

                return listViewOffers;
            }
            else
            {
                var linearLayout = new LinearLayout(context);
                ViewGroup.LayoutParams layoutparams = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);
                linearLayout.SetBackgroundColor(Android.Graphics.Color.Yellow);
                var txtView = new TextView(context);
                txtView.Text = "HEJ";
                txtView.TextSize = 15;
                txtView.SetTextColor(Android.Graphics.Color.Magenta);
                linearLayout.AddView(txtView);
                viewPager.AddView(linearLayout, layoutparams);
                return linearLayout;
            }
        }

        private void ListViewOffers_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var singleNewsItem = offerList[e.Position];
            Alert("OfferActivity", JsonConvert.SerializeObject(singleNewsItem), "OK");
            
        }

        public override void DestroyItem(View container, int position, Java.Lang.Object view)
        {
            var viewPager = container.JavaCast<ViewPager>();
            viewPager.RemoveView(view as View);
        }

        public override Java.Lang.ICharSequence GetPageTitleFormatted(int position)
        {
            return new Java.Lang.String(treeCatalog[position].caption);
        }

        private void Alert(string sHeadline, string sMessage, string sButtonText)
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(context);
            AlertDialog ad = builder.Create();
            ad.SetTitle(sHeadline);
            ad.SetIcon(Android.Resource.Drawable.IcDialogAlert);
            ad.SetMessage(sMessage);
            ad.SetButton(sButtonText, (s, e) => { Console.WriteLine("OK Button clicked, alert dismissed"); });

            ad.Show();
        }
    }

    // TreePage: contains image resource ID and caption for a tree:
    public class TreePage
    {

        // Image ID for this tree image:
        public int imageId;

        // Caption text for this image:
        public string caption;

        // Returns the ID of the image:
        public int ImageID { get { return imageId; } }

        // Returns the caption text for the image:
        public string Caption { get { return caption; } }
    }

    // Tree catalog: holds image resource IDs and caption text:
    public class TreeCatalog
    {
        // Built-in tree catalog (could be replaced with a database)
        static TreePage[] treeBuiltInCatalog = {
            new TreePage { caption = "Offers" },
            new TreePage { caption = "Rewards" },
        };

        // Array of tree pages that make up the catalog:
        private TreePage[] treePages;

        // Create an instance copy of the built-in tree catalog:
        public TreeCatalog() { treePages = treeBuiltInCatalog; }

        // Indexer (read only) for accessing a tree page:
        public TreePage this[int i] { get { return treePages[i]; } }

        // Returns the number of tree pages in the catalog:
        public int NumTrees { get { return treePages.Length; } }
    }
}