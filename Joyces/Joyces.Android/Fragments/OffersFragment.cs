using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.View;
using Android.Util;
using Android.Views;
using Android.Widget;

using Newtonsoft.Json;

using FragmentTreePager;
using TreePager;

namespace Joyces.Droid.Fragments
{
    public class OffersFragment : Fragment
    {
        private string tag = "Relapp";
        View view;
        private String sSelectedTab = string.Empty;

        async public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
            var resp = await RestAPI.GetOffer(Joyces.Helpers.Settings.UserEmail, Joyces.Helpers.Settings.AccessToken);

            if (resp != null && resp is List<Offer>)
            {
                Joyces.Platform.AppContext.Instance.Platform.OfferList = (List<Offer>)resp;
            }
            else
            {
                Joyces.Platform.AppContext.Instance.Platform.OfferList = null;
            }
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            view = inflater.Inflate(Resource.Layout.OffersFragment, container, false);
            ListView offerListView = view.FindViewById<ListView>(Resource.Id.offersListView);
            CustomListViewOffersAdapter adapter = new CustomListViewOffersAdapter(container.Context, Joyces.Platform.AppContext.Instance.Platform.OfferList);
            offerListView.Adapter = adapter;
            return view;
        }

        // Does not seem to be called anymore
        private async void LoadOffersSingleView()
        {
            try
            {
                var OfferList = Joyces.Platform.AppContext.Instance.Platform.OfferList;

                if (OfferList != null)
                {
                    CustomListViewOffersAdapter adapter = new CustomListViewOffersAdapter(view.Context, Joyces.Platform.AppContext.Instance.Platform.OfferList);
                    ListView listViewOffers = view.FindViewById<ListView>(Resource.Id.listViewOffers);

                    if (adapter != null)
                        listViewOffers.Adapter = adapter;

                    listViewOffers.ItemClick += ListViewOffers_ItemClick;
                }
                else
                {
                    Alert(Lang.MESSAGE_HEADLINE, Lang.SERVICE_NOT_AVAILABLE, "Try again");
                }
            }
            catch (Exception ex)
            {
                Log.Error(tag, "Exception with the following msg was thrown. Msg: " + ex.ToString());
            }
        }

        // Does not seem to be called anymore
        private async void LoadOffersView()
        {
            var OfferList = Joyces.Platform.AppContext.Instance.Platform.OfferList;

            if (OfferList != null)
            {
                ViewPager viewPager = view.FindViewById<ViewPager>(Resource.Id.viewpager);

                FragmentTreePager.TreeCatalog treeCatalog = new FragmentTreePager.TreeCatalog();
                viewPager.Adapter = new FragmentTreePagerAdapter(view.Context, treeCatalog, OfferList);
            }
            else
            {
                Alert(Lang.MESSAGE_HEADLINE, Lang.SERVICE_NOT_AVAILABLE, Lang.BUTTON_OK);
            }
        }

        private void ListViewOffers_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var singleNewsItem = Joyces.Platform.AppContext.Instance.Platform.OfferList[e.Position];

            if (singleNewsItem != null)
            {
                var intent = new Intent(view.Context, typeof(OfferActivity));
                intent.PutExtra("OfferActivity", JsonConvert.SerializeObject(singleNewsItem));
                StartActivity(intent);
            }
            else
            {
                Alert(Lang.MESSAGE_HEADLINE, Lang.SERVICE_NOT_AVAILABLE, Lang.BUTTON_OK);
            }
        }

        private void Alert(string sHeadline, string sMessage, string sButtonText)
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(view.Context);
            AlertDialog ad = builder.Create();
            ad.SetTitle(sHeadline);
            ad.SetIcon(Android.Resource.Drawable.IcDialogAlert);
            ad.SetMessage(sMessage);
            ad.SetButton(sButtonText, (s, e) => { Console.WriteLine("OK Button clicked, alert dismissed"); });
            ad.Show();
        }
    }
}