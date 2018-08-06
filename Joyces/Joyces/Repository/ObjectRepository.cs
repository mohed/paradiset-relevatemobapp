using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;



namespace Joyces.Repository
{
    public class ObjectRepository
    {

        public static bool EmailValidator(string sEmail)
        {
            return Regex.IsMatch(sEmail, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
        }

        public static string parseOfferValue(Offer offer)
        {
            try
            {
                string sValueText = string.Empty;

                if (offer.unit == "Amount")
                {
                    if (offer.currency != null && !string.IsNullOrEmpty(offer.currency.code))
                        sValueText = offer.value.ToString() + " " + offer.currency.code;
                    else
                        sValueText = offer.value.ToString();

                }
                else if (offer.unit == "%")
                    sValueText = offer.value.ToString() + "%";
                else
                    sValueText = offer.value.ToString();

                return sValueText;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        public static string ParseDateTimeToCulture(DateTime? dt)
        {
            try
            {
                var culture = new CultureInfo("sv-SE");// CultureInfo.CurrentCulture; //new CultureInfo("en-US");
                var pattern = culture.DateTimeFormat.ShortDatePattern + " " + culture.DateTimeFormat.ShortTimePattern;
                var newDateTime = dt.Value.ToString(pattern);
                //var newPattern = pattern.Replace("h", "H").Replace("t", "");

                return newDateTime;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        //public static bool asd()
        //{
           
        //}
    }
}
