using Joyces;
using Foundation;
using System;
using System.Collections.Generic;
using UIKit;
using Joyces.iOS.Model;
using Joyces.iOS.Helpers;
using FFImageLoading;

namespace Joyces.iOS
{
    public partial class ProfileViewController : UIViewController
    {
        Customer customerList;

        public ProfileViewController (IntPtr handle) : base (handle)
        {
        }

        private void setCurrentClientTheme()
        {
            btnLogout.BackgroundColor = IosHelper.FromHexString(GeneralSettings.ButtonBackgroundColor);
            btnLogout.SetTitleColor(IosHelper.FromHexString(GeneralSettings.ButtonTextColor), UIControlState.Normal);
            this.Title = Lang.ACCOUNT_MY_ACCOUNT;
            try
            {
                if (Joyces.GeneralSettings.MainFont != null)
                {
                    lblAccountHeader.Font = ThemeHelperIOS.GetThemeFont(17);
                    lblAccountEmail.Font = ThemeHelperIOS.GetThemeFont(17);
                    lblAccountMobile.Font = ThemeHelperIOS.GetThemeFont(17);
                    lblAccountName.Font = ThemeHelperIOS.GetThemeFont(17);
                    txtEmail.Font = ThemeHelperIOS.GetThemeFont(17);
                    txtMobileNo.Font = ThemeHelperIOS.GetThemeFont(17);
                    txtName.Font = ThemeHelperIOS.GetThemeFont(17);
                    btnLogout.Font = ThemeHelperIOS.GetThemeFont(17);
                }
                lblAccountHeader.Text = Lang.ACCOUNT_PERSONAL_DETAILS;
                lblAccountEmail.Text = Lang.EMAIL;
                lblAccountMobile.Text = Lang.ACCOUNT_MOBILE;
                lblAccountName.Text = Lang.ACCOUNT_NAME;

                btnLogout.SetTitle(Lang.LOGOUT, UIControlState.Normal);
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

        public override void ViewDidAppear(bool animated)
        {
            loadUserInfo();
        }

        public override void ViewDidLoad()
        {
            loadUserInfo();
            setCurrentClientTheme();
        }

        private void loadUserInfo()
        {
            customerList = Joyces.Platform.AppContext.Instance.Platform.CustomerList;

            //lblPointBalance.Text = customerList.accumulatedPoints + "p";

            //if (customerList.pointsNeeded == null)
            //    lblPointToCheck.Text = "0p";
            //else
            //    lblPointToCheck.Text = customerList.pointsNeeded + "p";

            txtName.Text = customerList.firstName + " " + customerList.lastName;
            txtEmail.Text = customerList.email;
            txtMobileNo.Text = customerList.mobile;
        }

        partial void UIBarButtonItem112514_Activated(UIBarButtonItem sender)
        {
            DismissModalViewController(true);
        }

        partial void UIButton234963_TouchUpInside(UIButton sender)
        {
            Joyces.Helpers.Settings.AccessToken = string.Empty;
            Joyces.Helpers.Settings.UserEmail = string.Empty;
            Joyces.Helpers.Settings.UserAccountNo = string.Empty;

            var mainController = Storyboard.InstantiateViewController("loginNavigationController");
            UIApplication.SharedApplication.KeyWindow.RootViewController = mainController;
        }
        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            try
            {
                //TA BORT TEXTEN PÅ BAKÅTKNAPPEN, VISA BARA PIL
                this.NavigationItem.BackBarButtonItem = new UIBarButtonItem(string.Empty,
                UIBarButtonItemStyle.Bordered, null, null);
            }
            catch (Exception e)
            {

            }
            base.PrepareForSegue(segue, sender);
        }
    }
}