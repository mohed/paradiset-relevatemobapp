using Joyces.Repository;
using Foundation;
using System;
using UIKit;
using System.Collections.Generic;
using System.Net;
using Joyces.iOS.Model;
using Joyces.iOS.Helpers;
using FFImageLoading;

namespace Joyces.iOS
{
    public partial class RegisterViewController : UIViewController
    {
        LoadingOverlay loadPop;
        ApplicationTokenModel appTokenModel;

        public RegisterViewController(IntPtr handle) : base(handle)
        {
        }

        private void setCurrentClientTheme()
        {
            btnCreateAccount.BackgroundColor = IosHelper.FromHexString(GeneralSettings.ButtonBackgroundColor);
            btnCreateAccount.SetTitleColor(IosHelper.FromHexString(GeneralSettings.ButtonTextColor), UIControlState.Normal);
            
            this.Title = Lang.CREATE_ACCOUNT;
            try
            {
                //var d = txtEmail;
                //d.set
                if (Joyces.GeneralSettings.MainFont != null)
                {
                    txtEmail.Font = ThemeHelperIOS.GetThemeFont(17);
                    txtMobileNo.Font = ThemeHelperIOS.GetThemeFont(17);
                    txtPwd1.Font = ThemeHelperIOS.GetThemeFont(17);
                    txtPwd2.Font = ThemeHelperIOS.GetThemeFont(17);
                    txtPersNr.Font = ThemeHelperIOS.GetThemeFont(17);
                    txtCountryCode.Font = ThemeHelperIOS.GetThemeFont(17);
                    lblTerms.Font = ThemeHelperIOS.GetThemeFont(17);
                    lblTermsText.Font = ThemeHelperIOS.GetThemeFont(14);
                    lblTermsAccept.Font = ThemeHelperIOS.GetThemeFont(14);
                    
                }
                lblTerms.Text = Lang.CREATE_ACCOUNT_TERMS_HEADER;
                lblTermsText.Text = Lang.CREATE_ACCOUNT_TERMS;
               // lblTermsText.TextAlignment = UITextAlignment.Left;
                lblTermsText.SizeToFit();
                lblTermsAccept.Text = Lang.CREATE_ACCOUNT_ACCEPT_TERMS;
                txtEmail.Placeholder = Lang.EMAIL;
                if (btnCreateAccount != null)
                {
                    try
                    {
                        btnCreateAccount.SetTitle(Lang.CREATE_ACCOUNT, UIControlState.Normal);
                        btnCreateAccount.Font = ThemeHelperIOS.GetThemeFont(17);
                    }catch(Exception ee)
                    {

                    }
                }
                try
                {
                    this.View.BackgroundColor = ColorExtensions.ToUIColor(GeneralSettings.BackgroundColor); // Color.FromHex("#00FF00").ToUIColor();
                }
                catch (Exception eee)
                {

                }
                string s = this.NavigationItem.Title;
                var x = this.NavigationItem.BackBarButtonItem;
                setTitleTheme();
              //  UIBarButtonItem buttonBack = new UIBarButtonItem("text", UIBarButtonItemStyle.Plain, (sender, args) => { });
              //  buttonBack.TintColor = UIColor.Red;
              //  buttonBack.
              //  this.NavigationItem.SetLeftBarButtonItem()

              //this.NavigationItem.SetLeftBarButtonItem(new UIBarButtonItem("text", UIBarButtonItemStyle.Plain, (sender, args) => { }), true);
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
                ats.Font = ThemeHelperIOS.GetThemeFont(17); //UIFont.FromName("Copperplate", 24); //ThemeHelperIOS.GetThemeFont(17);
                // ats.ForegroundColor = UIColor.Purple;
                this.NavigationController.NavigationBar.TitleTextAttributes = ats;
                //BACK BUTTON COLOR
                //this.NavigationController.NavigationBar.TintColor = IosHelper.FromHexString(GeneralSettings.ButtonTextColor);// UIColor.Magenta;


                //this.NavigationController.NavigationBar
                //UITextAttributes myTextAttrib = new UITextAttributes();
                //myTextAttrib.Font = UIFont.FromName("Copperplate", 24);
                //myTextAttrib.TextColor = UIColor.FromRGB(202, 185, 131);
                //var attr = UINavigationBar.Appearance.GetTitleTextAttributes();

                //UINavigationBar.Appearance.SetTitleTextAttributes(new UITextAttributes()
                // {
                //     TextColor = UIColor.White,
                //     TextShadowColor = UIColor.Clear
                // });

                //this.NavigationController.NavigationBar.TitleTextAttributes = myTextAttrib;
                //myNavController.NavigationBar.SetTitleTextAttributes(myTextAttrib);

                //HEADER BACKGROUND COLOR
                this.NavigationController.NavigationBar.BarTintColor = IosHelper.FromHexString(GeneralSettings.BackgroundColor);// UIColor.Yellow;
                
               // UITextAttributes icoFontAttribute = new UITextAttributes();

               // icoFontAttribute.Font = ThemeHelperIOS.GetThemeFont(17);// UIFont.FromName("icomoon", 24);
               // //icoFontAttribute.TextColor = UIColor.Red;
               // var btnMap = new UIBarButtonItem("text", UIBarButtonItemStyle.Bordered, (sender, args) => { }); //new UIBarButtonItem();
               //// btnMap.SetBackButtonBackgroundImage(UIImage.FromFile("Images/iconbackarrow.png"), new UIControlState(), new UIBarMetrics());
               // btnMap.SetBackgroundImage(UIImage.FromFile("Images/iconbackarrow.png"), new UIControlState(), new UIBarMetrics());
               // // new UIBarButtonItem ("xe620", UIBarButtonItemStyle.Done, null);
               // btnMap.SetTitleTextAttributes(icoFontAttribute, UIControlState.Application);
               // btnMap.Title = "Login";
               // btnMap.Style = UIBarButtonItemStyle.Done;
               // btnMap.TintColor = UIColor.Red;
               // this.NavigationItem.SetLeftBarButtonItem(btnMap, true);
               // var par = this.ParentViewController;
                //UIView v = new UIView()
                //v.TintColor = UIColor.Purple;

                //this.NavigationItem.TitleView = v;
               
            }
            catch(Exception e)
            {

            }
        }

        async public override void ViewDidLoad()
        {
            setCurrentClientTheme();
            var tap = new UITapGestureRecognizer();
            tap.AddTarget(() => View.EndEditing(true));
            View.AddGestureRecognizer(tap);
            tap.CancelsTouchesInView = false;

            this.txtEmail.ShouldReturn += (textField) =>
            {
                textField.ResignFirstResponder();
                return true;
            };

            this.txtCountryCode.ShouldReturn += (textField) =>
            {
                textField.ResignFirstResponder();
                return true;
            };

            this.txtMobileNo.ShouldReturn += (textField) =>
            {
                textField.ResignFirstResponder();
                return true;
            };

            this.txtPersNr.ShouldReturn += (textField) =>
            {
                textField.ResignFirstResponder();
                return true;
            };

            this.txtPwd1.ShouldReturn += (textField) =>
            {
                textField.ResignFirstResponder();
                return true;
            };

            this.txtPwd2.ShouldReturn += (textField) =>
            {
                textField.ResignFirstResponder();
                return true;
            };

           
            //SET MAX CHAR
            this.txtCountryCode.ShouldChangeCharacters = (UITextField textField, NSRange range, string replacementString) =>
            {
                // Calculate new length
                var length = textField.Text.Length - range.Length + replacementString.Length;
                return length <= 3;
            };


            appTokenModel = await RestAPI.GetApplicationToken();

            if (appTokenModel == null)
            {
                //Skickar användaren till inloggningssidan
                var mainController = Storyboard.InstantiateViewController("loginNavigationController");
                UIApplication.SharedApplication.KeyWindow.RootViewController = mainController;

                UIAlertView _msg = new UIAlertView(Lang.ERROR_HEADLINE, Lang.UNEXPECTED_ERROR, null, Lang.BUTTON_OK, null);
                _msg.Show();
            }

            txtEmail.ShouldChangeCharacters = (textField, range, replacementString) =>
            {
                var newLength = textField.Text.Length + replacementString.Length - range.Length;
                return newLength <= 50;
            };

            txtMobileNo.ShouldChangeCharacters = (textField, range, replacementString) =>
            {
                var newLength = textField.Text.Length + replacementString.Length - range.Length;
                return newLength <= 20;
            };

            //txtFirstName.ShouldChangeCharacters = (textField, range, replacementString) =>
            //{
            //    var newLength = textField.Text.Length + replacementString.Length - range.Length;
            //    return newLength <= 20;
            //};

            //txtLastName.ShouldChangeCharacters = (textField, range, replacementString) =>
            //{
            //    var newLength = textField.Text.Length + replacementString.Length - range.Length;
            //    return newLength <= 20;
            //};

            //txtPassword1.ShouldChangeCharacters = (textField, range, replacementString) =>
            //{
            //    var newLength = textField.Text.Length + replacementString.Length - range.Length;
            //    return newLength <= 20;
            //};

            //txtPassword2.ShouldChangeCharacters = (textField, range, replacementString) =>
            //{
            //    var newLength = textField.Text.Length + replacementString.Length - range.Length;
            //    return newLength <= 20;
            //};

            

            txtCountryCode.Text = GeneralSettings.TelephoneNoMasking;
            //txtMobileNo.Placeholder = GeneralSettings.TelephoneNoMasking;
        }


        async partial void UIButton223557_TouchUpInside(UIButton sender)
        {

            var bounds = UIScreen.MainScreen.Bounds;
            loadPop = new LoadingOverlay(bounds);
            View.Add(loadPop);

            string sEmail = txtEmail.Text;
            string sMobile = txtMobileNo.Text;
            string sCountryCode = txtCountryCode.Text;
            if (sCountryCode.Contains("+"))
            {
                sCountryCode= sCountryCode.Replace("+", "");//SKA MAN TA BORT INLEDANDE NOLLOR OCKSÅ?
            }
            if (sMobile.StartsWith("0"))
            {
                sMobile = sMobile.Substring(1);
            }
            sMobile = sCountryCode + sMobile;
            string sPersNr = txtPersNr.Text;
            string sPassword1 = txtPwd1.Text;
            string sPassword2 = txtPwd2.Text;
           
            if (!ObjectRepository.EmailValidator(sEmail))
            {
                UIAlertView _msg = new UIAlertView(Lang.ERROR_HEADLINE, Lang.ENTER_VALID_EMAIL, null, Lang.BUTTON_OK, null);
                _msg.Show();

                loadPop.Hide();
                return;
            }

            if (sPassword1.Length < 1 || sPassword2.Length < 1)
            {
                UIAlertView _msg = new UIAlertView(Lang.ERROR_HEADLINE, Lang.ENTER_VALID_PASSWORD, null, Lang.BUTTON_OK, null);
                _msg.Show();

                loadPop.Hide();
                return;
            }

            if (sPassword1 != sPassword2)
            {
                UIAlertView _msg = new UIAlertView(Lang.ERROR_HEADLINE, Lang.PASSWORD_NOT_MATCH, null, Lang.BUTTON_OK, null);
                _msg.Show();

                loadPop.Hide();
                return;
            }

            if (!switchAccept.On)
            {
                UIAlertView _msg = new UIAlertView(Lang.ERROR_HEADLINE, Lang.TERM_AND_CONDITIONS, null, Lang.BUTTON_OK, null);
                _msg.Show();

                loadPop.Hide();
                return;
            }
           
            var response = await RestAPI.RegisterUserWithPersonalNumber(sEmail, sPersNr, true, true, sMobile, sPassword1, appTokenModel.access_token);

            if (response == null)
            {
                var mainController = Storyboard.InstantiateViewController("loginNavigationController");
                UIApplication.SharedApplication.KeyWindow.RootViewController = mainController;

                UIAlertView _msg = new UIAlertView(Lang.ERROR_HEADLINE, Lang.UNEXPECTED_ERROR, null, Lang.BUTTON_OK, null);
                _msg.Show();

                loadPop.Hide();
            }
            else if (response != null && response is Customer)
            {
                Joyces.Helpers.Settings.AccessToken = string.Empty;
                Joyces.Helpers.Settings.UserEmail = string.Empty;

                //Skickar användaren till inloggningssidan
                var mainController = Storyboard.InstantiateViewController("loginNavigationController");
                UIApplication.SharedApplication.KeyWindow.RootViewController = mainController;

                UIAlertView _msg = new UIAlertView(Lang.MESSAGE_HEADLINE, Lang.ACTIVATION_EMAIL_SENT, null, Lang.BUTTON_OK, null);
                _msg.Show();

                loadPop.Hide();
            }
            else if (response != null && response is AbalonErrors)
            {
                loadPop.Hide();

                UIAlertView _msg = new UIAlertView(Lang.ERROR_HEADLINE, ((AbalonErrors)response).message, null, Lang.BUTTON_OK, null);
                _msg.Show();
            }
        }

        //partial void UIButton326052_TouchUpInside(UIButton sender)
        //{
        //    //privacy policy
        //    UIApplication.SharedApplication.OpenUrl(new NSUrl(GeneralSettings.PRIVACY_POLICY_URL));
        //}


        //partial void UIButton223553_TouchUpInside(UIButton sender)
        //{
        //    //TERM_AND_CONDITION
        //    UIApplication.SharedApplication.OpenUrl(new NSUrl(GeneralSettings.TERM_AND_CONDITION_URL));
        //}
    }
}