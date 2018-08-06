using Joyces;
using Foundation;
using System;
using UIKit;
using Joyces.iOS.Model;
using Joyces.iOS.Helpers;
using FFImageLoading;

namespace Joyces.iOS
{
    public partial class ProfileEditViewController : UIViewController
    {
        Customer customerList = Joyces.Platform.AppContext.Instance.Platform.CustomerList;
        bool bSuccess = false;
        UIAlertView alert;
        LoadingOverlay loadPop;

        public ProfileEditViewController(IntPtr handle) : base(handle)
        {
        }

        private void setCurrentClientTheme()
        {
            btnSaveAccount.BackgroundColor = IosHelper.FromHexString(GeneralSettings.ButtonBackgroundColor);
            btnSaveAccount.SetTitleColor(IosHelper.FromHexString(GeneralSettings.ButtonTextColor), UIControlState.Normal);
            this.Title = Lang.ACCOUNT_MY_ACCOUNT;
            try
            {
                if (Joyces.GeneralSettings.MainFont != null)
                {
                    lblEditAccountFirstName.Font = ThemeHelperIOS.GetThemeFont(17);
                    lblEditAccountLastName.Font = ThemeHelperIOS.GetThemeFont(17);
                    lblEditAccountMobile.Font = ThemeHelperIOS.GetThemeFont(17);
                    txtFirstname.Font = ThemeHelperIOS.GetThemeFont(17);
                    txtMobileNo.Font = ThemeHelperIOS.GetThemeFont(17);
                    txtLastname.Font = ThemeHelperIOS.GetThemeFont(17);
                    btnSaveAccount.Font = ThemeHelperIOS.GetThemeFont(17);
                }
                lblEditAccountFirstName.Text = Lang.EDIT_ACCOUNT_FIRSTNAME;
                lblEditAccountLastName.Text = Lang.EDIT_ACCOUNT_LASTNAME;
                lblEditAccountMobile.Text = Lang.ACCOUNT_MOBILE;

                btnSaveAccount.SetTitle(Lang.SAVE, UIControlState.Normal);
                setTitleTheme();
            }
            catch (Exception e)
            {

            }
            try
            {
                this.View.BackgroundColor = ColorExtensions.ToUIColor(GeneralSettings.BackgroundColor); // Color.FromHex("#00FF00").ToUIColor();
            }
            catch (Exception eee)
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

                // this.NavigationController.NavigationBar.BarTintColor = IosHelper.FromHexString(GeneralSettings.BackgroundColor);// UIColor.Yellow;
            }
            catch (Exception e)
            {

            }
        }

        public override void ViewDidLoad()
        {
            var tap = new UITapGestureRecognizer();
            tap.AddTarget(() => View.EndEditing(true));
            View.AddGestureRecognizer(tap);
            tap.CancelsTouchesInView = false;

            txtFirstname.Text = customerList.firstName;
            txtLastname.Text = customerList.lastName;
            txtMobileNo.Text = customerList.mobile;
            txtMobileNo.Placeholder = GeneralSettings.TelephoneNoMasking;

            this.txtFirstname.ShouldReturn += (textField) =>
            {
                textField.ResignFirstResponder();
                return true;
            };
            this.txtLastname.ShouldReturn += (textField) =>
            {
                textField.ResignFirstResponder();
                return true;
            };
            this.txtMobileNo.ShouldReturn += (textField) =>
            {
                textField.ResignFirstResponder();
                return true;
            };

            txtFirstname.ShouldChangeCharacters = (textField, range, replacementString) =>
            {
                var newLength = textField.Text.Length + replacementString.Length - range.Length;
                return newLength <= 20;
            };

            txtLastname.ShouldChangeCharacters = (textField, range, replacementString) =>
            {
                var newLength = textField.Text.Length + replacementString.Length - range.Length;
                return newLength <= 20;
            };

            txtMobileNo.ShouldChangeCharacters = (textField, range, replacementString) =>
            {
                var newLength = textField.Text.Length + replacementString.Length - range.Length;
                return newLength <= 20;
            };

            setCurrentClientTheme();
        }

        async partial void UIButton185623_TouchUpInside(UIButton sender)
        {
            var bounds = UIScreen.MainScreen.Bounds;
            loadPop = new LoadingOverlay(bounds);
            View.Add(loadPop);

            string sFirstName = txtFirstname.Text;
            string sLastName = txtLastname.Text;
            string sMobile = txtMobileNo.Text;
            string sEmail = customerList.email;
            string sAccessToken = Joyces.Helpers.Settings.AccessToken;

            string sResp = await RestAPI.UpdateUser(sEmail, sFirstName, sLastName, sMobile, sAccessToken);

            if (string.IsNullOrEmpty(sResp))
            {
                var getCustomer = await RestAPI.GetCustomer(sEmail, sAccessToken);

                if (getCustomer == null)
                {
                    alert = new UIAlertView(Lang.MESSAGE_HEADLINE, Lang.UNEXPECTED_ERROR, null, Lang.BUTTON_OK, null);
                    alert.Show();
                }
                else if (getCustomer is Customer)
                {
                    Joyces.Platform.AppContext.Instance.Platform.CustomerList = (Customer)getCustomer;

                    alert = new UIAlertView(Lang.MESSAGE_HEADLINE, Lang.PROFILE_UPDATE, null, Lang.BUTTON_OK, null);
                    alert.Show();
                }
                else if (getCustomer is AbalonErrors)
                {
                    var localError = (AbalonErrors)getCustomer;

                    if (localError.error.Contains("invalid_token") || localError.error.Contains("invalid_grant"))
                    {
                        Joyces.Helpers.Settings.AccessToken = string.Empty;
                        Joyces.Helpers.Settings.UserEmail = string.Empty;

                        var mainController = Storyboard.InstantiateViewController("loginNavigationController");
                        UIApplication.SharedApplication.KeyWindow.RootViewController = mainController;

                        alert = new UIAlertView(Lang.MESSAGE_HEADLINE, Lang.USER_HAS_LOGGED_OUT, null, Lang.BUTTON_OK, null);
                        alert.Show();
                    }
                    else
                    {
                        alert = new UIAlertView(Lang.MESSAGE_HEADLINE, ((AbalonErrors)getCustomer).error_description, null, Lang.BUTTON_OK, null);
                        alert.Show();
                    }
                }

                loadPop.Hide();
            }
            else
            {
                alert = new UIAlertView(Lang.MESSAGE_HEADLINE, sResp, null, Lang.BUTTON_OK, null);
                alert.Show();

                loadPop.Hide();
            }
        }
    }
}