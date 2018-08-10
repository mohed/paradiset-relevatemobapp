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
    [Register ("RegisterViewController")]
    partial class RegisterViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnCreateAccount { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblTerms { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblTermsAccept { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblTermsText { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UISwitch switchAccept { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField txtCountryCode { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField txtEmail { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField txtMobileNo { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField txtPersNr { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField txtPwd1 { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField txtPwd2 { get; set; }

        [Action ("UIButton223557_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void UIButton223557_TouchUpInside (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (btnCreateAccount != null) {
                btnCreateAccount.Dispose ();
                btnCreateAccount = null;
            }

            if (lblTerms != null) {
                lblTerms.Dispose ();
                lblTerms = null;
            }

            if (lblTermsAccept != null) {
                lblTermsAccept.Dispose ();
                lblTermsAccept = null;
            }

            if (lblTermsText != null) {
                lblTermsText.Dispose ();
                lblTermsText = null;
            }

            if (switchAccept != null) {
                switchAccept.Dispose ();
                switchAccept = null;
            }

            if (txtCountryCode != null) {
                txtCountryCode.Dispose ();
                txtCountryCode = null;
            }

            if (txtEmail != null) {
                txtEmail.Dispose ();
                txtEmail = null;
            }

            if (txtMobileNo != null) {
                txtMobileNo.Dispose ();
                txtMobileNo = null;
            }

            if (txtPersNr != null) {
                txtPersNr.Dispose ();
                txtPersNr = null;
            }

            if (txtPwd1 != null) {
                txtPwd1.Dispose ();
                txtPwd1 = null;
            }

            if (txtPwd2 != null) {
                txtPwd2.Dispose ();
                txtPwd2 = null;
            }
        }
    }
}