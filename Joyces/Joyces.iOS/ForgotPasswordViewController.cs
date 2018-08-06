using Foundation;
using Joyces.iOS.Helpers;
using Joyces.iOS.Model;
using System;
using UIKit;

namespace Joyces.iOS
{
    public partial class ForgotPasswordViewController : UIViewController
    {
        UIAlertView _msg;
        LoadingOverlay loadPop;

        public ForgotPasswordViewController(IntPtr handle) : base(handle)
        {
        }

       
        /// <summary>
        /// 
        /// </summary>
        private void setCurrentClientTheme()
        {
            btnResetPassword.BackgroundColor = IosHelper.FromHexString(GeneralSettings.ButtonBackgroundColor);
            btnResetPassword.SetTitleColor(IosHelper.FromHexString(GeneralSettings.ButtonTextColor), UIControlState.Normal);

            this.Title = Lang.FORGOT_PASSWORD;
            try
            {
                if (Joyces.GeneralSettings.MainFont != null)
                {
                    txtForgotEmail.Font = ThemeHelperIOS.GetThemeFont(17);
                }
                txtForgotEmail.Placeholder = Lang.EMAIL;
                btnResetPassword.SetTitle(Lang.RESET_PASSWORD, UIControlState.Normal);
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
                ats.Font = ThemeHelperIOS.GetThemeFont(17); //UIFont.FromName("Copperplate", 24); //ThemeHelperIOS.GetThemeFont(17);
                // ats.ForegroundColor = UIColor.Purple;
                this.NavigationController.NavigationBar.TitleTextAttributes = ats;

                this.NavigationController.NavigationBar.BarTintColor = IosHelper.FromHexString(GeneralSettings.BackgroundColor);// UIColor.Yellow;
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

            this.txtForgotEmail.ShouldReturn += (textField) =>
            {
                textField.ResignFirstResponder();
                return true;
            };

            setCurrentClientTheme();
        }

        async partial void UIButton249827_TouchUpInside(UIButton sender)
        {
            var bounds = UIScreen.MainScreen.Bounds;
            loadPop = new LoadingOverlay(bounds);
            View.Add(loadPop);

            string sEmail = txtForgotEmail.Text;

            bool bSuccess = await RestAPI.ForgotPassword(sEmail);

            if (bSuccess)
            {
                _msg = new UIAlertView(Lang.MESSAGE_HEADLINE, Lang.RESET_PASSWORD_LINK, null, Lang.BUTTON_OK, null);
                _msg.Show();
            }
            else
            {
                _msg = new UIAlertView(Lang.MESSAGE_HEADLINE, Lang.NO_EMAIL_FOUND, null, Lang.BUTTON_OK, null);
                _msg.Show();
            }

            loadPop.Hide();
        }
    }
}