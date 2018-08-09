using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Joyces
{
    public class GeneralSettings
    {
        //TEST USER
        /////////////////////////////////////////////////////////////////////
        public static String TestUserUsername = "mohed@hotmail.com";
        public static String TestUserPassword = "stockholm";
        /////////////////////////////////////////////////////////////////////


        //Joyce's Supermarket (joyces.joycesupermarket.ie)
        /////////////////////////////////////////////////////////////////////
        /*
        /// <ManualChanges>
        /// Saker som manuellt måste ändras vid lansering:
        /// 1. AppID
        /// 2. Appikon för iOS (Görst under "Asset Catalog")
        /// 3. Versions nummer för båda OS
        /// </ManualChanges>
        /// 
        public static String TERM_AND_CONDITION_URL             = "http://joycesupermarket.ie/joyces-terms-conditions.html";
        public static String PRIVACY_POLICY_URL                 = "http://joycesupermarket.ie/joyces-privacy-policy.html";
        public static String Logo                               = "Images/logo_joyces.png";
        public static String LogoLogin = "Images/logo_joyces.png";
        public static String AppIcon                            = "Images/Icon.png";
        public static String BackgroundColor                    = "#1d1d1b";
        public static String ButtonBackgroundColor              = "#5daa30";
        public static String ButtonTextColor                    = "#ffffff";
        public static String NavigationTint                     = "#ffffff";    // Ikon outline för navigation
        public static String TabBarTint                         = "#5daa30";    // iOS: Ikon outline för tabbar
        public static String AndroidActionBarColor              = "#5daa30";    // Android: ActionBar färg
        public static bool AutoLogin                            = true;
        public static bool UseLoyaltyCard                       = false;
        public static String TelephoneNoMasking                 = "353";

        public static String MainFont = "Montserrat-Regular";
        */
        /////////////////////////////////////////////////////////////////////

        //NYTT PARADISET
        //Paradiset -> fixa nytt (joyces.joycesupermarket.ie)
        /////////////////////////////////////////////////////////////////////

        /// <ManualChanges>
        /// Saker som manuellt måste ändras vid lansering:
        /// 1. AppID
        /// 2. Appikon för iOS (Görst under "Asset Catalog")
        /// 3. Versions nummer för båda OS
        /// </ManualChanges>
        /// 
        public static String TERM_AND_CONDITION_URL = "http://joycesupermarket.ie/joyces-terms-conditions.html";
        public static String PRIVACY_POLICY_URL = "http://joycesupermarket.ie/joyces-privacy-policy.html";
        public static String LogoLogin = "Images/Logo_inside_paradiset_green.png";
        public static String Logo = "Images/Logo_inside_paradiset.png";
        public static String AppIcon = "Images/paradiset_logotyp_png.png";
        public static String BackgroundColorSpecific = "#e2f2e2";
        public static String BackgroundColor = "#f3f8f7";
        public static String ButtonBackgroundColor = "#009f4d";
        public static String ButtonTextColor = "#ffffff";
        public static String NavigationTint = "#009f4d";    // Ikon outline för navigation
        public static String TabBarTint = "#009f4d";//"#5daa30";    // iOS: Ikon outline för tabbar
        public static String AndroidActionBarColor = "#009f4d";//"#e2f2e2";//"#009f4d";//"#5daa30";    // Android: ActionBar färg
        public static bool AutoLogin = true;
        public static bool UseLoyaltyCard = false;
        public static String TelephoneNoMasking = "46";

        public static String MainFont = "Montserrat-Regular";
        /////////////////////////////////////////////////////////////////////
        
    }

    public class Lang
    {

        //Language
        /////////////////////////////////////////////////////////////////////
        //Joyces
        /*
        public static String BUTTON_OK                          = "OK";
        public static String BUTTON_OK_I_UNDERSTAND             = "OK, I understand";

        public static String ERROR_HEADLINE                     = "Error";
        public static String MESSAGE_HEADLINE                   = "Message";
        public static String INTERNET_CONNECTION_REQUIERED      = "An active internet connection is required.";
        public static String RESET_PASSWORD                     = "Reset password email has been sent.";
        public static String NO_EMAIL_FOUND                     = "We could not find your email in our system.";
        public static String UNEXPECTED_ERROR                   = "An unexpected error occurred. Please try again later or contact customer service.";
        public static String WRONG_PASSWORD                     = "Wrong username or password.";
        public static String SERVICE_NOT_AVAILABLE              = "This service is not available at this moment, please try again later.";
        public static String PROFILE_UPDATE                     = "Your user profile has been updated.";
        public static String ENTER_VALID_EMAIL                  = "Please enter a valid email address.";
        public static String ENTER_VALID_PASSWORD               = "Please enter a valid password.";
        public static String PASSWORD_NOT_MATCH                 = "Password does not match the confirm password.";
        public static String TERM_AND_CONDITIONS                = "Please read and accept our terms & conditions and privacy policy.";
        public static String ACTIVATION_EMAIL_SENT              = "An activation email has been sent. Please verify your account to access the app. Didn’t receive the verification email? Contact service@joycesfresh.com";//"An activation email has been sent to your email address.";
        public static String LOADING                            = "Loading Data...";
        public static String DEVICE_NOT_SUPPORTED               = "Sorry, this device is not supported.";
        public static String CONTACTING_SERVER_WAIT             = "Contacting server. Please wait...";
        public static String NOT_ACTIVATED_MEMBER               = "Please activate your account. An activation email has been sent to your email address.";
        public static String USER_HAS_LOGGED_OUT                = "You have been logged out due to inactivity.";
        */

        //Paradiset
        public static String APPLICATION_NAME = "Paradiset";
        public static String BUTTON_OK = "OK";
        public static String BUTTON_OK_I_UNDERSTAND = "OK, jag förstår";

        public static String ERROR_HEADLINE = "Felmeddelande";
        public static String MESSAGE_HEADLINE = "Meddelande";
        public static String INTERNET_CONNECTION_REQUIERED = "Internetuppkoppling behövs";
        public static String RESET_PASSWORD_LINK = "En återställningslänk har skickats till din e-post";
        public static String NO_EMAIL_FOUND = "E-postadress ej funnen";
        public static String UNEXPECTED_ERROR = "Ett oväntat fel har uppstått, kontakta support";
        public static String WRONG_PASSWORD = "Fel användarnamn eller lösenord.";
        public static String SERVICE_NOT_AVAILABLE = "Denna service är inte tillgänglig för tillfället, försök igen senare tack.";
        public static String PROFILE_UPDATE = "Din användarprofil har blivit uppdaterad.";
        public static String ENTER_VALID_EMAIL = "e-postadressen är inte giltig.";
        public static String ENTER_VALID_PASSWORD = "skriv in giltigt lösenord.";
        public static String PASSWORD_NOT_MATCH = "Lösenordet passar inte.";
        public static String TERM_AND_CONDITIONS = "Läs och acceptera våra användarvillkor och integritetspolicy.";
        public static String ACTIVATION_EMAIL_SENT = "En aktiveringslänk har skickats till din e-post. Verifiera ditt konto för att komma åt appen. Om du inte fick verifieringsmeddelandet kontakta oss på 08-6133600";//"An activation email has been sent to your email address.";
        public static String LOADING = "Laddar...";
        public static String DEVICE_NOT_SUPPORTED = "Tyvärr, den här enheten stöds inte.";
        public static String CONTACTING_SERVER_WAIT = "Kontaktar servern. Vänta...";
        public static String NOT_ACTIVATED_MEMBER = "Vänligen aktivera ditt konto. Ett aktiverings-e-postmeddelande har skickats till din e-postadress.";
        public static String USER_HAS_LOGGED_OUT = "Du har loggats ut på grund av inaktivitet.";
        //NYA

        public static String EMAIL = "E-post";
        public static String MOBILEPHONE = "Mobiltelefon";
        public static String PERSONALNUMBER = "Personnummer";


        public static String CREATE_ACCOUNT = "Skapa konto";
        public static String PASSWORD = "Lösenord";
        public static String PASSWORD_CONFIRM = "Bekräfta lösenord";
        public static String LOGIN = "Logga in";
        public static String LOGOUT = "Logga ut";
        public static String FORGOT_PASSWORD = "Glömt lösenord";
        public static String CREATE_ACCOUNT_TERMS_HEADER = "Villkorstext";
        public static String CREATE_ACCOUNT_TERMS = "– Lorem ipsum dolor sit amet, consectetur adipiscing elit." + System.Environment.NewLine +
                                                    "– Incididunt ut labore et dolore magna aliqua. Sollicitudin ac orci phasellus egestas." + System.Environment.NewLine +
                                                     "– Tellus rutrum. Ligula ullamcorper malesuada." + System.Environment.NewLine +
                                                        "– Tristique senectus et netus et malesuada fames ac. Fringilla est ullamcorper eget nulla.";
        public static String CREATE_ACCOUNT_ACCEPT_TERMS = "Jag har läst och accepterar villkoren";
        //ID
        public static String ID_DESCRIPTION = "Skanna din kod vid kassan";
        public static String ID_DESCRIPTION_NEWS = "Se våra senaste nyheter och erbjudanden nedan!";
        public static String ID_HEADER = "Din Kod";
        //MORE
        public static String MORE_HEADER = "Övrigt";
        //NEWS
        public static String NEWS_HEADER = "Nyheter";
        //OFFER
        public static String OFFER_HEADER = "Erbjudanden";
        //RESET PASSWORD
        public static String RESET_PASSWORD = "Återställ lösenord";
        //ACCOUNT
        public static String ACCOUNT_MY_ACCOUNT = "Mitt konto";
        public static String ACCOUNT_PERSONAL_DETAILS = "Min profil";
        public static String ACCOUNT_NAME = "Namn";
        public static String ACCOUNT_MOBILE = "Mobilnummer";
        //EDIT ACCOUNT
        public static String EDIT_ACCOUNT_FIRSTNAME = "Förnamn";
        public static String EDIT_ACCOUNT_LASTNAME = "Efternamn";
        public static String SAVE = "Spara";

        public static String VALID_UNTIL = "Giltig till";
    }


}
