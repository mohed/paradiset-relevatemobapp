using Joyces;
using Joyces.iOS;
using Joyces.iOS.Model;
using FFImageLoading;
using Foundation;
using System;
using UIKit;

namespace Joyces.iOS
{
    public partial class NewsViewController : UIViewController
    {

        public NewsViewController (IntPtr handle) : base (handle)
        {
            //todo: kör raden under i 
            //newsHeadline.Text = IosHelper.getNSUserDefaults("newsHeadline");
        }

        public async override void ViewDidLoad()
        {
            if (!string.IsNullOrEmpty(IosHelper.getNSUserDefaults("newsImageURL")))
                ImageService.Instance.LoadUrl(IosHelper.getNSUserDefaults("newsImageURL")).Into(newsImage);

            newsHeadline.Text = IosHelper.getNSUserDefaults("newsName");
            newsDescription.Text = IosHelper.getNSUserDefaults("newsNote");
            this.Title = newsHeadline.Text;

            UIColor BorderColor = new UIColor(red: 0.71f, green: 0.70f, blue: 0.68f, alpha: 1.0f);
            lblNewPanel.Layer.BorderColor = BorderColor.CGColor;
            lblNewPanel.Layer.BorderWidth = 1;
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