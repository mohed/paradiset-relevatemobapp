﻿// WARNING
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
    [Register ("MoreTableViewController")]
    partial class MoreTableViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableView moreTableView { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (moreTableView != null) {
                moreTableView.Dispose ();
                moreTableView = null;
            }
        }
    }
}