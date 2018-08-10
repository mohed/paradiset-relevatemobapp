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
    [Register ("OfferCell")]
    partial class OfferCell
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel OfferBody { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel OfferHeadline { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView OfferImage { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel offerValidDate { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel OfferValue { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (OfferBody != null) {
                OfferBody.Dispose ();
                OfferBody = null;
            }

            if (OfferHeadline != null) {
                OfferHeadline.Dispose ();
                OfferHeadline = null;
            }

            if (OfferImage != null) {
                OfferImage.Dispose ();
                OfferImage = null;
            }

            if (offerValidDate != null) {
                offerValidDate.Dispose ();
                offerValidDate = null;
            }

            if (OfferValue != null) {
                OfferValue.Dispose ();
                OfferValue = null;
            }
        }
    }
}