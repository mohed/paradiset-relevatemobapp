using Joyces.iOS.Model;
using FFImageLoading;
using Foundation;
using System;
using UIKit;
using Joyces.iOS.Helpers;

namespace Joyces.iOS
{
    public partial class NewsfeedCell : UITableViewCell
    {
        public NewsfeedCell (IntPtr handle) : base (handle)
        {
        }

        internal void UpdateCell(Joyces.News news)
        {
            //Todo: när Joyces har sett till att ladda upp en korrekt bild, ändra om så den tankas ner från offer.imageUrl
            //if (!string.IsNullOrEmpty(news.imageUrl))
            //    NewsImage.Image = IosHelper.GetImageFromUrl(news.imageUrl);

            //ImageService.Instance.LoadUrl("http://blog.studiomado.it/wp-content/uploads/certificazione-google-analytics3-345x190.png").Into(NewsImage);

            //NewsImage.Image = IosHelper.GetImageFromUrl("http://blog.studiomado.it/wp-content/uploads/certificazione-google-analytics3-345x190.png");

            if (!string.IsNullOrEmpty(news.imageUrl))
                ImageService.Instance.LoadUrl(news.imageUrl).Into(NewsImage);


            NewsHeadline.Text = news.name;
            NewsBody.Text = news.note;
            
        }
        private void setCurrentClientTheme()
        {
            try
            {
                // lblQRCodeBackground.BackgroundColor = IosHelper.FromHexString(GeneralSettings.BackgroundColor);
                if (Joyces.GeneralSettings.MainFont != null)
                {
                    NewsHeadline.Font = ThemeHelperIOS.GetThemeFont(17);
                    NewsBody.Font = ThemeHelperIOS.GetThemeFont(15);
                }
                //try
                //{
                //    this.View.BackgroundColor = ColorExtensions.ToUIColor(GeneralSettings.BackgroundColor); // Color.FromHex("#00FF00").ToUIColor();
                //}
                //catch (Exception eee)
                //{

                //}
            }
            catch (Exception e)
            {

            }
        }


    }
}