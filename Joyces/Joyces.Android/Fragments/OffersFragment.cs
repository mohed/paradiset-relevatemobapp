using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace Joyces.Droid.Fragments
{
    public class OffersFragment : Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.OffersFragment, container, false);
            ListView offerListView = view.FindViewById<ListView>(Resource.Id.offersListView);
            CustomListViewOffersAdapter adapter = new CustomListViewOffersAdapter(container.Context, Joyces.Platform.AppContext.Instance.Platform.OfferList);
            offerListView.Adapter = adapter;
            return view;
        }
    }
}