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
    [Register ("DeveloperViewController")]
    partial class DeveloperViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextView TextViewRESTStatus { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextView TextViewServerStatus { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextView txtBundleIdentifier { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextView txtDeviceToken { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (TextViewRESTStatus != null) {
                TextViewRESTStatus.Dispose ();
                TextViewRESTStatus = null;
            }

            if (TextViewServerStatus != null) {
                TextViewServerStatus.Dispose ();
                TextViewServerStatus = null;
            }

            if (txtBundleIdentifier != null) {
                txtBundleIdentifier.Dispose ();
                txtBundleIdentifier = null;
            }

            if (txtDeviceToken != null) {
                txtDeviceToken.Dispose ();
                txtDeviceToken = null;
            }
        }
    }
}