using FFImageLoading;
using Foundation;
using Joyces.iOS.Model;
using System;
using System.Collections.Generic;
using UIKit;

namespace Joyces.iOS
{
    public partial class MenuTabBarController : UITabBarController
    {
        public MenuTabBarController(IntPtr handle) : base(handle)
        {
            
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.

            //var tbViewControllers = new List<UIViewController>(TabBarController.ViewControllers);
            //tbViewControllers.Insert(2, new RemvoedViewController());
            //TabBarController.ViewControllers = tbViewControllers.ToArray();

            UITabBar.Appearance.SelectedImageTintColor = IosHelper.FromHexString(GeneralSettings.TabBarTint);
            UITabBar.Appearance.TintColor = IosHelper.FromHexString(GeneralSettings.TabBarTint);
            try
            {
                this.View.BackgroundColor = ColorExtensions.ToUIColor(GeneralSettings.BackgroundColor); // Color.FromHex("#00FF00").ToUIColor();
            }
            catch (Exception eee)
            {

            }
        }


    }
}