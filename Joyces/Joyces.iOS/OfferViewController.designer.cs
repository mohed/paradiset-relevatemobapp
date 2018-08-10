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
    [Register ("OfferViewController")]
    partial class OfferViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblCode { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblOfferPanel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblValidDate { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView offerCodeQR { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel offerCodeQRHolder { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel OfferDutyText { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView offerImage { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel offerName { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel offerNote { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel OfferValue { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (lblCode != null) {
                lblCode.Dispose ();
                lblCode = null;
            }

            if (lblOfferPanel != null) {
                lblOfferPanel.Dispose ();
                lblOfferPanel = null;
            }

            if (lblValidDate != null) {
                lblValidDate.Dispose ();
                lblValidDate = null;
            }

            if (offerCodeQR != null) {
                offerCodeQR.Dispose ();
                offerCodeQR = null;
            }

            if (offerCodeQRHolder != null) {
                offerCodeQRHolder.Dispose ();
                offerCodeQRHolder = null;
            }

            if (OfferDutyText != null) {
                OfferDutyText.Dispose ();
                OfferDutyText = null;
            }

            if (offerImage != null) {
                offerImage.Dispose ();
                offerImage = null;
            }

            if (offerName != null) {
                offerName.Dispose ();
                offerName = null;
            }

            if (offerNote != null) {
                offerNote.Dispose ();
                offerNote = null;
            }

            if (OfferValue != null) {
                OfferValue.Dispose ();
                OfferValue = null;
            }
        }
    }
}