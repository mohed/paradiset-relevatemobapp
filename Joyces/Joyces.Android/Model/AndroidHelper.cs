using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Graphics;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Android.Preferences;
using Android.Content.PM;

namespace Joyces.Droid.Model
{
    public class AndroidHelper
    {


        public static String GetAppVersion()
        {
            try
            {
                string sPackageName = Application.Context.ApplicationContext.PackageName;
                return Application.Context.ApplicationContext.PackageManager.GetPackageInfo(sPackageName, 0).VersionName;

            }
            catch (Exception e)
            {
                return "n/a";
            }
        }

        public static void setNSUserDefaults(string sKey, string sData, Context sContext)
        {
            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(sContext);
            ISharedPreferencesEditor editor = prefs.Edit();
            editor.PutString(sKey, sData);
            editor.Commit();
            editor.Apply(); // applies changes asynchronously on newer APIs
        }

        public static string getNSUserDefaults(string sKey, Context sContext)
        {
            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(sContext);
            return prefs.GetString(sKey, "");
        }

        public static Android.Graphics.Bitmap GetQrCode(string sData, int iWidth, int iHeight, int iMargin = 0)
        {
            var barcodeWriter = new ZXing.Mobile.BarcodeWriter
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

        public static async Task<Bitmap> GetImageFromUrlAsync(string url)
        {
            Bitmap imageBitmap = null;

            using (var webClient = new WebClient())
            {
                var imageBytes = await webClient.DownloadDataTaskAsync(new Uri(url));
                if (imageBytes != null && imageBytes.Length > 0)
                {
                    imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                }
            }

            return imageBitmap;
        }



        public static Bitmap GetImageFromUrl(string url)
        {
            try
            {
                Bitmap imageBitmap = null;

                using (var webClient = new WebClient())
                {
                    var imageBytes = webClient.DownloadData(url);
                    if (imageBytes != null && imageBytes.Length > 0)
                    {
                        imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                    }
                }

                return imageBitmap;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}