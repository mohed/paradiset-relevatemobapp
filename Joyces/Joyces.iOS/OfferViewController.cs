using Joyces.iOS.Model;
using FFImageLoading;
using Foundation;
using System;
using UIKit;

namespace Joyces.iOS
{
    public partial class OfferViewController : UIViewController
    {
        public OfferViewController (IntPtr handle) : base (handle)
        {
        }

        public async override void ViewDidLoad()
        {
            if (!string.IsNullOrEmpty(IosHelper.getNSUserDefaults("offerImageURL")))
                ImageService.Instance.LoadUrl(IosHelper.getNSUserDefaults("offerImageURL")).Into(offerImage);

            offerName.Text = IosHelper.getNSUserDefaults("offerName");
            offerNote.Text = IosHelper.getNSUserDefaults("offerNote");
            OfferValue.Text = IosHelper.getNSUserDefaults("offerValue");
            lblValidDate.Text = Lang.VALID_UNTIL + " " + IosHelper.getNSUserDefaults("offerValidityDate");
            OfferDutyText.Text = IosHelper.getNSUserDefaults("offerDutyText");
            this.Title = offerName.Text;

            UIColor BorderColor =  new UIColor(red: 0.71f, green: 0.70f, blue: 0.68f, alpha: 1.0f);
            lblOfferPanel.Layer.BorderColor = BorderColor.CGColor;
            lblOfferPanel.Layer.BorderWidth = 1;
            try
            {
               this.View.BackgroundColor = ColorExtensions.ToUIColor(GeneralSettings.BackgroundColor); // Color.FromHex("#00FF00").ToUIColor();
                
            }catch(Exception eee)
            {

            }
            //Ska inte synas för Joyces
            //
            //offerCodeQR.Image = IosHelper.GetQrCode(IosHelper.getNSUserDefaults("offerCode"), 120, 120, 0);
            //offerCodeQRHolder.Layer.CornerRadius = 10f;
            //offerCodeQRHolder.Layer.MasksToBounds = true;
            //lblCode.Text = IosHelper.getNSUserDefaults("offerCode");
        }
    }
}