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
using Android.Support.V4.View;
using TreePager;
using System.Threading.Tasks;
using Joyces.Droid.Model;
using Android.Graphics.Drawables;
using System.Threading;
using Android.Graphics;
using Android.Text;
using Calligraphy;



namespace Joyces.Droid
{

    //[Activity(Label = "Joyces", Theme = "@style/CustomActionBarTheme")]
    [Activity(Label = "Paradiset", Theme = "@style/CustomActionBarTheme" ,ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class TabMenuActivity : Activity
    {
        string sCustomerId = Joyces.Platform.AppContext.Instance.Platform.CustomerId;

        bool bIsUpdating = false;
        ProgressDialog progress;

        private Button btnUpdate;
        private Button btnLogout;

        string sUserAccountNumber;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            sUserAccountNumber = Joyces.Helpers.Settings.UserAccountNo;

            base.OnCreate(savedInstanceState);

            progress = new ProgressDialog(this);
            progress.Indeterminate = true;
            progress.SetProgressStyle(ProgressDialogStyle.Spinner);
            progress.SetMessage(Lang.CONTACTING_SERVER_WAIT);
            progress.SetCancelable(false);

            //Kontrollerar ifall kunden är inloggat
            if (string.IsNullOrEmpty(sCustomerId))
            {
                var t = Task.Run(() => goToLogin());
                t.Wait();
            }

            var t1 = Task.Run(async () => await LoadApp());
            t1.Wait();

            string sDeviceToken = Joyces.Helpers.Settings.PushDeviceToken;

            if (!string.IsNullOrEmpty(sDeviceToken))
                SendRegistrationToAppServer(sDeviceToken);
           // SetActionbarFont();
        }
        //TESTA FONT
        String ExternalFontPath;
        Typeface FontLoaderTypeface;
        private void SetActionbarFont()
        {
            try
            {
                //CalligraphyConfig.initDefault(new CalligraphyConfig.Builder()
                //        .setDefaultFontPath(Joyces.Helpers.Settings.MainFont)
                //        .build());

                // "fonts/Roboto-RobotoRegular.ttf"
                try
                {
                    Typeface tf = Typeface.CreateFromAsset(Assets, Joyces.Helpers.Settings.MainFont);

                    SpannableString st = new SpannableString(Lang.APPLICATION_NAME);
                                                                          //st.SetSpan(new TypefaceSpan(this, "Signika-Regular.otf"), 0, st.Length(), SpanTypes.ExclusiveExclusive);
                                                                          //string errorMessage = "God kväll";
                                                                          //SpannableString wordtoSpan = new SpannableString();
                                                                          //wordtoSpan.SetSpan(new ForegroundColorSpan(new Color(255, 124, 67, 149)), 34, errorMessage.Length, SpanTypes.ExclusiveExclusive);




                    st.SetSpan(tf, 0, st.Length(), SpanTypes.ExclusiveExclusive);

                    ActionBar.TitleFormatted = st;
                }
                catch (Exception ee)
                {

                }


                //ActionBar actionBar = ActionBar;
                //TextView TextViewNewFont = new TextView(this);
                //ViewGroup.LayoutParams layoutparams = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent);
                //TextViewNewFont.LayoutParameters = layoutparams;
                //TextViewNewFont.Text = "Actionbar Title";
                //TextViewNewFont.SetTextColor(Color.Red);
                //TextViewNewFont.Gravity = GravityFlags.Center;
                //TextViewNewFont.SetTextSize(Android.Util.ComplexUnitType.Sp, 27);
                //ExternalFontPath = "Montserrat-Regulart.ttf";

                //FontLoaderTypeface = Typeface.CreateFromAsset(Assets, Joyces.Helpers.Settings.MainFont); //Typeface.CreateFromAsset(Assets, ExternalFontPath);
                //TextViewNewFont.SetTypeface(FontLoaderTypeface, TypefaceStyle.Normal);
                //// actionBar.SetCustomView(TextViewNewFont);
                //ActionBar.SetCustomView(TextViewNewFont, new Android.App.ActionBar.LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent));

            }
            catch (Exception e)
            {

            }
        }


        async public void SendRegistrationToAppServer(string sDeviceToken)
        {
            //Skicka en token tillsammans uniqe UUID till Relevate
            try
            {
                string sUserToken = Joyces.Helpers.Settings.AccessToken;

                if (!string.IsNullOrEmpty(sUserToken))
                {
                    string sUUID = Android.Provider.Settings.Secure.GetString(ContentResolver, Android.Provider.Settings.Secure.AndroidId);
                    string sSystemOSVersion = System.Environment.OSVersion.Version.ToString();
                    string sAppID = Application.Context.PackageName + ".android";
                    string sAppVersion = AndroidHelper.GetAppVersion();
                    string sCustomerName = Joyces.Platform.AppContext.Instance.Platform.CustomerList.name;
                    string sCustomerId = Joyces.Platform.AppContext.Instance.Platform.CustomerList.accountNumber;
                    string sPlatform = "Android";

                    var result = await RestAPI.PostDeviceInformation(sAppID, sAppVersion, sSystemOSVersion, sUUID, sCustomerName, sPlatform, sDeviceToken, sUserToken, sCustomerId);
                }
            }
            catch (Exception ex)
            { }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            ActionBar.RemoveTabAt(4);

            MenuInflater.Inflate(Resource.Menu.ToolbarMenu, menu);
            return base.OnCreateOptionsMenu(menu);
        }


        //private void changeTabsFont()
        //{

        //    ViewGroup vg = (ViewGroup)tabLayout.getChildAt(0);
        //    int tabsCount = vg.getChildCount();
        //    for (int j = 0; j < tabsCount; j++)
        //    {
        //        ViewGroup vgTab = (ViewGroup)vg.getChildAt(j);
        //        int tabChildsCount = vgTab.getChildCount();
        //        for (int i = 0; i < tabChildsCount; i++)
        //        {
        //            View tabViewChild = vgTab.getChildAt(i);
        //            if (tabViewChild instanceof TextView) {
        //        ((TextView)tabViewChild).setTypeface(Font.getInstance().getTypeFace(), Typeface.NORMAL);
        //    }
        //}


        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.menu_profile:
                    SetContentView(Resource.Layout.ProfileView);
                    loadProfileViewContent(true);

                    return true;
                default:
                    return false;
            }


            // return base.OnOptionsItemSelected(item);
        }

        string sSelectedTab = string.Empty;
        bool mbProfileSelected = false;

        private void LoadTabs()
        {

            ActionBar.Tab tab = ActionBar.NewTab();
            ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;
            
            ActionBar.SetBackgroundDrawable(new ColorDrawable(Android.Graphics.Color.ParseColor(GeneralSettings.AndroidActionBarColor)));
            
            //tab.SetText("News");
            tab.SetText("Nyheter");

           

            tab.SetIcon(null);
            tab.TabSelected += async (sender, args) =>
            {
                sSelectedTab = "news";
                SetContentView(Resource.Layout.NewsfeedView);
                await LoadNewsfeedView();
            };
            ActionBar.AddTab(tab);

            tab = ActionBar.NewTab();
            tab.SetText("Din kod");
            tab.SetIcon(null);
            tab.TabSelected += (sender, args) =>
            {
                sSelectedTab = "id";
                SetContentView(Resource.Layout.IdView);
                LoadIdView();
            };
            ActionBar.AddTab(tab);

            tab = ActionBar.NewTab();
            tab.SetText("Erbjudanden");
            tab.SetIcon(null);
            tab.TabSelected += (sender, args) =>
            {
                if (GeneralSettings.UseLoyaltyCard)
                {
                    sSelectedTab = "offersWithLoyalty";
                    SetContentView(Resource.Layout.OffersView);
                    LoadOffersView();
                }
                else
                {
                    sSelectedTab = "offersWithoutLoyalty";
                    SetContentView(Resource.Layout.OfferSingleView);
                    LoadOffersSingleView();
                }

            };
            ActionBar.AddTab(tab);

            tab = ActionBar.NewTab();
            tab.SetText("Övrigt");
            tab.SetIcon(null);
            tab.TabSelected += (sender, args) =>
            {
                sSelectedTab = "more";
                SetContentView(Resource.Layout.MoreView);
                LoadMoreFeed();
            };
            ActionBar.AddTab(tab);

            tab = ActionBar.NewTab();
            tab.SetText("Profil");
            tab.SetIcon(null);
            tab.TabSelected += (sender, args) =>
            {
                sSelectedTab = "profile";
                SetContentView(Resource.Layout.ProfileView);
                loadProfileViewContent(true);
            };
            ActionBar.AddTab(tab);

            ActionBar.SetSelectedNavigationItem(1);
           // SetActionbarFont();
        }
        
        async private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!bIsUpdating)
            {
                bIsUpdating = true;
                progress.Show();

                string sEmail = FindViewById<EditText>(Resource.Id.txtProfileViewEmail).Text;
                string sFirstName = FindViewById<EditText>(Resource.Id.txtProfileViewFirstName).Text;
                string sLastName = FindViewById<EditText>(Resource.Id.txtProfileViewLastName).Text;
                string sMobileNo = FindViewById<EditText>(Resource.Id.txtProfileViewMobileNo).Text;

                string sUserToken = Joyces.Helpers.Settings.AccessToken;

                var sResp = await RestAPI.UpdateUser(sEmail, sFirstName, sLastName, sMobileNo, sUserToken);

                if (string.IsNullOrEmpty(sResp))
                {
                    var getCustomer = await RestAPI.GetCustomer(sEmail, sUserToken);

                    if (getCustomer == null)
                    {
                        Alert(Lang.MESSAGE_HEADLINE, Lang.UNEXPECTED_ERROR, Lang.BUTTON_OK);
                    }
                    else if (getCustomer is Customer)
                    {
                        Joyces.Platform.AppContext.Instance.Platform.CustomerList = (Customer)getCustomer;
                        Alert(Lang.MESSAGE_HEADLINE, Lang.PROFILE_UPDATE, Lang.BUTTON_OK);
                    }
                    else if (getCustomer is AbalonErrors)
                    {
                        var localError = (AbalonErrors)getCustomer;

                        if (localError.error.Contains("invalid_token") || localError.error.Contains("invalid_grant"))
                        {
                            Joyces.Helpers.Settings.AccessToken = string.Empty;
                            Joyces.Helpers.Settings.UserEmail = string.Empty;

                            //Skickar användaren till inloggningssidan
                            var intent = new Intent(this, typeof(MainActivity));
                            StartActivity(intent);

                            Alert(Lang.MESSAGE_HEADLINE, Lang.USER_HAS_LOGGED_OUT, Lang.BUTTON_OK);
                        }
                        else
                        {
                            Alert(Lang.MESSAGE_HEADLINE, ((AbalonErrors)getCustomer).error_description, Lang.BUTTON_OK);
                        }


                    }

                    progress.Hide();
                }
                else
                    Alert(Lang.MESSAGE_HEADLINE, sResp, Lang.BUTTON_OK);

                loadProfileViewContent(true);

                progress.Hide();
                bIsUpdating = false;
            }
        }

        async private void btnLogout_Click(object sender, EventArgs e)
        {
            Joyces.Helpers.Settings.AccessToken = string.Empty;
            Joyces.Helpers.Settings.UserEmail = string.Empty;
            Joyces.Helpers.Settings.UserAccountNo = string.Empty;

            //Skickar användaren till inloggningssidan
            var intent = new Intent(this, typeof(MainActivity));
            StartActivity(intent);
        }

        private void goToLogin()
        {
            SetContentView(Resource.Layout.Main);
        }

        private async Task LoadApp()
        {
            try
            {
                LoadTabs();
            }
            catch (Exception exc)
            {

            }
        }

        private async Task LoadAllFeeds()
        {
            try
            {
                //   LoadNewsfeedView();
                //  LoadOffersView();
            }
            catch (Exception e)
            {

            }
        }


        


        private void loadProfileViewContent(bool reload)
        {
            try
            {
                
                var customerList = Joyces.Platform.AppContext.Instance.Platform.CustomerList;
                Typeface tf = Typeface.CreateFromAsset(Assets, Joyces.Helpers.Settings.MainFont);

                //mbProfileSelected = true;
                if (customerList != null)
                {
                    

                    EditText txtProfileViewEmail = FindViewById<EditText>(Resource.Id.txtProfileViewEmail);
                    txtProfileViewEmail.Text = customerList.email;
                    txtProfileViewEmail.SetTypeface(tf, Android.Graphics.TypefaceStyle.Normal);

                    EditText txtProfileViewFirstName = FindViewById<EditText>(Resource.Id.txtProfileViewFirstName);
                    txtProfileViewFirstName.Text = customerList.firstName;
                    txtProfileViewFirstName.SetTypeface(tf, Android.Graphics.TypefaceStyle.Normal);
                    txtProfileViewFirstName.Enabled = false;

                    EditText txtProfileViewLastName = FindViewById<EditText>(Resource.Id.txtProfileViewLastName);
                    txtProfileViewLastName.Text = customerList.lastName;
                    txtProfileViewLastName.SetTypeface(tf, Android.Graphics.TypefaceStyle.Normal);
                    txtProfileViewLastName.Enabled = false;

                    EditText txtProfileViewMobileNo = FindViewById<EditText>(Resource.Id.txtProfileViewMobileNo);
                    txtProfileViewMobileNo.Text = customerList.mobile;
                    txtProfileViewMobileNo.SetTypeface(tf, Android.Graphics.TypefaceStyle.Normal);

                    TextView txtEmail = FindViewById<TextView>(Resource.Id.textViewProfileViewEmail);
                    txtEmail.SetTypeface(tf, Android.Graphics.TypefaceStyle.Normal);
                    txtEmail.Text = Lang.EMAIL;

                    TextView txtHeader = FindViewById<TextView>(Resource.Id.textViewHeadline);
                    txtHeader.SetTypeface(tf, Android.Graphics.TypefaceStyle.Normal);
                    txtHeader.Text = Lang.ACCOUNT_PERSONAL_DETAILS;

                    TextView txtMobile = FindViewById<TextView>(Resource.Id.textViewProfileViewMobileNo);
                    txtMobile.SetTypeface(tf, Android.Graphics.TypefaceStyle.Normal);
                    txtMobile.Text = Lang.MOBILEPHONE;

                    TextView txtFirstName = FindViewById<TextView>(Resource.Id.textViewProfileViewFirstname);
                    txtFirstName.SetTypeface(tf, Android.Graphics.TypefaceStyle.Normal);
                    txtFirstName.Text = Lang.EDIT_ACCOUNT_FIRSTNAME;

                    TextView txtLastName = FindViewById<TextView>(Resource.Id.textViewProfileViewLastname);
                    txtLastName.SetTypeface(tf, Android.Graphics.TypefaceStyle.Normal);
                    txtLastName.Text = Lang.EDIT_ACCOUNT_FIRSTNAME;

                    btnUpdate = FindViewById<Button>(Resource.Id.btnProfileViewUpdate);
                    btnUpdate.SetTypeface(tf, Android.Graphics.TypefaceStyle.Normal);
                    btnUpdate.Text = Lang.SAVE;
                    btnUpdate.Click += btnUpdate_Click;

                    btnLogout = FindViewById<Button>(Resource.Id.btnProfileViewLogout);
                    btnLogout.SetTypeface(tf, Android.Graphics.TypefaceStyle.Normal);
                    btnLogout.Text = Lang.LOGOUT;
                    btnLogout.Click += btnLogout_Click;

                    btnUpdate.SetBackgroundColor(Android.Graphics.Color.ParseColor(GeneralSettings.ButtonBackgroundColor));
                    btnUpdate.SetTextColor(Android.Graphics.Color.ParseColor(GeneralSettings.ButtonTextColor));

                   
                }
                else
                {
                    //Alert(Lang.MESSAGE_HEADLINE, Lang.UNEXPECTED_ERROR, Lang.BUTTON_OK);
                    if (reload)
                    {
                        progress.Show();
                        Task.Run(async () =>
                        {
                            reloadCustomerDetails();

                        });
                    }
                    else if (progress.IsShowing)
                    {
                        progress.Hide();
                    }
                }
                
                //FindViewById<EditText>(Resource.Id.txtProfileViewMobileNo).Hint = GeneralSettings.TelephoneNoMasking;
            }
            catch (Exception ex)
            {

            }
        }

        private async Task reloadCustomerDetails()
        {
            try
            {
                for(int i = 0; i < 16; i++)
                {
                    if (Joyces.Platform.AppContext.Instance.Platform.CustomerList != null)
                        break;
                    Thread.Sleep(500);
                  //  Thread.sleep(500);
                }
                if (Joyces.Platform.AppContext.Instance.Platform.CustomerList != null)
                {
                    RunOnUiThread(() =>
                    {
                        loadProfileViewContent(false);
                        progress.Hide();
                    });
                }
                else
                {
                    RunOnUiThread(() =>
                    {
                        progress.Hide();
                    });
                }
            }catch(Exception e)
            {

            }
        }
        private ListView listviewMoreGlobal;

        private async void SetMoreListAdapter()
        {
            listviewMoreGlobal = new ListView(this);// FindViewById<ListView>(Resource.Id.MoreViewList);
            CustomListViewMoreAdapter adapter = new CustomListViewMoreAdapter(this, Joyces.Platform.AppContext.Instance.Platform.MoreList, this);
            listviewMoreGlobal.Adapter = adapter;

        }

        private async void LoadMoreFeed()
        {
            ListView listViewMore = FindViewById<ListView>(Resource.Id.MoreViewList);

            if (Joyces.Platform.AppContext.Instance.Platform.MoreList != null)
            {
                CustomListViewMoreAdapter adapter = new CustomListViewMoreAdapter(this, Joyces.Platform.AppContext.Instance.Platform.MoreList, this);

                RunOnUiThread(() =>
                        {
                            if (adapter != null)
                                listViewMore.Adapter = adapter;

                            listViewMore.ItemClick += listViewMore_ItemClick;
                        });




            }
            else
            {
                progress.Show();
                //Alert(Lang.MESSAGE_HEADLINE, Lang.SERVICE_NOT_AVAILABLE, Lang.BUTTON_OK, "Try again", "more");
                //Alert(Lang.MESSAGE_HEADLINE, Lang.SERVICE_NOT_AVAILABLE, Lang.BUTTON_OK);
            }
        }

        private void listViewMore_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var singleMoreItem = Joyces.Platform.AppContext.Instance.Platform.MoreList[e.Position];

            var uri = Android.Net.Uri.Parse(singleMoreItem.companyInfoUrl);
            var intent = new Intent(Intent.ActionView, uri);
            StartActivity(intent);
        }

        public void DismissProgressbar()
        {
            try
            {
                RunOnUiThread(() =>
                {
                    progress.Hide();
                });

            }
            catch (Exception e)
            {

            }
        }

        private async Task LoadNewsfeedView()
        {
            try
            {


                ListView listViewNewsfeed = FindViewById<ListView>(Resource.Id.listViewNewsfeed);


                string strerr = "";

                if (Joyces.Platform.AppContext.Instance.Platform.NewsList != null)
                {
                    try
                    {
                        CustomListViewNewsfeedAdapter adapter = new CustomListViewNewsfeedAdapter(this, Joyces.Platform.AppContext.Instance.Platform.NewsList, this);

                        RunOnUiThread(() =>
                        {
                            if (adapter != null)
                                listViewNewsfeed.Adapter = adapter;

                            listViewNewsfeed.ItemClick += ListViewNewsfeed_ItemClick;
                        });
                        progress.Hide();
                    }
                    catch (Exception ex)
                    {
                        strerr = ex.ToString();
                        Toast.MakeText(ApplicationContext, ex.Message, ToastLength.Long).Show();
                    }

                }
                else
                {
                    progress.Show();
                    //Alert(Lang.MESSAGE_HEADLINE, Lang.SERVICE_NOT_AVAILABLE, Lang.BUTTON_OK, "Try again", "news");
                    //Alert(Lang.MESSAGE_HEADLINE, Lang.SERVICE_NOT_AVAILABLE, Lang.BUTTON_OK);
                    //Toast.MakeText(ApplicationContext, "This service not available at this moment, please try again later.", ToastLength.Long).Show();
                }


            }
            catch (Exception ex)
            {

            }
        }

        private void ListViewNewsfeed_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            //newsList[e.Posision].Firstname;
            // SetContentView(Resource.Layout.NewsView);

            var singleNewsItem = Joyces.Platform.AppContext.Instance.Platform.NewsList[e.Position];

            var intent = new Intent(this, typeof(NewsActivity));
            intent.PutExtra("NewsActivity", JsonConvert.SerializeObject(singleNewsItem));
            StartActivity(intent);
        }

        public override void OnBackPressed()
        {
            // This prevents a user from being able to hit the back button and leave this page.
            return;

            //base.OnBackPressed();
        }

        async private void LoadIdView()
        {
            ImageView imgQRCode = FindViewById<ImageView>(Resource.Id.imageViewQRId);
            imgQRCode.SetImageBitmap(AndroidHelper.GetQrCode(sUserAccountNumber, 300, 300, 0));
            setCurrentClientTheme();
            await RefreshAllData();
        }
        private void setCurrentClientTheme()
        {
            try
            {
                Typeface tf = Typeface.CreateFromAsset(Assets, Joyces.Helpers.Settings.MainFont);
                TextView txtHeader = FindViewById<TextView>(Resource.Id.textView1);
                txtHeader.SetTypeface(tf, Android.Graphics.TypefaceStyle.Normal);
                txtHeader.Text = Lang.ID_HEADER;

                TextView txtInfo = FindViewById<TextView>(Resource.Id.textView2);
                txtInfo.SetTypeface(tf, Android.Graphics.TypefaceStyle.Normal);
                txtInfo.Text = Lang.ID_DESCRIPTION;

                TextView txtInfoOffer = FindViewById<TextView>(Resource.Id.textView3);
                txtInfoOffer.SetTypeface(tf, Android.Graphics.TypefaceStyle.Normal);
                txtInfoOffer.Text = Lang.ID_DESCRIPTION_NEWS;

                LinearLayout layoutBackCode = FindViewById<LinearLayout>(Resource.Id.idviewLinearBack);
                layoutBackCode.SetBackgroundColor(Android.Graphics.Color.ParseColor(GeneralSettings.BackgroundColor));


            }
            catch (Exception ex)
            {
            }
        }

        async private Task RefreshAllData()
        {
            //Refreashing News, Offer and More async
            try
            {
                string sUserToken = Joyces.Helpers.Settings.AccessToken;
                string sUserEmail = Joyces.Helpers.Settings.UserEmail;
                string strAccessTokenExp = Joyces.Helpers.Settings.AccessTokenExpiration;
                int iSecondsToExpiration = 0;
                if (!string.IsNullOrEmpty(strAccessTokenExp))
                {
                    Int32.TryParse(strAccessTokenExp, out iSecondsToExpiration);
                }
                await CheckValuesFromSettings();
                  //Int.TryParse(strAccessTokenExp, out iSecondsToExpiration);
                //System.Diagnostics.Debug.WriteLine("================START 1 ================ " + DateTime.Now.ToString("HH:mm:ss.fff"));
                //Skicka användaren vidare till inloggatläge
                if (iSecondsToExpiration < 10)
                {

                    if (!string.IsNullOrEmpty(sUserToken) && !string.IsNullOrEmpty(sUserEmail))
                    {
                        Joyces.Platform.AppContext.Instance.Platform.CustomerId = sUserEmail;


                        var getCustomer = await RestAPI.GetCustomer(sUserEmail, sUserToken);
         //               System.Diagnostics.Debug.WriteLine("================AFTER GET CUSTOMER================ " + DateTime.Now.ToString("HH:mm:ss.fff"));

                        if (getCustomer != null && getCustomer is Customer)
                        {
                            Joyces.Platform.AppContext.Instance.Platform.CustomerList = (Customer)getCustomer;
                            //var resp = await RestAPI.GetOffer(sUserEmail, sUserToken);

                            //if (resp != null && resp is List<Offer>)
                            //    Joyces.Platform.AppContext.Instance.Platform.OfferList = (List<Offer>)resp;
                            //else
                            //    Joyces.Platform.AppContext.Instance.Platform.OfferList = null;

                            //resp = await RestAPI.GetNews(sUserEmail, sUserToken);

                            //if (resp != null && resp is List<News>)
                            //    Joyces.Platform.AppContext.Instance.Platform.NewsList = (List<News>)resp;
                            //else
                            //    Joyces.Platform.AppContext.Instance.Platform.NewsList = null;

                            //Joyces.Platform.AppContext.Instance.Platform.MoreList = await RestAPI.GetMore(Helpers.Settings.AccessToken);
                            GetAllDataForTabs();

                        }
                        else if (getCustomer != null && getCustomer is AbalonErrors)
                        {
                            var localError = (AbalonErrors)getCustomer;

                            if (localError.error.Contains("invalid_token") || localError.error.Contains("invalid_grant"))
                            {
                                Joyces.Helpers.Settings.AccessToken = string.Empty;
                                Joyces.Helpers.Settings.UserEmail = string.Empty;
                                Joyces.Helpers.Settings.UserAccountNo = string.Empty;

                                Alert(Lang.MESSAGE_HEADLINE, Lang.USER_HAS_LOGGED_OUT, Lang.BUTTON_OK);


                                //Skickar användaren till inloggningssidan
                                var intent = new Intent(this, typeof(MainActivity));
                                StartActivity(intent);
                            }
                            else
                            {
                                Alert(Lang.MESSAGE_HEADLINE, localError.message, Lang.BUTTON_OK);
                            }
                        }
                        else
                        {
                            Alert(Lang.MESSAGE_HEADLINE, Lang.UNEXPECTED_ERROR, Lang.BUTTON_OK);
                        }
                    }
                }
                else
                {
                    //            System.Diagnostics.Debug.WriteLine("================ACCESS OK ================ " + DateTime.Now.ToString("HH:mm:ss.fff"));
                    Joyces.Platform.AppContext.Instance.Platform.CustomerId = sUserEmail;

                    GetAllDataForTabs();
                    GetCustomerOwnTaskAndRefreshAccessToken();
                }
         //       System.Diagnostics.Debug.WriteLine("================END 1 ================ " + DateTime.Now.ToString("HH:mm:ss.fff"));

                //if (sSelectedTab == "news")
                //    LoadNewsfeedView();
                //else if (sSelectedTab == "offersWithLoyalty")
                //    LoadOffersView();
                //else if (sSelectedTab == "offersWithoutLoyalty")
                //    LoadOffersSingleView();
                //else if (sSelectedTab == "more")
                //    LoadMoreFeed();

                //progress.Hide();
            }
            catch (Exception)
            {

            }
        }

        private void GetCustomerOwnTaskAndRefreshAccessToken()
        {
            try
            {
                string sUserToken = Joyces.Helpers.Settings.AccessToken;
                string sUserEmail = Joyces.Helpers.Settings.UserEmail;
                Task.Run(async () =>
                {

                var getCustomer = await RestAPI.GetCustomer(sUserEmail, sUserToken);
                    //               System.Diagnostics.Debug.WriteLine("================AFTER GET CUSTOMER================ " + DateTime.Now.ToString("HH:mm:ss.fff"));

                    if (getCustomer != null && getCustomer is Customer)
                    {
                        Joyces.Platform.AppContext.Instance.Platform.CustomerList = (Customer)getCustomer;
                        await SetCustomerSetting();
                    }
                   

                });
                Task.Run(async () =>
                {
                    Thread.Sleep(TimeSpan.FromSeconds(15));
               //     System.Diagnostics.Debug.WriteLine("================REFRESHTOKEN START================ " + DateTime.Now.ToString("HH:mm:ss.fff"));
                    bool bRefreshed = await RestAPI.RefreshTokenInBackground(sUserEmail);
                    string strRefreshed = "";
                    if (bRefreshed)
                        strRefreshed = "true";
                    else
                        strRefreshed = "false";
               //     System.Diagnostics.Debug.WriteLine("================REFRESHTOKEN END ================ " + DateTime.Now.ToString("HH:mm:ss.fff"));


                });

                //HERE REFRESH TOKEN
            }
            catch(Exception e)
            {

            }
        }
         private void GetAllDataForTabs()
        {
            try
            {
        //        System.Diagnostics.Debug.WriteLine("================START 2 ================ " + DateTime.Now.ToString("HH:mm:ss.fff"));

                string sUserToken = Joyces.Helpers.Settings.AccessToken;
                string sUserEmail = Joyces.Helpers.Settings.UserEmail;
                Task.Run(async () =>
                {
        //            System.Diagnostics.Debug.WriteLine("================OFFERS START ================ " + DateTime.Now.ToString("HH:mm:ss.fff"));
                    var resp = await RestAPI.GetOffer(sUserEmail, sUserToken);
                    //            System.Diagnostics.Debug.WriteLine("================OFFERS END ================ " + DateTime.Now.ToString("HH:mm:ss.fff"));
                    bool bOfferIsNotTheSame = false;
                    if (resp != null && resp is List<Offer>)
                    {
                        string strJsonFromRest = JsonConvert.SerializeObject((List<Offer>)resp);
                        if (strJsonFromRest != Joyces.Helpers.Settings.OfferJson)
                        {
                            Joyces.Platform.AppContext.Instance.Platform.OfferList = (List<Offer>)resp;
                            bOfferIsNotTheSame = true;
                        }
                        await SetOfferSetting();
                    }
                    else
                        Joyces.Platform.AppContext.Instance.Platform.OfferList = null;
                    if (Joyces.Platform.AppContext.Instance.Platform.OfferList != null && bOfferIsNotTheSame)
                    {
                        if (sSelectedTab == "offersWithLoyalty")
                        {
                            RunOnUiThread(() =>
                            {
                                LoadOffersView();
                            });
                        }
                        else if (sSelectedTab == "offersWithoutLoyalty")
                        {
                            RunOnUiThread(() =>
                            {
                                LoadOffersSingleView();
                            });
                        }
                    }
                   
                });
                Task.Run(async () =>
                {
       //             System.Diagnostics.Debug.WriteLine("================NEWS START ================ " + DateTime.Now.ToString("HH:mm:ss.fff"));
                    var resp2 = await RestAPI.GetNews(sUserEmail, sUserToken);
                    //             System.Diagnostics.Debug.WriteLine("================NEWS END ================ " + DateTime.Now.ToString("HH:mm:ss.fff"));
                    bool bNewsIsNotTheSame = false;
                    if (resp2 != null && resp2 is List<News>)
                    {
                        string strJsonFromRest = JsonConvert.SerializeObject((List<News>)resp2);

                        if (strJsonFromRest != Joyces.Helpers.Settings.NewsJson)
                        {
                            Joyces.Platform.AppContext.Instance.Platform.NewsList = (List<News>)resp2;
                            bNewsIsNotTheSame = true;
                        }
                        await SetNewsSetting();
                    }
                    else
                        Joyces.Platform.AppContext.Instance.Platform.NewsList = null;
                    if(Joyces.Platform.AppContext.Instance.Platform.NewsList != null && bNewsIsNotTheSame)
                    {
                        if (sSelectedTab == "news")
                        {
                            RunOnUiThread(() =>
                        {
                            LoadNewsfeedView();
                        });
                        }
                }
                });
                Task.Run(async () =>
                {
                    //              System.Diagnostics.Debug.WriteLine("================MORE START ================ " + DateTime.Now.ToString("HH:mm:ss.fff"));
                    bool bMoreIsNotTheSame = false;
                    var resp = await RestAPI.GetMore(Joyces.Helpers.Settings.AccessToken);
                    if (resp != null && resp is List<More>)
                    {
                        string strJsonFromRest = JsonConvert.SerializeObject((List<More>)resp);

                        if (strJsonFromRest != Joyces.Helpers.Settings.MoreJson)
                        {
                            Joyces.Platform.AppContext.Instance.Platform.MoreList = (List<More>)resp;
                            bMoreIsNotTheSame = true;
                        }
                        if (Joyces.Platform.AppContext.Instance.Platform.MoreList != null)
                            await SetMoreSetting();
                    }
        //            System.Diagnostics.Debug.WriteLine("================MORE END ================ " + DateTime.Now.ToString("HH:mm:ss.fff"));
                    
                    if (sSelectedTab == "more")
                    {
                        if (Joyces.Platform.AppContext.Instance.Platform.MoreList != null && bMoreIsNotTheSame)
                        {
                            RunOnUiThread(() =>
                            {
                                LoadMoreFeed();
                            });
                        }
                    }
                });
          //      System.Diagnostics.Debug.WriteLine("================END 2 ================ " + DateTime.Now.ToString("HH:mm:ss.fff"));

            }
            catch (Exception e)
            {

            }
        }
        private async Task CheckValuesFromSettings()
        {
            try
            {
                string strMore = Joyces.Helpers.Settings.MoreJson;
                if (strMore !=null && strMore.Length > 0)
                {
                    Joyces.Platform.AppContext.Instance.Platform.MoreList = JsonConvert.DeserializeObject<List<More>>(strMore);
                }
                string strOffer = Joyces.Helpers.Settings.OfferJson;
                if(strOffer!=null && strOffer.Length > 0)
                {
                    Joyces.Platform.AppContext.Instance.Platform.OfferList = JsonConvert.DeserializeObject<List<Offer>>(strOffer);
                }
                string strNews = Joyces.Helpers.Settings.NewsJson;
                if (strNews != null && strNews.Length > 0)
                {
                    Joyces.Platform.AppContext.Instance.Platform.NewsList = JsonConvert.DeserializeObject<List<News>>(strNews);
                }
                string strCustomer = Joyces.Helpers.Settings.CustomerJson;
                if (strCustomer != null && strCustomer.Length > 0)
                {
                    Joyces.Platform.AppContext.Instance.Platform.CustomerList = JsonConvert.DeserializeObject<Customer>(strCustomer);
                }
            }
            catch(Exception e)
            {

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
            catch (Exception e) { }
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
        private async void LoadOffersSingleView()
        {
            try
            {
                var OfferList = Joyces.Platform.AppContext.Instance.Platform.OfferList;



                if (OfferList != null)
                {
                    CustomListViewOffersAdapter adapter = new CustomListViewOffersAdapter(this, Joyces.Platform.AppContext.Instance.Platform.OfferList, this);

                    ListView listViewOffers = FindViewById<ListView>(Resource.Id.listViewOffers);

                    RunOnUiThread(() =>
                    {
                        if (adapter != null)
                            listViewOffers.Adapter = adapter;

                        listViewOffers.ItemClick += ListViewOffers_ItemClick;
                    });
                }
                else
                {
                    progress.Show();
                    //Alert(Lang.MESSAGE_HEADLINE, Lang.SERVICE_NOT_AVAILABLE, Lang.BUTTON_OK, "Try again", "offers");
                }
            }
            catch (Exception ex)
            {

            }
        }

        private async void LoadOffersView()
        {
            var OfferList = Joyces.Platform.AppContext.Instance.Platform.OfferList;

            if (OfferList != null)
            {
                ViewPager viewPager = FindViewById<ViewPager>(Resource.Id.viewpager);

                TreeCatalog treeCatalog = new TreeCatalog();
                viewPager.Adapter = new TreePagerAdapter(this, treeCatalog, OfferList, this);
            }
            else
            {
                progress.Show();
                //Alert(Lang.MESSAGE_HEADLINE, Lang.SERVICE_NOT_AVAILABLE, Lang.BUTTON_OK);
            }
        }

        private void ListViewOffers_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var singleNewsItem = Joyces.Platform.AppContext.Instance.Platform.OfferList[e.Position];

            if (singleNewsItem != null)
            {
                var intent = new Intent(this, typeof(OfferActivity));
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
            AlertDialog.Builder builder = new AlertDialog.Builder(this);
            AlertDialog ad = builder.Create();
            ad.SetTitle(sHeadline);
            ad.SetIcon(Android.Resource.Drawable.IcDialogAlert);
            ad.SetMessage(sMessage);
            ad.SetButton(sButtonText, (s, e) => { Console.WriteLine("OK Button clicked, alert dismissed"); });

            ad.Show();
        }

        private void Alert(string sHeadline, string sMessage, string sButtonText1, string sButtonText2, string sReloadTab)
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(this);
            AlertDialog ad = builder.Create();
            ad.SetTitle(sHeadline);
            ad.SetIcon(Android.Resource.Drawable.IcDialogAlert);
            ad.SetMessage(sMessage);

            ad.SetButton2(sButtonText1, (senderAlert, args) =>
            {
                //Toast.MakeText(this, "DO NOTHING!", ToastLength.Short).Show();
            });

            ad.SetButton(sButtonText2, (senderAlert, args) =>
            {
                var t1 = Task.Run(async () => await RefreshAllData());
                t1.Wait();

                if (sReloadTab == "offers")
                {
                    if (GeneralSettings.UseLoyaltyCard)
                    {
                        SetContentView(Resource.Layout.OffersView);
                        LoadOffersView();
                    }
                    else
                    {
                        SetContentView(Resource.Layout.OfferSingleView);
                        LoadOffersSingleView();
                    }
                }
                else if (sReloadTab == "news")
                {
                    SetContentView(Resource.Layout.NewsfeedView);
                    LoadNewsfeedView();
                }
                else if (sReloadTab == "more")
                {
                    SetContentView(Resource.Layout.MoreView);
                    LoadMoreFeed();
                }
                else if (sReloadTab == "profile")
                {
                    SetContentView(Resource.Layout.ProfileView);
                    loadProfileViewContent(true);
                }
            });

            ad.Show();
        }
    }
}