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
using Joyces.Droid.Model;
using Android.Views.InputMethods;

namespace Joyces.Droid
{
    [Activity(Label = "Developer")]
    public class DeveloperMenuActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.DeveloperMenuView);

            ScrollView scrollViewDeveloperMenu = FindViewById<ScrollView>(Resource.Id.scrollViewDeveloperMenu);
            scrollViewDeveloperMenu.Focusable = false;
            scrollViewDeveloperMenu.FocusableInTouchMode = false;

            EditText editTextDeveloperMenuDeviceToken = FindViewById<EditText>(Resource.Id.editTextDeveloperMenuDeviceToken);
            editTextDeveloperMenuDeviceToken.Text = Joyces.Helpers.Settings.PushDeviceToken;

            EditText editTextDeveloperMenuBundleId = FindViewById<EditText>(Resource.Id.editTextDeveloperMenuBundleId);
            editTextDeveloperMenuBundleId.Text = Application.Context.PackageName;

            //Testar ifall WSO2 är up-and-running
            CheckWSO2ServerStatus();

            //Testar ifall REST är up-and-running
            CheckRESTServerStatus();
        }


        async private void CheckWSO2ServerStatus()
        {
            EditText editTextDeveloperMenuStatusWSO2 = FindViewById<EditText>(Resource.Id.editTextDeveloperMenuStatusWSO2);

            var appTokenModel = await RestAPI.GetApplicationToken();

            if (appTokenModel != null)
            {
                editTextDeveloperMenuStatusWSO2.Text = "Online";
                editTextDeveloperMenuStatusWSO2.SetTextColor(Android.Graphics.Color.DarkGreen);
            }
            else
            {
                editTextDeveloperMenuStatusWSO2.Text = "Offline";
                editTextDeveloperMenuStatusWSO2.SetTextColor(Android.Graphics.Color.DarkRed);
            }
        }

        async private void CheckRESTServerStatus()
        {
            EditText editTextDeveloperMenuStatusREST = FindViewById<EditText>(Resource.Id.editTextDeveloperMenuStatusREST);

            var tokenModel = await RestAPI.GetUserToken(GeneralSettings.TestUserUsername, GeneralSettings.TestUserPassword);

            var getCustomer = await RestAPI.GetCustomer(GeneralSettings.TestUserUsername, ((TokenModel)tokenModel).access_token);

            if (tokenModel != null && getCustomer is Customer)
            {
                editTextDeveloperMenuStatusREST.Text = "Online";
                editTextDeveloperMenuStatusREST.SetTextColor(Android.Graphics.Color.DarkGreen);
            }
            else if (tokenModel != null && tokenModel is AbalonErrors)
            {
                editTextDeveloperMenuStatusREST.Text = "Offline: " + ((AbalonErrors)tokenModel).message;
                editTextDeveloperMenuStatusREST.SetTextColor(Android.Graphics.Color.DarkRed);
            }
            else
            {
                editTextDeveloperMenuStatusREST.Text = "Offline";
                editTextDeveloperMenuStatusREST.SetTextColor(Android.Graphics.Color.DarkRed);
            }
        }
    }
}