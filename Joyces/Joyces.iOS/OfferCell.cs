using Joyces;
using Joyces.iOS.Model;
using Joyces.Repository;
using FFImageLoading;
using Foundation;
using System;
using UIKit;
using System.Globalization;
using Joyces.iOS.Helpers;

namespace Joyces.iOS
{
    public partial class OfferCell : UITableViewCell
    {
        public OfferCell (IntPtr handle) : base (handle)
        {
        }

        internal void UpdateCell(Offer offer)
        {
            //Todo: när Abalon har sett till att ladda upp en korrekt bild, ändra om så den tankas ner från offer.imageUrl
            //if (!string.IsNullOrEmpty(offer.imageUrl))
            //    OfferImage.Image = IosHelper.GetImageFromUrl(offer.imageUrl);

            //OfferImage.Image = IosHelper.GetImageFromUrl("http://thetoddlerfoodies.com/wp-content/uploads/2015/11/77006E81-EA99-42A5-AD88-DE129E272521-345x190.jpg");

            //ImageService.Instance.LoadUrl("http://thetoddlerfoodies.com/wp-content/uploads/2015/11/77006E81-EA99-42A5-AD88-DE129E272521-345x190.jpg").Into(OfferImage);


            if (!string.IsNullOrEmpty(offer.imageUrl))
                ImageService.Instance.LoadUrl(offer.imageUrl).Into(OfferImage);
            else
                OfferImage.Image = UIImage.FromBundle("Images/Placeholder");

            OfferHeadline.Text = offer.name;
            OfferBody.Text = offer.note;
            OfferValue.Text = ObjectRepository.parseOfferValue(offer);

            //DateTime test = new DateTime(2017, 7, 11, 16, 55, 33);
            //TimeSpan timespan = new TimeSpan(03, 00, 00);
            //DateTime time = DateTime.Today.Add(timespan);
            //string displayTime = time.ToString("hh:mm tt");

            //var d = DateTime.Parse(test.ToString(), CultureInfo.InvariantCulture);
            //offerValidDate.Text = "Valid until " + d.ToString() + " " + displayTime;

            ////offerValidDate.Text = "Valid until " + test.ToString().Substring(0, 10) + " " + displayTime;



            offerValidDate.Text = Lang.VALID_UNTIL +  " " + ObjectRepository.ParseDateTimeToCulture(offer.validityDate);

            //OfferImage.Layer.CornerRadius = 0f;
            //OfferImage.Layer.MasksToBounds = true;
            //OfferImage.Layer.BorderColor = UIColor.Red.CGColor;
            //OfferImage.Layer.BorderWidth = 3f;
            //OfferImage.BorderStyle = UITextBorderStyle.None;
            setCurrentClientTheme();
        }
        private void setCurrentClientTheme()
        {
            try
            {
                // lblQRCodeBackground.BackgroundColor = IosHelper.FromHexString(GeneralSettings.BackgroundColor);
                if (Joyces.GeneralSettings.MainFont != null)
                {
                    OfferHeadline.Font = ThemeHelperIOS.GetThemeFont(17);
                    OfferValue.Font = ThemeHelperIOS.GetThemeFont(15);
                    OfferBody.Font = ThemeHelperIOS.GetThemeFont(13);
                    offerValidDate.Font = ThemeHelperIOS.GetThemeFont(13);
                }

            }
            catch (Exception e)
            {

            }
        }
    }
}