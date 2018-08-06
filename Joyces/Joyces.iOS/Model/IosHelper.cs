using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using ZXing.Mobile;

namespace Joyces.iOS.Model
{
    class IosHelper
    {
        public static UIImage GetQrCode(string sData, int iWidth, int iHeight, int iMargin = 0)
        {
            var barcodeWriter = new BarcodeWriter
            {
                Format = ZXing.BarcodeFormat.QR_CODE,
                Options = new ZXing.Common.EncodingOptions
                {
                    Width = iWidth,
                    Height = iHeight,
                    Margin = iMargin,
                    PureBarcode = true
                }
            };

            var barcode = barcodeWriter.Write("!" + sData);

            return barcode;
        }

        public static UIImage GetImageFromUrl(string uri)
        {
            using (var url = new NSUrl(uri))
            using (var data = NSData.FromUrl(url))
                return UIImage.LoadFromData(data);
        }

        public static void setNSUserDefaults(string sKey, string sData)
        {
            NSUserDefaults.StandardUserDefaults.SetString(sData, sKey);
        }

        public static string getNSUserDefaults(string sKey)
        {
            return NSUserDefaults.StandardUserDefaults.StringForKey(sKey);
        }

        public static bool CheckTexfieldMaxLength(UITextField textField, NSRange range, string replacementString, int maxLength)
        {
            int newLength = (textField.Text.Length - (int)range.Length) + replacementString.Length;
            if (newLength <= maxLength)
            {
                return true;
            }
            else
            {
                if (range.Length == 0 && range.Location > 0 && replacementString.Length > 0 && textField.Text.Length >= maxLength)
                    return false;

                int emptySpace = maxLength - (textField.Text.Length - (int)range.Length);

                textField.Text = textField.Text.Substring(0, (int)range.Location)
                + replacementString.Substring(0, emptySpace)
                + textField.Text.Substring((int)range.Location + (int)range.Length, emptySpace >= maxLength ? 0 : (maxLength - (int)range.Location - emptySpace));
                return false;
            }
        }

        public static UIColor FromHexString(string hexValue)
        {
            var colorString = hexValue.Replace("#", "");
            float red, green, blue;

            switch (colorString.Length)
            {
                case 3: // #RGB
                    {
                        red = Convert.ToInt32(string.Format("{0}{0}", colorString.Substring(0, 1)), 16) / 255f;
                        green = Convert.ToInt32(string.Format("{0}{0}", colorString.Substring(1, 1)), 16) / 255f;
                        blue = Convert.ToInt32(string.Format("{0}{0}", colorString.Substring(2, 1)), 16) / 255f;

                        return UIColor.FromRGB(red, green, blue);
                    }
                case 6: // #RRGGBB
                    {
                        red = Convert.ToInt32(colorString.Substring(0, 2), 16) / 255f;
                        green = Convert.ToInt32(colorString.Substring(2, 2), 16) / 255f;
                        blue = Convert.ToInt32(colorString.Substring(4, 2), 16) / 255f;

                        return UIColor.FromRGB(red, green, blue);
                    }
                case 8: // #AARRGGBB
                    {
                        var alpha = Convert.ToInt32(colorString.Substring(0, 2), 16) / 255f;
                        red = Convert.ToInt32(colorString.Substring(2, 2), 16) / 255f;
                        green = Convert.ToInt32(colorString.Substring(4, 2), 16) / 255f;
                        blue = Convert.ToInt32(colorString.Substring(6, 2), 16) / 255f;

                        return UIColor.FromRGBA(red, green, blue, alpha);
                    }
                default:
                    throw new ArgumentOutOfRangeException(string.Format("Invalid color value {0} is invalid. It should be a hex value of the form #RBG, #RRGGBB, or #AARRGGBB", hexValue));

            }
        }
    }
}