using Joyces.iOS.Model;
using Foundation;
using System;
using UIKit;
using System.Threading.Tasks;
using System.Collections.Generic;
using Joyces.iOS.Helpers;
using FFImageLoading;

namespace Joyces.iOS
{
    public partial class IdCardController : UIViewController
    {
        LoadingOverlay loadPop;
        string sCustomerId = Joyces.Platform.AppContext.Instance.Platform.CustomerId;
        string sUserAccountNumber = Joyces.Helpers.Settings.UserAccountNo;

        public IdCardController(IntPtr handle) : base(handle)
        {

        }

        async override public void ViewWillAppear(bool animated)
        {
            try
            {
                base.ViewWillAppear(animated);
                NavigationItem.RightBarButtonItem.TintColor = IosHelper.FromHexString(GeneralSettings.TabBarTint);
                setCurrentClientTheme();


            }
            catch (Exception ex)
            {
            }
        }

        private void setCurrentClientTheme()
        {
            //btnCreateAccount.BackgroundColor = IosHelper.FromHexString(GeneralSettings.ButtonBackgroundColor);
            //btnCreateAccount.SetTitleColor(IosHelper.FromHexString(GeneralSettings.ButtonTextColor), UIControlState.Normal);

            this.Title = Lang.ID_HEADER;
            try
            {
                //var d = txtEmail;
                //d.set
                lblIdScanDescription.Text = Lang.ID_DESCRIPTION;
                lblIdLowerDescription.Text = Lang.ID_DESCRIPTION_NEWS;
                lblQRCodeBackground.BackgroundColor = IosHelper.FromHexString(GeneralSettings.BackgroundColorSpecific);
                if (Joyces.GeneralSettings.MainFont != null)
                {
                    lblIdScanDescription.Font = ThemeHelperIOS.GetThemeFont(17);
                    lblIdLowerDescription.Font = ThemeHelperIOS.GetThemeFont(14);
                }
                //try
                //{
                //    this.View.BackgroundColor = ColorExtensions.ToUIColor(GeneralSettings.BackgroundColor); // Color.FromHex("#00FF00").ToUIColor();
                //}
                //catch (Exception eee)
                //{

                //}
                setTitleTheme();
            }
            catch (Exception e)
            {

            }
        }
        private void setTitleTheme()
        {
            try
            {

                //TITLE FONT
                UIStringAttributes ats = new UIStringAttributes();
                ats.Font = ThemeHelperIOS.GetThemeFont(17); 
                // ats.ForegroundColor = UIColor.Purple;
                this.NavigationController.NavigationBar.TitleTextAttributes = ats;
                //BACK BUTTON COLOR
                //this.NavigationController.NavigationBar.TintColor = IosHelper.FromHexString(GeneralSettings.ButtonTextColor);// UIColor.Magenta;

                //HEADER BACKGROUND COLOR
               // this.NavigationController.NavigationBar.BarTintColor = IosHelper.FromHexString(GeneralSettings.BackgroundColor);// UIColor.Yellow;
           }
            catch (Exception e)
            {

            }
        }
        async private void sendDeviceToken(string sAccessToken)
        {
            //Skicka DeviceToken varje gång appen laddas, så länge deviceToken inte är null
            if (!string.IsNullOrEmpty(Joyces.Helpers.Settings.PushDeviceToken))
            {
                string sUUID = UIDevice.CurrentDevice.IdentifierForVendor.ToString();
                string sSystemOSVersion = UIDevice.CurrentDevice.SystemVersion;
                string sAppID = NSBundle.MainBundle.InfoDictionary["CFBundleIdentifier"].ToString() + ".ios";
                string sAppVersion = NSBundle.MainBundle.InfoDictionary["CFBundleVersion"].ToString();
                string sCustomerName = Joyces.Platform.AppContext.Instance.Platform.CustomerList.name;
                string sCustomerId = Joyces.Platform.AppContext.Instance.Platform.CustomerList.accountNumber;
                string sPlatform = "iOS";

                var result = await RestAPI.PostDeviceInformation(sAppID, sAppVersion, sSystemOSVersion, sUUID, sCustomerName, sPlatform, Joyces.Helpers.Settings.PushDeviceToken.Replace(" ", string.Empty), sAccessToken, sCustomerId);
            }
        }

        async public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            try
            {
                if (string.IsNullOrEmpty(sUserAccountNumber))
                    sUserAccountNumber = "0000";

                ImagePersonalQRCode.Image = IosHelper.GetQrCode(sUserAccountNumber, 240, 240, 0);
                lblCustomerId.Text = sUserAccountNumber;

                lblQRCodeBackground.Layer.CornerRadius = 10f;
                lblQRCodeBackground.Layer.MasksToBounds = true;

                qrCodePlaceHolderBorder.Layer.CornerRadius = 10f;
                qrCodePlaceHolderBorder.Layer.MasksToBounds = true;
                qrCodePlaceHolderBorder.Layer.BorderColor = UIColor.White.CGColor;
                qrCodePlaceHolderBorder.Layer.BorderWidth = 1f;

                if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0))
                {
                    var pushSettings = UIUserNotificationSettings.GetSettingsForTypes(
                                       UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound,
                                       new NSSet());

                    UIApplication.SharedApplication.RegisterUserNotificationSettings(pushSettings);
                    UIApplication.SharedApplication.RegisterForRemoteNotifications();
                }
                else
                {
                    UIRemoteNotificationType notificationTypes = UIRemoteNotificationType.Alert | UIRemoteNotificationType.Badge | UIRemoteNotificationType.Sound;
                    UIApplication.SharedApplication.RegisterForRemoteNotificationTypes(notificationTypes);
                }

                //string sAccessToken = Helpers.Settings.AccessToken;
                //string sUserEmail = Helpers.Settings.UserEmail;

                //if (!string.IsNullOrEmpty(sAccessToken) && !string.IsNullOrEmpty(sUserEmail))
                //{
                //    Joyces.Platform.AppContext.Instance.Platform.CustomerId = sUserEmail;

                //    var getCustomer = await RestAPI.GetCustomer(sUserEmail, sAccessToken);

                //    if (getCustomer != null && getCustomer is Customer)
                //    {
                //        Joyces.Platform.AppContext.Instance.Platform.CustomerList = (Customer)getCustomer;

                //        var resp = await RestAPI.GetOffer(sUserEmail, sAccessToken);

                //        if (resp != null && resp is List<Offer>)
                //            Joyces.Platform.AppContext.Instance.Platform.OfferList = (List<Offer>)resp;
                //        else
                //            Joyces.Platform.AppContext.Instance.Platform.OfferList = null;

                //        resp = await RestAPI.GetNews(sUserEmail, sAccessToken);

                //        if (resp != null && resp is List<Joyces.News>)
                //            Joyces.Platform.AppContext.Instance.Platform.NewsList = (List<Joyces.News>)resp;
                //        else
                //            Joyces.Platform.AppContext.Instance.Platform.NewsList = null;

                //        Joyces.Platform.AppContext.Instance.Platform.MoreList = await RestAPI.GetMore(sAccessToken);
                //    }
                //    else
                //    {
                //        Helpers.Settings.AccessToken = string.Empty;
                //        Helpers.Settings.UserEmail = string.Empty;

                //        var mainController = Storyboard.InstantiateViewController("loginNavigationController");
                //        UIApplication.SharedApplication.KeyWindow.RootViewController = mainController;
                //    }
                //}
                //else
                //{
                //    //Något är JÄÄÄVLIGT FEL, hit ska den absolut aldrig kunna komma!!!

                //    Helpers.Settings.AccessToken = string.Empty;
                //    Helpers.Settings.UserEmail = string.Empty;

                //    var mainController = Storyboard.InstantiateViewController("loginNavigationController");
                //    UIApplication.SharedApplication.KeyWindow.RootViewController = mainController;

                //    return;
                //}

                if (!string.IsNullOrEmpty(Joyces.Helpers.Settings.AccessToken))
                    sendDeviceToken(Joyces.Helpers.Settings.AccessToken);

                
            }
            catch (Exception ex)
            {

            }
        }
    }
}