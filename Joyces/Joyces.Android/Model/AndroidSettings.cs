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

namespace Joyces.Droid.Model
{
    class AndroidSettings
    {
        public static void SetGeneralSetting()
        {
            Joyces.Platform.AppContext.Instance.Platform.WebServiceURL = "https://testparadiset.abalonrelevate.se/api/v1/";//"https://joyces.abalonrelevate.se/api/v1/"; // "https://joyces.abalonrelevate.se/api/v1/"; //

            Joyces.Platform.AppContext.Instance.Platform.WebServiceUserName = "mohedrest";//"vismawsu";//"vismawsu";//
            Joyces.Platform.AppContext.Instance.Platform.WebServicePassWord = "stockholm";//"c2RmbHNkamVyZm1vZMO2b3k=";//  "c2RmbHNkamVyZm1vZMO2b3k=";//
            Joyces.Platform.AppContext.Instance.Platform.SenderId = "970515158925";

            Joyces.Platform.AppContext.Instance.Platform.ClientId = "YrzDAbapWkCOZQ5MC426Taocf2Ma";
            Joyces.Platform.AppContext.Instance.Platform.ClientSecret = "3L73kuNO7spmUd7CugPRL3A_Hr8a";
            Joyces.Platform.AppContext.Instance.Platform.Authorize_uri = "https://wso2testparadiset.abalonrelevate.se/oauth2/authorize";//"https://wso2joyces.abalonrelevate.se/oauth2/authorize"; //"https://wso2joyces.abalonrelevate.se/oauth2/authorize";
            Joyces.Platform.AppContext.Instance.Platform.Token_uri = "https://wso2testparadiset.abalonrelevate.se/oauth2/token";//"https://wso2joyces.abalonrelevate.se/oauth2/token"; //
            Joyces.Helpers.Settings.MainFont = "Montserrat-Regular.ttf";

            //Joyces.Platform.AppContext.Instance.Platform.WebServiceURL = "https://paradiset.abalonrelevate.se/api/v1/";//"https://joyces.abalonrelevate.se/api/v1/"; // "https://joyces.abalonrelevate.se/api/v1/"; //

            //Joyces.Platform.AppContext.Instance.Platform.WebServiceUserName = "joaxws";//"vismawsu";//"vismawsu";//
            //Joyces.Platform.AppContext.Instance.Platform.WebServicePassWord = "Paradiset@2018";// "Paradisettest1";//"c2RmbHNkamVyZm1vZMO2b3k=";//  "c2RmbHNkamVyZm1vZMO2b3k=";//
            //Joyces.Platform.AppContext.Instance.Platform.SenderId = "970515158925";

            //Joyces.Platform.AppContext.Instance.Platform.ClientId = "YrzDAbapWkCOZQ5MC426Taocf2Ma";
            //Joyces.Platform.AppContext.Instance.Platform.ClientSecret = "3L73kuNO7spmUd7CugPRL3A_Hr8a";
            //Joyces.Platform.AppContext.Instance.Platform.Authorize_uri = "https://wso2paradiset.abalonrelevate.se/oauth2/authorize";//"https://wso2joyces.abalonrelevate.se/oauth2/authorize"; //"https://wso2joyces.abalonrelevate.se/oauth2/authorize";
            //Joyces.Platform.AppContext.Instance.Platform.Token_uri = "https://wsoparadiset.abalonrelevate.se/oauth2/token";//"https://wso2joyces.abalonrelevate.se/oauth2/token"; //
            //Joyces.Helpers.Settings.MainFont = "Montserrat-Regular.ttf";
            // Joyces.Helpers.Settings.MainFont = "Oswald-Stencbab.ttf";


        }
    }
}