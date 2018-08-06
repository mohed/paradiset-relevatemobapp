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
    [Register ("NewsfeedCell")]
    partial class NewsfeedCell
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel NewsBody { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel NewsHeadline { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView NewsImage { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (NewsBody != null) {
                NewsBody.Dispose ();
                NewsBody = null;
            }

            if (NewsHeadline != null) {
                NewsHeadline.Dispose ();
                NewsHeadline = null;
            }

            if (NewsImage != null) {
                NewsImage.Dispose ();
                NewsImage = null;
            }
        }
    }
}