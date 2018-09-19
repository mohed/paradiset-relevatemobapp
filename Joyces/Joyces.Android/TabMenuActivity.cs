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
using Joyces.Droid.Fragments;

namespace Joyces.Droid
{

    //[Activity(Label = "Joyces", Theme = "@style/CustomActionBarTheme")]
    [Activity(Label = "Paradiset", Theme = "@style/CustomActionBarTheme", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class TabMenuActivity : Activity
    {
        string sCustomerId = Joyces.Platform.AppContext.Instance.Platform.CustomerId;

        bool bIsUpdating = false;
        ActionBar actionBar;
        ProgressDialog progress;
        private Button btnUpdate;
        private Button btnLogout;
        string sUserAccountNumber;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            sUserAccountNumber = Joyces.Helpers.Settings.UserAccountNo;

            base.OnCreate(savedInstanceState);
            actionBar = base.ActionBar;
            progress = new ProgressDialog(this);
            progress.Indeterminate = true;
            progress.SetProgressStyle(ProgressDialogStyle.Spinner);
            progress.SetMessage(Lang.CONTACTING_SERVER_WAIT);
            progress.SetCancelable(false);

            var t1 = Task.Run(async () => await LoadApp());
            t1.Wait();

            string sDeviceToken = Joyces.Helpers.Settings.PushDeviceToken;

            if (!string.IsNullOrEmpty(sDeviceToken))
                SendRegistrationToAppServer(sDeviceToken);
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
            actionBar.RemoveTabAt(4);

            MenuInflater.Inflate(Resource.Menu.ToolbarMenu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

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
            ActionBar.Tab tab = actionBar.NewTab();
            actionBar.NavigationMode = ActionBarNavigationMode.Tabs;
            actionBar.SetBackgroundDrawable(new ColorDrawable(Android.Graphics.Color.ParseColor(GeneralSettings.AndroidActionBarColor)));

            tab.SetText(Lang.NEWS_HEADER);
            tab.SetIcon(null);
            tab.TabSelected += async (sender, args) =>
            {
                sSelectedTab = "news";
                SetContentView(Resource.Layout.NewsfeedView);
                await LoadNewsfeedView();
            };
            actionBar.AddTab(tab);

            tab = actionBar.NewTab();
            tab.SetText(Lang.ID_HEADER);
            tab.SetIcon(null);
            tab.TabSelected += (sender, args) =>
            {
                sSelectedTab = "id";
                SetContentView(Resource.Layout.IdView);
               LoadIdView();
            };
            actionBar.AddTab(tab);

            tab = actionBar.NewTab();
            tab.SetText(Lang.OFFER_HEADER);
            tab.SetIcon(null);
            tab.TabSelected += (sender, args) =>
            {
                if (GeneralSettings.UseLoyaltyCard)
                {
                    sSelectedTab = "offersWithLoyalty";
                    //SetContentView(Resource.Layout.OffersView);
                    //LoadOffersView();

                    //Implement using fragment
                    SetContentView(Resource.Layout.OfferTabView);
                    var trans = FragmentManager.BeginTransaction();
                    trans.Add(Resource.Id.offersTabView, new OffersFragment(), "OffersFragment");
                    trans.Commit();
                }
                else
                {
                    sSelectedTab = "offersWithoutLoyalty";
                    //SetContentView(Resource.Layout.OfferSingleView);
                    //LoadOffersSingleView();

                    //Implement using fragment
                    SetContentView(Resource.Layout.OfferTabView);
                    var trans = FragmentManager.BeginTransaction();
                    trans.Add(Resource.Id.offersTabView, new OffersFragment(), "OffersFragment");
                    trans.Commit();
                }
            };
            actionBar.AddTab(tab);

            tab = actionBar.NewTab();
            tab.SetText(Lang.MORE_HEADER);
            tab.SetIcon(null);
            tab.TabSelected += (sender, args) =>
            {
                sSelectedTab = "more";
                SetContentView(Resource.Layout.MoreView);
                LoadMoreFeed();
            };
            actionBar.AddTab(tab);

            tab = actionBar.NewTab();
            tab.SetText(Lang.ACCOUNT_MY_ACCOUNT);
            tab.SetIcon(null);
            tab.TabSelected += (sender, args) =>
            {
                sSelectedTab = "profile";
                SetContentView(Resource.Layout.ProfileView);
                loadProfileViewContent(true);
            };
            actionBar.AddTab(tab);

            actionBar.SetSelectedNavigationItem(1);

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


                //CheckBox optInEmail = FindViewById<CheckBox>(Resource.Id.chOptInEmail);
                //CheckBox optInSms = FindViewById<CheckBox>(Resource.Id.chOptInSms);
                //CheckBox optInPush = FindViewById<CheckBox>(Resource.Id.chOptInPush);

                //Communicationchoice updatedEmailCommunicationChoice = new Communicationchoice();
                //Communicationchoice updatedSmsCommunicationChoice = new Communicationchoice();
                //Communicationchoice updatedPushCommunicationChoice = new Communicationchoice();

                //List<Communicationchoice> updatedCommunicationChoices = new List<Communicationchoice>();

                //updatedEmailCommunicationChoice.choice = optInEmail.Checked;
                //updatedEmailCommunicationChoice.typeCode = "email";
                //updatedCommunicationChoices.Add(updatedEmailCommunicationChoice);

                //updatedSmsCommunicationChoice.choice = optInSms.Checked;
                //updatedSmsCommunicationChoice.typeCode = "sms";
                //updatedCommunicationChoices.Add(updatedSmsCommunicationChoice);

                //updatedPushCommunicationChoice.choice = optInPush.Checked;
                //updatedPushCommunicationChoice.typeCode = "push";
                //updatedCommunicationChoices.Add(updatedPushCommunicationChoice);


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
                //mbProfileSelected = true;
                if (customerList != null)
                {
                    EditText txtProfileViewEmail = FindViewById<EditText>(Resource.Id.txtProfileViewEmail);
                    txtProfileViewEmail.Text = customerList.email;

                    EditText txtProfileViewFirstName = FindViewById<EditText>(Resource.Id.txtProfileViewFirstName);
                    txtProfileViewFirstName.Text = customerList.firstName;

                    EditText txtProfileViewLastName = FindViewById<EditText>(Resource.Id.txtProfileViewLastName);
                    txtProfileViewLastName.Text = customerList.lastName;

                    EditText txtProfileViewMobileNo = FindViewById<EditText>(Resource.Id.txtProfileViewMobileNo);
                    txtProfileViewMobileNo.Text = customerList.mobile;

                    btnUpdate = FindViewById<Button>(Resource.Id.btnProfileViewUpdate);
                    btnUpdate.Click += btnUpdate_Click;

                    btnLogout = FindViewById<Button>(Resource.Id.btnProfileViewLogout);
                    btnLogout.Click += btnLogout_Click;

                    btnUpdate.SetBackgroundColor(Android.Graphics.Color.ParseColor(GeneralSettings.ButtonBackgroundColor));
                    btnUpdate.SetTextColor(Android.Graphics.Color.ParseColor(GeneralSettings.ButtonTextColor));


                    Customer.Communicationchoice[] communicationchoices = customerList.communicationChoices;

                    //CheckBox optInEmail = FindViewById<CheckBox>(Resource.Id.chOptInEmail);
                    //CheckBox optInSms = FindViewById<CheckBox>(Resource.Id.chOptInSms);
                    //CheckBox optInPush = FindViewById<CheckBox>(Resource.Id.chOptInPush);

                    //TextView sOptInEmailText = FindViewById<TextView>(Resource.Id.optInEmailText);
                    //TextView sOptInSmsText = FindViewById<TextView>(Resource.Id.optInSmsText);
                    //TextView sOptInPushText = FindViewById<TextView>(Resource.Id.optInPushText);


                    //foreach (Customer.Communicationchoice cc in communicationchoices)
                    //{
                    //    switch (cc.type.code)
                    //    {
                    //        case "email":
                    //            sOptInEmailText.Text = cc.type.name;
                    //            optInEmail.Checked = cc.choice;
                    //            break;
                    //        case "sms":
                    //            sOptInSmsText.Text = cc.type.name;
                    //            optInSms.Checked = cc.choice;
                    //            break;
                    //        case "push":
                    //            sOptInPushText.Text = cc.type.name;
                    //            optInPush.Checked = cc.choice;
                    //            break;
                    //    }
                    //}


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
                for (int i = 0; i < 16; i++)
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
            }
            catch (Exception e)
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

            await RefreshAllData();
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
            }
            catch (Exception)
            {

            }
        }

        private void GetAllDataForTabs()
        {
            //        System.Diagnostics.Debug.WriteLine("================START 2 ================ " + DateTime.Now.ToString("HH:mm:ss.fff"));

            string sUserToken = Joyces.Helpers.Settings.AccessToken;
            string sUserEmail = Joyces.Helpers.Settings.UserEmail;

            // Get Offer
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
                        // await SetOfferSetting();
                    }
                else
                    Joyces.Platform.AppContext.Instance.Platform.OfferList = null;
                if (Joyces.Platform.AppContext.Instance.Platform.OfferList != null && bOfferIsNotTheSame)
                {
                    if (sSelectedTab == "offersWithLoyalty")
                    {
                        RunOnUiThread(() =>
                        {
                                //LoadOffersView();

                                SetContentView(Resource.Layout.OfferTabView);
                            var trans = FragmentManager.BeginTransaction();
                            trans.Add(Resource.Id.offersTabView, new OffersFragment(), "OffersFragment");
                            trans.Commit();
                        });
                    }
                    else if (sSelectedTab == "offersWithoutLoyalty")
                    {
                        RunOnUiThread(() =>
                        {
                                // LoadOffersSingleView();

                                SetContentView(Resource.Layout.OfferTabView);
                            var trans = FragmentManager.BeginTransaction();
                            trans.Add(Resource.Id.offersTabView, new OffersFragment(), "OffersFragment");
                            trans.Commit();
                        });
                    }
                }

            });
            

            // Get news
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
                if (Joyces.Platform.AppContext.Instance.Platform.NewsList != null && bNewsIsNotTheSame)
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

            // Get more
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
            //      System.Diagnostics.Debug.WriteLine("================END 2 ================ " + DateTime.Now.ToString("HH:mm:ss.fff
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
            catch (Exception e)
            {

            }
        }

        private async Task CheckValuesFromSettings()
        {
            try
            {
                string strMore = Joyces.Helpers.Settings.MoreJson;
                if (strMore != null && strMore.Length > 0)
                {
                    Joyces.Platform.AppContext.Instance.Platform.MoreList = JsonConvert.DeserializeObject<List<More>>(strMore);
                }
                /*
                string strOffer = Joyces.Helpers.Settings.OfferJson;
                if (strOffer != null && strOffer.Length > 0)
                {
                    Joyces.Platform.AppContext.Instance.Platform.OfferList = JsonConvert.DeserializeObject<List<Offer>>(strOffer);
                }
                */
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
            catch (Exception e)
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
                        //LoadOffersView();
                    }
                    else
                    {
                        SetContentView(Resource.Layout.OfferSingleView);
                        //LoadOffersSingleView();
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