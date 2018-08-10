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
    [Register ("ProfileEditViewController")]
    partial class ProfileEditViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnSaveAccount { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblEditAccountFirstName { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblEditAccountLastName { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblEditAccountMobile { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField txtFirstname { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField txtLastname { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField txtMobileNo { get; set; }

        [Action ("UIButton185623_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void UIButton185623_TouchUpInside (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (btnSaveAccount != null) {
                btnSaveAccount.Dispose ();
                btnSaveAccount = null;
            }

            if (lblEditAccountFirstName != null) {
                lblEditAccountFirstName.Dispose ();
                lblEditAccountFirstName = null;
            }

            if (lblEditAccountLastName != null) {
                lblEditAccountLastName.Dispose ();
                lblEditAccountLastName = null;
            }

            if (lblEditAccountMobile != null) {
                lblEditAccountMobile.Dispose ();
                lblEditAccountMobile = null;
            }

            if (txtFirstname != null) {
                txtFirstname.Dispose ();
                txtFirstname = null;
            }

            if (txtLastname != null) {
                txtLastname.Dispose ();
                txtLastname = null;
            }

            if (txtMobileNo != null) {
                txtMobileNo.Dispose ();
                txtMobileNo = null;
            }
        }
    }
}