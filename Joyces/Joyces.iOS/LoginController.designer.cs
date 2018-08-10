// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace Joyces.iOS
{
    [Register ("LoginController")]
    partial class LoginController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnForgotPassword { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnLogin { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnRegister { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView imageLogo { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblLoginPassword { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblUsername { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton lblVersion { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView mainView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField txtName { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField txtPassword { get; set; }

        [Action ("BtnLogin_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void BtnLogin_TouchUpInside (UIKit.UIButton sender);

        [Action ("LblVersion_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void LblVersion_TouchUpInside (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (btnForgotPassword != null) {
                btnForgotPassword.Dispose ();
                btnForgotPassword = null;
            }

            if (btnLogin != null) {
                btnLogin.Dispose ();
                btnLogin = null;
            }

            if (btnRegister != null) {
                btnRegister.Dispose ();
                btnRegister = null;
            }

            if (imageLogo != null) {
                imageLogo.Dispose ();
                imageLogo = null;
            }

            if (lblLoginPassword != null) {
                lblLoginPassword.Dispose ();
                lblLoginPassword = null;
            }

            if (lblUsername != null) {
                lblUsername.Dispose ();
                lblUsername = null;
            }

            if (lblVersion != null) {
                lblVersion.Dispose ();
                lblVersion = null;
            }

            if (mainView != null) {
                mainView.Dispose ();
                mainView = null;
            }

            if (txtName != null) {
                txtName.Dispose ();
                txtName = null;
            }

            if (txtPassword != null) {
                txtPassword.Dispose ();
                txtPassword = null;
            }
        }
    }
}