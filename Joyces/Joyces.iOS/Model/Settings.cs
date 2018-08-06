using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

namespace Joyces.iOS.Model
{
    class Settings
    {
        public Settings()
        {

        }

        public static void SetGeneralSetting()
        {
            Platform.PlatformIos.InitAPIs();

            //Joyces.Platform.AppContext.Instance.Platform.WebServiceURL = "https://joyces.abalonrelevate.se/api/v1/";

            //Joyces.Platform.AppContext.Instance.Platform.WebServiceUserName = "vismawsu";
            //Joyces.Platform.AppContext.Instance.Platform.WebServicePassWord = "c2RmbHNkamVyZm1vZMO2b3k=";

            //Joyces.Platform.AppContext.Instance.Platform.ClientId = "YZxWcx_ouU7gJThkz4akRouTz8Ma";
            //Joyces.Platform.AppContext.Instance.Platform.ClientSecret = "Stpvd4_Rf2MfoKpVW_KNq1QjB_Aa";
            //Joyces.Platform.AppContext.Instance.Platform.Authorize_uri = "https://wso2joyces.abalonrelevate.se/oauth2/authorize";
            //Joyces.Platform.AppContext.Instance.Platform.Token_uri = "https://wso2joyces.abalonrelevate.se/oauth2/token";


            //PARADISET
            Joyces.Platform.AppContext.Instance.Platform.WebServiceURL = "https://testparadiset.abalonrelevate.se/api/v1/";//"https://joyces.abalonrelevate.se/api/v1/"; // "https://joyces.abalonrelevate.se/api/v1/"; //

            Joyces.Platform.AppContext.Instance.Platform.WebServiceUserName = "joaxws";//"vismawsu";//"vismawsu";//
            Joyces.Platform.AppContext.Instance.Platform.WebServicePassWord = "Paradisettest1";//"c2RmbHNkamVyZm1vZMO2b3k=";//  "c2RmbHNkamVyZm1vZMO2b3k=";//
            Joyces.Platform.AppContext.Instance.Platform.SenderId = "970515158925";

            Joyces.Platform.AppContext.Instance.Platform.ClientId = "YrzDAbapWkCOZQ5MC426Taocf2Ma";
            Joyces.Platform.AppContext.Instance.Platform.ClientSecret = "3L73kuNO7spmUd7CugPRL3A_Hr8a";
            Joyces.Platform.AppContext.Instance.Platform.Authorize_uri = "https://wso2testparadiset.abalonrelevate.se/oauth2/authorize";//"https://wso2joyces.abalonrelevate.se/oauth2/authorize"; //"https://wso2joyces.abalonrelevate.se/oauth2/authorize";
            Joyces.Platform.AppContext.Instance.Platform.Token_uri = "https://wso2testparadiset.abalonrelevate.se/oauth2/token";//"https://wso2joyces.abalonrelevate.se/oauth2/token"; //
            //Joyces.Helpers.Settings.MainFont = "Montserrat-Regular.ttf";

        }

    }
}