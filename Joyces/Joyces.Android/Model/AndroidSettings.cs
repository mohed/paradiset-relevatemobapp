namespace Joyces.Droid.Model
{
    class AndroidSettings
    {
        public static void SetGeneralSetting()
        {
            Joyces.Platform.AppContext.Instance.Platform.WebServiceURL = "https://testparadiset.abalonrelevate.se/api/v1/";

            Joyces.Platform.AppContext.Instance.Platform.WebServiceUserName = "vismawsu";
            Joyces.Platform.AppContext.Instance.Platform.WebServicePassWord = "vismawsu";
            Joyces.Platform.AppContext.Instance.Platform.SenderId = "970515158925";

            Joyces.Platform.AppContext.Instance.Platform.ClientId = "YrzDAbapWkCOZQ5MC426Taocf2Ma";
            Joyces.Platform.AppContext.Instance.Platform.ClientSecret = "3L73kuNO7spmUd7CugPRL3A_Hr8a";
            Joyces.Platform.AppContext.Instance.Platform.Authorize_uri = "https://wso2testparadiset.abalonrelevate.se/oauth2/authorize";//"https://wso2joyces.abalonrelevate.se/oauth2/authorize";
            Joyces.Platform.AppContext.Instance.Platform.Token_uri = "https://wso2testparadiset.abalonrelevate.se/oauth2/token";//"https://wso2joyces.abalonrelevate.se/oauth2/token"; //
            Joyces.Helpers.Settings.MainFont = "Montserrat-Regular.ttf";

        }
    }
}