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
    [Register ("MoreCell")]
    partial class MoreCell
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel moreDescription { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel moreHeadline { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView MoreImage { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (moreDescription != null) {
                moreDescription.Dispose ();
                moreDescription = null;
            }

            if (moreHeadline != null) {
                moreHeadline.Dispose ();
                moreHeadline = null;
            }

            if (MoreImage != null) {
                MoreImage.Dispose ();
                MoreImage = null;
            }
        }
    }
}