using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Net;
using Android.Gms.Common;
using Android.Graphics;

using Joyces.Droid.Model;

using Newtonsoft.Json;


namespace Joyces.Droid
{
    [Activity(Label = "Paradiset", MainLauncher = true, ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class MainActivity : Activity
    {
        private Button loginButton;
        private Button registerButton;
        private Button forgotButton;
        private EditText EmailEditText;
        private EditText PasswordEditText;
        private Button buttonwMainViewVersion;

        ProgressDialog progress;
        Activity activity;

        int iSecretButtonClickCounter = 0;

        private void setCurrentClientTheme()
        {
            try
            {
                Typeface tf = Typeface.CreateFromAsset(Assets, Joyces.Helpers.Settings.MainFont);
               

                ScrollView mainViewBackground = FindViewById<ScrollView>(Resource.Id.mainViewBackground);
                mainViewBackground.SetBackgroundColor(Android.Graphics.Color.ParseColor(GeneralSettings.BackgroundColor));

                Button forgotButton = FindViewById<Button>(Resource.Id.ForgotButton);
                forgotButton.SetBackgroundColor(Android.Graphics.Color.ParseColor(GeneralSettings.ButtonBackgroundColor));
                forgotButton.SetTextColor(Android.Graphics.Color.ParseColor(GeneralSettings.ButtonTextColor));
                forgotButton.SetTypeface(tf, Android.Graphics.TypefaceStyle.Normal);
                forgotButton.Text = Lang.FORGOT_PASSWORD;

                Button buttonwMainViewVersion = FindViewById<Button>(Resource.Id.buttonwMainViewVersion);
                buttonwMainViewVersion.SetBackgroundColor(Android.Graphics.Color.ParseColor(GeneralSettings.BackgroundColor));
                buttonwMainViewVersion.SetTypeface(tf, Android.Graphics.TypefaceStyle.Normal);

                Button loginButton = FindViewById<Button>(Resource.Id.loginButton);
                loginButton.SetBackgroundColor(Android.Graphics.Color.ParseColor(GeneralSettings.ButtonBackgroundColor));
                loginButton.SetTextColor(Android.Graphics.Color.ParseColor(GeneralSettings.ButtonTextColor));
                loginButton.SetTypeface(tf, Android.Graphics.TypefaceStyle.Normal);
                loginButton.Text = Lang.LOGIN;

                Button registerButton = FindViewById<Button>(Resource.Id.RegisterButton);
                registerButton.SetBackgroundColor(Android.Graphics.Color.ParseColor(GeneralSettings.ButtonBackgroundColor));
                registerButton.SetTextColor(Android.Graphics.Color.ParseColor(GeneralSettings.ButtonTextColor));
                registerButton.SetTypeface(tf, Android.Graphics.TypefaceStyle.Normal);
                registerButton.Text = Lang.CREATE_ACCOUNT;

                EditText editTextEmail = FindViewById<EditText>(Resource.Id.EmailEditText);
                editTextEmail.SetTypeface(tf, Android.Graphics.TypefaceStyle.Normal);
                editTextEmail.Hint = Lang.EMAIL;

                EditText editTextPassword = FindViewById<EditText>(Resource.Id.PasswordEditText);
                editTextPassword.SetTypeface(tf, Android.Graphics.TypefaceStyle.Normal);
                editTextPassword.Hint = Lang.PASSWORD;

                //Retrieving the local Resource ID from the name
                string sLogo = GeneralSettings.LogoLogin.Replace("Images/", string.Empty);
                sLogo = sLogo.Replace(".png", string.Empty);
                sLogo = sLogo.Replace(".jpg", string.Empty);
                sLogo = sLogo.Replace(".jpeg", string.Empty);
                sLogo = sLogo.Replace(".gif", string.Empty);

                int id = (int)typeof(Resource.Drawable).GetField(sLogo).GetValue(null);
                ImageView mainViewLogo = FindViewById<ImageView>(Resource.Id.mainViewLogo);
                mainViewLogo.SetImageResource(id);
            }
            catch (Exception ex)
            {
            }
        }

        public override void OnBackPressed()
        {
            // This prevents a user from being able to hit the back button and leave the login page.
            return;
        }

        async protected override void OnCreate(Bundle bundle)
        {
            try
            {
                base.OnCreate(bundle);

                RequestWindowFeature(WindowFeatures.NoTitle);
                Platform.PlatformAndroid.InitAPIs(this);

                AndroidSettings.SetGeneralSetting();

                progress = new ProgressDialog(this);
                progress.Indeterminate = true;
                progress.SetProgressStyle(ProgressDialogStyle.Spinner);
                progress.SetMessage(Lang.LOADING);
                progress.SetCancelable(false);

                //string sUserToken = Helpers.Settings.AccessToken;
                string sUserEmail = Joyces.Helpers.Settings.UserEmail;

                //Skicka användaren vidare till inloggatläge
                if (GeneralSettings.AutoLogin && !string.IsNullOrEmpty(Joyces.Helpers.Settings.AccessToken) && !string.IsNullOrEmpty(sUserEmail))
                {
                    var t = Task.Run(async () => await LoadApp());
                }
                else
                {
                    ShowLoginView();
                }
            }
            catch (Exception ex)
            {
                SetContentView(Resource.Layout.Main);
                buttonwMainViewVersion.Text = buttonwMainViewVersion.Text + "_err";
                setCurrentClientTheme();
            }
        }
        
        private void ShowLoginView()
        {
            SetContentView(Resource.Layout.MainParadiset);

            loginButton = FindViewById<Button>(Resource.Id.loginButton);
            registerButton = FindViewById<Button>(Resource.Id.RegisterButton);
            forgotButton = FindViewById<Button>(Resource.Id.ForgotButton);

            buttonwMainViewVersion = FindViewById<Button>(Resource.Id.buttonwMainViewVersion);
            
            #if DEBUG
                buttonwMainViewVersion.Text = "Version " + AndroidHelper.GetAppVersion() + "_dev";
            #else
                buttonwMainViewVersion.Text = "Version " + AndroidHelper.GetAppVersion();
            #endif

            //Anropar GCM Service
            if (IsPlayServicesAvailable())
            {
                var intent = new Intent(this, typeof(RegistrationIntentService));
                StartService(intent);
            }

            HandleEvents();

            setCurrentClientTheme();
        }

        private void CheckConnection()
        {
            var cm = (ConnectivityManager)GetSystemService(ConnectivityService);
            bool bIsOnline = cm.ActiveNetworkInfo == null ? false : cm.ActiveNetworkInfo.IsConnected;

            if (!bIsOnline)
            {
                AlertDialog.Builder builder = new AlertDialog.Builder(this);
                AlertDialog ad = builder.Create();
                ad.SetTitle(Lang.ERROR_HEADLINE);
                ad.SetIcon(Android.Resource.Drawable.IcDialogAlert);
                ad.SetMessage(Lang.INTERNET_CONNECTION_REQUIERED);
                ad.SetButton(Lang.BUTTON_OK_I_UNDERSTAND, (s, e) => { CheckConnection(); });
                ad.Show();
            }
        }

        private void Alert(string sHeadline, string sMessage, string sOkButtonText)
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(this);
            AlertDialog ad = builder.Create();
            ad.SetTitle(sHeadline);
            ad.SetIcon(Android.Resource.Drawable.IcDialogAlert);
            ad.SetMessage(sMessage);
            ad.SetButton(sOkButtonText, (s, e) => { Console.WriteLine("OK Button clicked, alert dismissed"); });
            ad.Show();
        }

        public bool IsPlayServicesAvailable()
        {
            int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
            if (resultCode != ConnectionResult.Success)
            {
                if (GoogleApiAvailability.Instance.IsUserResolvableError(resultCode))
                    Alert(Lang.ERROR_HEADLINE, GoogleApiAvailability.Instance.GetErrorString(resultCode), Lang.BUTTON_OK);
                else
                {
#if DEBUG
                    Alert(Lang.ERROR_HEADLINE, Lang.DEVICE_NOT_SUPPORTED, Lang.BUTTON_OK);
#endif
                    Finish();
                }
                return false;
            }
            else
            {
                return true;
            }
        }

        private void HandleEvents()
        {
            loginButton.Click += LoginButton_Click;
            registerButton.Click += RegisterButton_Click;
            forgotButton.Click += ForgotButton_Click;
            buttonwMainViewVersion.Click += DeveloperMenu_Click;

        }

        private void DeveloperMenu_Click(object sender, EventArgs e)
        {
            iSecretButtonClickCounter++;

            if (iSecretButtonClickCounter > 5)
            {
                var intent = new Intent(this, typeof(DeveloperMenuActivity));
                StartActivity(intent);
            }
        }

        private void RegisterButton_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(RegisterActivity));
            StartActivity(intent);
        }

        private void ForgotButton_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(ForgotActivity));
            StartActivity(intent);
        }

        async private void LoginButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Login button clicked");
            progress.Show();

            Console.WriteLine("Progress Show exited");

            EditText txtEmail = FindViewById<EditText>(Resource.Id.EmailEditText);
            string sUsername = txtEmail.Text;

            EditText txtPassword = FindViewById<EditText>(Resource.Id.PasswordEditText);
            string sPassword = txtPassword.Text;

            var tokenModel = await RestAPI.GetUserToken(sUsername, sPassword);

            if (tokenModel != null && tokenModel is TokenModel)
            {
                //Om allt går som det ska, så ska den komma in hit!
                Joyces.Helpers.Settings.AccessToken = ((TokenModel)tokenModel).access_token;
                Joyces.Helpers.Settings.UserEmail = sUsername;
                if (((TokenModel)tokenModel).expires_in!=null)
                    Joyces.Helpers.Settings.AccessTokenExpiration = ((TokenModel)tokenModel).expires_in.ToString();

                Joyces.Platform.AppContext.Instance.Platform.CustomerId = sUsername;
                var getCustomer = await RestAPI.GetCustomer(sUsername, ((TokenModel)tokenModel).access_token);

                if (getCustomer == null)
                {
                    Joyces.Helpers.Settings.AccessToken = string.Empty;
                    Joyces.Helpers.Settings.UserEmail = string.Empty;
                    Joyces.Helpers.Settings.AccessTokenExpiration = string.Empty;
                    Alert(Lang.MESSAGE_HEADLINE, Lang.UNEXPECTED_ERROR, Lang.BUTTON_OK);

                    progress.Hide();

                    return;
                }
                else if (getCustomer is Customer)
                {
                    //Always set new token here.
                    //((TokenModel)tokenModel).access_token = Helpers.Settings.AccessToken;
                   // Task.Run(async () => await LoadApp());

                    Joyces.Platform.AppContext.Instance.Platform.CustomerList = (Customer)getCustomer;
                    if (Joyces.Platform.AppContext.Instance.Platform.CustomerList != null)
                        await SetCustomerSetting();

                    var resp = await RestAPI.GetOffer(sUsername, Joyces.Helpers.Settings.AccessToken);

                    if (resp != null && resp is List<Offer>)
                    {
                        Joyces.Platform.AppContext.Instance.Platform.OfferList = (List<Offer>)resp;
                        await SetOfferSetting();
                    }
                    else
                        Joyces.Platform.AppContext.Instance.Platform.OfferList = null;

                    resp = await RestAPI.GetNews(sUsername, Joyces.Helpers.Settings.AccessToken);

                    if (resp != null && resp is List<News>)
                    {
                        Joyces.Platform.AppContext.Instance.Platform.NewsList = (List<News>)resp;
                        await SetNewsSetting();
                    }
                    else
                        Joyces.Platform.AppContext.Instance.Platform.NewsList = null;

                    Joyces.Platform.AppContext.Instance.Platform.MoreList = await RestAPI.GetMore(Joyces.Helpers.Settings.AccessToken);
                    if (Joyces.Platform.AppContext.Instance.Platform.MoreList != null)
                        await SetMoreSetting();
                    Joyces.Helpers.Settings.UserAccountNo = Joyces.Platform.AppContext.Instance.Platform.CustomerList.accountNumber;

                    progress.Hide();

                    var t = Task.Run(async () => await LoadApp());
                   
                }
                else if (getCustomer is AbalonErrors)
                {
                    var localError = (AbalonErrors)getCustomer;

                    Joyces.Helpers.Settings.AccessToken = string.Empty;
                    Joyces.Helpers.Settings.UserEmail = string.Empty;

                    Alert(Lang.ERROR_HEADLINE, localError.message, Lang.BUTTON_OK);

                    progress.Hide();
                }
            }
            else if (tokenModel != null && tokenModel is AbalonErrors)
            {
                //Hit kommer den om ett fel returneras från Abalon
                progress.Hide();

                Joyces.Helpers.Settings.AccessToken = string.Empty;
                Joyces.Helpers.Settings.UserEmail = string.Empty;

                if (((AbalonErrors)tokenModel).error_description.Contains("PRE_AUTHENTICATION"))
                    ((AbalonErrors)tokenModel).error_description = Lang.NOT_ACTIVATED_MEMBER;

                Alert(Lang.ERROR_HEADLINE, ((AbalonErrors)tokenModel).error_description.Replace("@carbon.super", ""), Lang.BUTTON_OK);
            }
            else
            {
                //Vid all sorts exception kommer den hit och visar UNEXPECTED_ERROR! 
                progress.Hide();

                Joyces.Helpers.Settings.AccessToken = string.Empty;
                Joyces.Helpers.Settings.UserEmail = string.Empty;

                Alert(Lang.ERROR_HEADLINE, Lang.WRONG_PASSWORD, Lang.BUTTON_OK);
            }
        }

        private async Task SetMoreSetting()
        {
            try
            {
                List<More> moreList = Joyces.Platform.AppContext.Instance.Platform.MoreList;
                string strMoreAsJson = JsonConvert.SerializeObject(moreList);
                Joyces.Helpers.Settings.MoreJson = strMoreAsJson;
            }
            catch (Exception e)
            {
            }
        }

        private async Task SetOfferSetting()
        {
            try
            {
                List<Offer> offerList = Joyces.Platform.AppContext.Instance.Platform.OfferList;
                string strOfferAsJson = JsonConvert.SerializeObject(offerList);
                Joyces.Helpers.Settings.OfferJson = strOfferAsJson;
            }
            catch (Exception e) { }
        }

        private async Task SetNewsSetting()
        {
            try
            {
                List<News> newsList = Joyces.Platform.AppContext.Instance.Platform.NewsList;
                string strNewsAsJson = JsonConvert.SerializeObject(newsList);
                Joyces.Helpers.Settings.NewsJson = strNewsAsJson;
            }
            catch (Exception e) { }
        }

        private async Task SetCustomerSetting()
        {
            try
            {
                Customer cust = Joyces.Platform.AppContext.Instance.Platform.CustomerList;
                string strCustomerJson = JsonConvert.SerializeObject(cust);
                Joyces.Helpers.Settings.CustomerJson = strCustomerJson;
            }
            catch (Exception e) { }
        }

        private async Task LoadApp()
        {
            try
            {
                //var t = Task.Run(async () => await loadValues());
                //t.Wait();

                var intent = new Intent(this, typeof(TabMenuActivity));
                DismissProgressbar();
                StartActivity(intent);

            }
            catch (Exception exc)
            {

            }
        }

        public void ShowProgressbar()
        {
            try
            {
                RunOnUiThread(() =>
                {
                    progress.Show();
                });

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void DismissProgressbar()
        {
            try
            {
                RunOnUiThread(() =>
                {
                    progress.Dismiss();
                });

            }
            catch (Exception e)
            {

            }
        }
    }
}