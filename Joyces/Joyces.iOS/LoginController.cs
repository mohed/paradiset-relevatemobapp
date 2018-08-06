using Joyces;
using Joyces.iOS.Model;
using System;
using System.Collections.Generic;
using UIKit;
using Foundation;
using Joyces.iOS;
using System.Globalization;
using CoreGraphics;
using Joyces.iOS.Helpers;

namespace Joyces.iOS
{
    public partial class LoginController : UIViewController
    {
        LoadingOverlay loadPop;
        BlackScreen bs;
        int iSecretButtonClickCounter = 0;

        public LoginController(IntPtr handle) : base(handle)
        {

        }

        public override bool PrefersStatusBarHidden()
        {
            return true;
        }

        async override public void ViewDidAppear(bool animated)
        {
            if (GeneralSettings.AutoLogin)
            {

                if (!string.IsNullOrEmpty(Joyces.Helpers.Settings.AccessToken) &&
                    !string.IsNullOrEmpty(Joyces.Helpers.Settings.UserEmail))
                {
                    //Kontrollerar om användaren redan har loggat in en gång. 
                    try
                    {
                        var bounds = UIScreen.MainScreen.Bounds;
                        loadPop = new LoadingOverlay(bounds);
                        View.Add(loadPop);
                        System.Diagnostics.Debug.WriteLine("================LOGIN  IOS BEFORE CALLS ================ " + DateTime.Now.ToString("HH:mm:ss.fff"));

                        //string sAccessToken = Helpers.Settings.AccessToken;
                        string sUserEmail = Joyces.Helpers.Settings.UserEmail;

                        if (!string.IsNullOrEmpty(Joyces.Helpers.Settings.AccessToken) && !string.IsNullOrEmpty(sUserEmail))
                        {
                            Joyces.Platform.AppContext.Instance.Platform.CustomerId = sUserEmail;

                            var getCustomer = await RestAPI.GetCustomer(sUserEmail, Joyces.Helpers.Settings.AccessToken);

                            if (getCustomer != null && getCustomer is Customer)
                            {
                                Joyces.Platform.AppContext.Instance.Platform.CustomerList = (Customer)getCustomer;

                                var resp = await RestAPI.GetOffer(sUserEmail, Joyces.Helpers.Settings.AccessToken);

                                if (resp != null && resp is List<Offer>)
                                    Joyces.Platform.AppContext.Instance.Platform.OfferList = (List<Offer>)resp;
                                else
                                    Joyces.Platform.AppContext.Instance.Platform.OfferList = null;

                                resp = await RestAPI.GetNews(sUserEmail, Joyces.Helpers.Settings.AccessToken);

                                if (resp != null && resp is List<Joyces.News>)
                                    Joyces.Platform.AppContext.Instance.Platform.NewsList = (List<Joyces.News>)resp;
                                else
                                    Joyces.Platform.AppContext.Instance.Platform.NewsList = null;

                                Joyces.Platform.AppContext.Instance.Platform.MoreList = await RestAPI.GetMore(Joyces.Helpers.Settings.AccessToken);

                                var x = new MenuTabBarController(this.Handle);
                                x.PerformSegue("segueMenu", this);
                            }
                            else
                            {
                                Joyces.Helpers.Settings.AccessToken = string.Empty;
                                Joyces.Helpers.Settings.UserEmail = string.Empty;

                                return;
                            }
                        }
                        System.Diagnostics.Debug.WriteLine("================LOGIN  IOS AFTER CALLS ================ " + DateTime.Now.ToString("HH:mm:ss.fff"));

                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
        }

        override public void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            NavigationController.NavigationBarHidden = true;

        }

        private void cool()
        {

        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            setCurrentClientTheme();

            //Gör så att tagenbordet fösvinner om man nuddar Viewn
            var tap = new UITapGestureRecognizer();
            tap.AddTarget(() => View.EndEditing(true));
            View.AddGestureRecognizer(tap);
            tap.CancelsTouchesInView = false;

            Settings.SetGeneralSetting();
            //NavigationController.NavigationBarHidden = true;

            float fRadius = 5f;
            bool bMaskToBounds = true;

            lblUsername.Layer.CornerRadius = fRadius;
            lblLoginPassword.Layer.CornerRadius = fRadius;
            btnLogin.Layer.CornerRadius = fRadius;
            btnRegister.Layer.CornerRadius = fRadius;
            btnForgotPassword.Layer.CornerRadius = fRadius;
            lblUsername.Layer.MasksToBounds = bMaskToBounds;
            lblLoginPassword.Layer.MasksToBounds = bMaskToBounds;
            btnLogin.Layer.MasksToBounds = bMaskToBounds;
            btnRegister.Layer.MasksToBounds = bMaskToBounds;
            btnForgotPassword.Layer.MasksToBounds = bMaskToBounds;


            string sAppVersion = NSBundle.MainBundle.InfoDictionary["CFBundleVersion"].ToString();

#if DEBUG
            lblVersion.SetTitle("Version " + sAppVersion + "_dev", UIControlState.Normal);
            //txtName.Text = "ifuser9@grr.la";
            //txtPassword.Text = "whacko";
            txtName.Text = "lunkan30@grr.la";
            txtPassword.Text = "lunkan";
#else
            lblVersion.SetTitle("Version " + sAppVersion, UIControlState.Normal);
#endif

            this.txtName.ShouldReturn += (textField) =>
            {
                textField.ResignFirstResponder();
                return true;
            };

            this.txtPassword.ShouldReturn += (textField) =>
            {
                textField.ResignFirstResponder();
                return true;
            };

            txtName.ShouldChangeCharacters = (textField, range, replacementString) =>
            {
                var newLength = textField.Text.Length + replacementString.Length - range.Length;
                return newLength <= 50;
            };

            txtPassword.ShouldChangeCharacters = (textField, range, replacementString) =>
            {
                var newLength = textField.Text.Length + replacementString.Length - range.Length;
                return newLength <= 20;
            };
        }
        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        public override bool ShouldPerformSegue(string segueIdentifier, NSObject sender)
        {
            if (segueIdentifier == "segueRegister" ||
                segueIdentifier == "segueForgotPassword")
            {
                this.NavigationController.NavigationBar.BarStyle = UIBarStyle.Default;

                NavigationController.NavigationBarHidden = false;
                return base.ShouldPerformSegue(segueIdentifier, sender);
            }
            else if (segueIdentifier == "segueDeveloperMenu" && iSecretButtonClickCounter > 5)
            {
                this.NavigationController.NavigationBar.BarStyle = UIBarStyle.Default;

                NavigationController.NavigationBarHidden = false;
                return base.ShouldPerformSegue(segueIdentifier, sender);
            }
            else
            {
                return false;
            }
        }

        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            try
            {
                //TA BORT TEXTEN PÅ BAKÅTKNAPPEN, VISA BARA PIL
                this.NavigationItem.BackBarButtonItem = new UIBarButtonItem(string.Empty,
                UIBarButtonItemStyle.Bordered, null, null);
            }catch(Exception e)
            {

            }
            base.PrepareForSegue(segue, sender);
        }

        //Login button
        async partial void BtnLogin_TouchUpInside(UIButton sender)
        {
            var bounds = UIScreen.MainScreen.Bounds;
            loadPop = new LoadingOverlay(bounds);
            View.Add(loadPop);

            string sUsername = txtName.Text;
            string sPassword = txtPassword.Text;

            var tokenModel = await RestAPI.GetUserToken(sUsername, sPassword);


            
            if (tokenModel != null && tokenModel is TokenModel)
            {
                //Om allt går som det ska, så ska den komma in hit!
                Joyces.Helpers.Settings.AccessToken = ((TokenModel)tokenModel).access_token;
                Joyces.Helpers.Settings.UserEmail = sUsername;

                Joyces.Platform.AppContext.Instance.Platform.CustomerId = sUsername;
                var getCustomer = await RestAPI.GetCustomer(sUsername, Joyces.Helpers.Settings.AccessToken);

                if (getCustomer == null)
                {
                    Joyces.Helpers.Settings.AccessToken = string.Empty;
                    Joyces.Helpers.Settings.UserEmail = string.Empty;

                    UIAlertView _error = new UIAlertView(Lang.ERROR_HEADLINE, Lang.UNEXPECTED_ERROR, null, Lang.BUTTON_OK, null);
                    _error.Show();

                    loadPop.Hide();
                }
                else if (getCustomer is Customer)
                {
                    //Always set new token here.
                    ((TokenModel)tokenModel).access_token = Joyces.Helpers.Settings.AccessToken;

                    Joyces.Platform.AppContext.Instance.Platform.CustomerList = (Customer)getCustomer;

                    var resp = await RestAPI.GetOffer(sUsername, Joyces.Helpers.Settings.AccessToken);

                    if (resp != null && resp is List<Offer>)
                        Joyces.Platform.AppContext.Instance.Platform.OfferList = (List<Offer>)resp;
                    else
                        Joyces.Platform.AppContext.Instance.Platform.OfferList = null;

                    resp = await RestAPI.GetNews(sUsername, Joyces.Helpers.Settings.AccessToken);

                    if (resp != null && resp is List<Joyces.News>)
                        Joyces.Platform.AppContext.Instance.Platform.NewsList = (List<Joyces.News>)resp;
                    else
                        Joyces.Platform.AppContext.Instance.Platform.NewsList = null;


                    Joyces.Platform.AppContext.Instance.Platform.MoreList = await RestAPI.GetMore(Joyces.Helpers.Settings.AccessToken);

                    loadPop.Hide();

                    Joyces.Helpers.Settings.UserAccountNo = Joyces.Platform.AppContext.Instance.Platform.CustomerList.accountNumber;

                    var x = new MenuTabBarController(this.Handle);
                    x.PerformSegue("segueMenu", this);
                }
                else if (getCustomer is AbalonErrors)
                {
                    var localError = (AbalonErrors)getCustomer;

                    Joyces.Helpers.Settings.AccessToken = string.Empty;
                    Joyces.Helpers.Settings.UserEmail = string.Empty;

                    UIAlertView _error = new UIAlertView(Lang.MESSAGE_HEADLINE, localError.message, null, Lang.BUTTON_OK, null);
                    _error.Show();

                    loadPop.Hide();

                    return;
                }
            }
            else if (tokenModel != null && tokenModel is AbalonErrors)
            {
                //Hit kommer den om ett fel returneras från Abalon
                loadPop.Hide();

                Joyces.Helpers.Settings.AccessToken = string.Empty;
                Joyces.Helpers.Settings.UserEmail = string.Empty;

                if (((AbalonErrors)tokenModel).error_description.Contains("PRE_AUTHENTICATION"))
                    ((AbalonErrors)tokenModel).error_description = Lang.NOT_ACTIVATED_MEMBER;
                try
                {
                    //TEMPORÄRT
                    string strErrorDesc = ((AbalonErrors)tokenModel).error_description;
                    strErrorDesc = strErrorDesc.Replace("Missing parameters", "Saknade parametrar").Replace("username", "Epost").Replace("password", "Lösenord");
                    ((AbalonErrors)tokenModel).error_description = strErrorDesc;
                    strErrorDesc = strErrorDesc + "";

                }
                catch (Exception eee)
                {

                }


                UIAlertView _error = new UIAlertView(Lang.MESSAGE_HEADLINE, ((AbalonErrors)tokenModel).error_description.Replace("@carbon.super", ""), null, Lang.BUTTON_OK, null);
                _error.Show();
            }
            else
            {
                //Vid all sorts exception kommer den hit och visar UNEXPECTED_ERROR! 
                loadPop.Hide();

                Joyces.Helpers.Settings.AccessToken = string.Empty;
                Joyces.Helpers.Settings.UserEmail = string.Empty;

                UIAlertView _error = new UIAlertView(Lang.MESSAGE_HEADLINE, Lang.UNEXPECTED_ERROR, null, Lang.BUTTON_OK, null);
                _error.Show();
            }
        }

        private void setCurrentClientTheme()
        {
            imageLogo.Image = UIImage.FromBundle(GeneralSettings.LogoLogin);

            UIColor backgroundColor = IosHelper.FromHexString(GeneralSettings.BackgroundColorSpecific);
            
            mainView.BackgroundColor = backgroundColor;
            btnForgotPassword.BackgroundColor = IosHelper.FromHexString(GeneralSettings.ButtonBackgroundColor); //backgroundColor;
            btnForgotPassword.SetTitleColor(IosHelper.FromHexString(GeneralSettings.ButtonTextColor), UIControlState.Normal);

            btnLogin.BackgroundColor = IosHelper.FromHexString(GeneralSettings.ButtonBackgroundColor);
            btnLogin.SetTitleColor(IosHelper.FromHexString(GeneralSettings.ButtonTextColor), UIControlState.Normal);

            btnRegister.BackgroundColor = IosHelper.FromHexString(GeneralSettings.ButtonBackgroundColor);
            btnRegister.SetTitleColor(IosHelper.FromHexString(GeneralSettings.ButtonTextColor), UIControlState.Normal);
            try
            {
                if (Joyces.GeneralSettings.MainFont != null)
                {
                    btnRegister.Font = ThemeHelperIOS.GetThemeFont(17);
                    btnLogin.Font = ThemeHelperIOS.GetThemeFont(17);
                    btnForgotPassword.Font = ThemeHelperIOS.GetThemeFont(17);
                    lblLoginPassword.Font = ThemeHelperIOS.GetThemeFont(17);
                    lblUsername.Font = ThemeHelperIOS.GetThemeFont(17);
                    txtName.Font = ThemeHelperIOS.GetThemeFont(17);
                    txtPassword.Font = ThemeHelperIOS.GetThemeFont(17);
                    
                }
                btnRegister.SetTitle(Lang.CREATE_ACCOUNT, UIControlState.Normal);
                btnLogin.SetTitle(Lang.LOGIN, UIControlState.Normal);
                btnForgotPassword.SetTitle(Lang.FORGOT_PASSWORD, UIControlState.Normal);
                lblLoginPassword.Text = Lang.PASSWORD;
                lblUsername.Text = Lang.EMAIL;
                

            }
            catch(Exception eee)
            {

            }
        }

        partial void LblVersion_TouchUpInside(UIButton sender)
        {
            iSecretButtonClickCounter++;
        }
    }
}

