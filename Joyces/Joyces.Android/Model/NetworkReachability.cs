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
using System.Threading;
using Android.Net;
using System.Threading.Tasks;

namespace Joyces.Droid.Model
{
    public class NetworkReachability
    {
        Activity activity;
        ConnectivityManager connectivityManager;
        NetworkInfo networkInfo;

        public NetworkReachability(Activity activity)
        {
            this.activity = activity;
            connectivityManager = (ConnectivityManager)activity.GetSystemService(Context.ConnectivityService);
        }

        public bool CheckInternetConnection()
        {
            if (CheckCheckNetwork() || CheckWifi())
                return true;
            else
                return false;
        }

        private bool CheckCheckNetwork()
        {
            networkInfo = connectivityManager.ActiveNetworkInfo;
            bool isOnline = networkInfo.IsConnected;

            if (isOnline)
                return true;
            else
                return false;
        }

        private bool CheckWifi()
        {
            bool isWifi = networkInfo.Type == ConnectivityType.Wifi;

            if (isWifi)
                return true;
            else
                return false;
        }
    }
}