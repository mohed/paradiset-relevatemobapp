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
    [Register ("IdCardController")]
    partial class IdCardController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView ImagePersonalQRCode { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblCustomerId { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblIdLowerDescription { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblIdScanDescription { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblQRCodeBackground { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel qrCodePlaceHolderBorder { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITabBarItem TabBarItemId { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (ImagePersonalQRCode != null) {
                ImagePersonalQRCode.Dispose ();
                ImagePersonalQRCode = null;
            }

            if (lblCustomerId != null) {
                lblCustomerId.Dispose ();
                lblCustomerId = null;
            }

            if (lblIdLowerDescription != null) {
                lblIdLowerDescription.Dispose ();
                lblIdLowerDescription = null;
            }

            if (lblIdScanDescription != null) {
                lblIdScanDescription.Dispose ();
                lblIdScanDescription = null;
            }

            if (lblQRCodeBackground != null) {
                lblQRCodeBackground.Dispose ();
                lblQRCodeBackground = null;
            }

            if (qrCodePlaceHolderBorder != null) {
                qrCodePlaceHolderBorder.Dispose ();
                qrCodePlaceHolderBorder = null;
            }

            if (TabBarItemId != null) {
                TabBarItemId.Dispose ();
                TabBarItemId = null;
            }
        }
    }
}