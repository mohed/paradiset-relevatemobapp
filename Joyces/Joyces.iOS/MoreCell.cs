using Joyces;
using Joyces.iOS.Model;
using FFImageLoading;
using Foundation;
using System;
using UIKit;
using Joyces.iOS.Helpers;

namespace Joyces.iOS
{
    public partial class MoreCell : UITableViewCell
    {
        public MoreCell(IntPtr handle) : base(handle)
        {
        }
        internal void UpdateCell(More more)
        {
            if (!string.IsNullOrEmpty(more.imageUrl))
                ImageService.Instance.LoadUrl(more.imageUrl).Into(MoreImage);

            moreHeadline.Text = more.desc;
            moreDescription.Text = more.note;
        }
        private void setCurrentClientTheme()
        {
             try
            {
               // lblQRCodeBackground.BackgroundColor = IosHelper.FromHexString(GeneralSettings.BackgroundColor);
                if (Joyces.GeneralSettings.MainFont != null)
                {
                    moreHeadline.Font = ThemeHelperIOS.GetThemeFont(19);
                    moreDescription.Font = ThemeHelperIOS.GetThemeFont(13);
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