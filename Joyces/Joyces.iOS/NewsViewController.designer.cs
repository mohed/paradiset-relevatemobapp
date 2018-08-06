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
    [Register ("NewsViewController")]
    partial class NewsViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblNewPanel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel newsDescription { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel newsHeadline { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView newsImage { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (lblNewPanel != null) {
                lblNewPanel.Dispose ();
                lblNewPanel = null;
            }

            if (newsDescription != null) {
                newsDescription.Dispose ();
                newsDescription = null;
            }

            if (newsHeadline != null) {
                newsHeadline.Dispose ();
                newsHeadline = null;
            }

            if (newsImage != null) {
                newsImage.Dispose ();
                newsImage = null;
            }
        }
    }
}