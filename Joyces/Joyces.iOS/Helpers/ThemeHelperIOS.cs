using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

namespace Joyces.iOS.Helpers
{
    public class ThemeHelperIOS
    {

        public static UIFont GetThemeFont(float size)
        {
            string strFontName = Joyces.Helpers.Settings.MainFont; 
           // strFontName = "Montserrat-Regular";// Joyces.Helpers.Settings.MainFont;
            return UIFont.FromName(strFontName, size); 
        }
    }
}