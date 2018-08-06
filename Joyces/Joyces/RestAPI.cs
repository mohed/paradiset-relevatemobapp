using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.IO;
using ModernHttpClient;

namespace Joyces
{

    public class UpdateUserModel
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string mobile { get; set; }
    }

    public class RegisterModel
    {
        public string email { get; set; }
        public string firstName { get; set; }
        public bool isMainMember { get; set; }
        public bool isMember { get; set; }
        public string lastName { get; set; }
        public string mobile { get; set; }

        public string personalIdNumber { get; set; }
    }

    public class AbalonErrors
    {
        public string code { get; set; }
        public string description { get; set; }
        public string message { get; set; }
        public string error_description { get; set; }
        public string error { get; set; }
    }

    public class TokenModel
    {
        public string access_token { get; set; }
        public string refresh_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
    }

    public class ApplicationTokenModel
    {
        public string access_token { get; set; }
        public string scope { get; set; }
        public string id_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
    }

    public class RestAPI
    {
        static string sCustomerId = Platform.AppContext.Instance.Platform.CustomerId;
        static string sURI =  Platform.AppContext.Instance.Platform.WebServiceURL;//"https://testparadiset.abalonrelevate.se/api/";//
        static string sUsername = Platform.AppContext.Instance.Platform.WebServiceUserName;//"joaxws";// 
        static string sPassword = Platform.AppContext.Instance.Platform.WebServicePassWord;//"Paradisettest1";// 

        static string sClientId = Platform.AppContext.Instance.Platform.ClientId;
        static string sClientSeacret = Platform.AppContext.Instance.Platform.ClientSecret;
        static string sAuthorize = Platform.AppContext.Instance.Platform.Authorize_uri;
        static string sTokenURI = Platform.AppContext.Instance.Platform.Token_uri;

        static TokenModel tokenModel;

        public RestAPI()
        {

        }


        private static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static async Task<bool> ForgotPassword(string sEmail)
        {
            try
            {
                var client = new HttpClient(new NativeMessageHandler());
                //var client = new HttpClient();

                var request = new HttpRequestMessage(HttpMethod.Post, sURI + "user/password/reset");


                var json = "{\"username\": \"" + sEmail + "\"}";

                using (var content = new StringContent(json, Encoding.UTF8, "application/json"))
                {
                    request.Content = content;
                    var response = await client.SendAsync(request).ConfigureAwait(false);

                    response.EnsureSuccessStatusCode();

                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static async Task<String> UpdateUser(string sEmail, string sFirstName, string sLastName, string sMobileNo, string sUserToken)
        {

            try
            {
                //var client = new HttpClient();
                var client = new HttpClient(new NativeMessageHandler());

                setBearerAuthenticationHttpClient(client, sUserToken);

                var request = new HttpRequestMessage(HttpMethod.Put, sURI + "customers/" + sEmail);

                UpdateUserModel u = new UpdateUserModel();

                u.firstName = sFirstName;
                u.lastName = sLastName;
                u.mobile = sMobileNo;

                var json = JsonConvert.SerializeObject(u);

                using (var content = new StringContent(json, Encoding.UTF8, "application/json"))
                {
                    request.Content = content;
                    var response = await client.SendAsync(request).ConfigureAwait(false);

                    if (response.StatusCode == HttpStatusCode.Conflict ||
                        response.StatusCode == HttpStatusCode.BadRequest ||
                        response.StatusCode == HttpStatusCode.InternalServerError)
                    {
                        var error = await response.Content.ReadAsStringAsync();
                        var errorMsg = JsonConvert.DeserializeObject<AbalonErrors>(error);

                        return errorMsg.message;
                    }

                    response.EnsureSuccessStatusCode();

                    return "";
                }
            }
            catch (Exception ex)
            {
                var response = await refreshTokenModel(Helpers.Settings.RefreshToken);
                return Lang.UNEXPECTED_ERROR;
            }
        }

        public static async Task<Object>  RegisterUser(string sEmail, string sFirstName, string sLastName, bool isMainMember, bool bIsMember, string sMobileNo, string sCustomerPassword, string sAppAccessToken)
        {
            try
            {
                //var client = new HttpClient();
                var client = new HttpClient(new NativeMessageHandler());

                setBearerAuthenticationHttpClient(client, sAppAccessToken);

                string sBase64Password = Base64Encode(sCustomerPassword);

                client.DefaultRequestHeaders.Add("X-User-Credentials", sBase64Password);

                var request = new HttpRequestMessage(HttpMethod.Post, sURI + "customers");

                RegisterModel reg = new RegisterModel();

                reg.email = sEmail;
                reg.firstName = sFirstName;
                reg.lastName = sLastName;
                reg.isMainMember = isMainMember;
                reg.isMember = bIsMember;
                reg.mobile = sMobileNo;

                var json = JsonConvert.SerializeObject(reg);

                using (var content = new StringContent(json, Encoding.UTF8, "application/json"))
                {
                    request.Content = content;

                    var response = await client.SendAsync(request).ConfigureAwait(false);


                    if (response.StatusCode == HttpStatusCode.Conflict ||
                        response.StatusCode == HttpStatusCode.BadRequest ||
                        response.StatusCode == HttpStatusCode.InternalServerError)
                    {
                        var localJson = await response.Content.ReadAsStringAsync();
                        var errors = JsonConvert.DeserializeObject<AbalonErrors>(localJson);

                        if (errors != null)
                            return errors;
                    }
                    else
                    {
                        var localJson = await response.Content.ReadAsStringAsync();
                        var customer = JsonConvert.DeserializeObject<Customer>(localJson);

                        if (customer != null)
                            return customer;
                    }

                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static async Task<Object> RegisterUserWithPersonalNumber(string sEmail, string sPersNr, bool isMainMember, bool bIsMember, string sMobileNo, string sCustomerPassword, string sAppAccessToken)
        {
            try
            {
                //var client = new HttpClient();
                var client = new HttpClient(new NativeMessageHandler());

                setBearerAuthenticationHttpClient(client, sAppAccessToken);

                string sBase64Password = Base64Encode(sCustomerPassword);

                client.DefaultRequestHeaders.Add("X-User-Credentials", sBase64Password);

                var request = new HttpRequestMessage(HttpMethod.Post, sURI + "customers");

                RegisterModel reg = new RegisterModel();

                reg.email = sEmail;
                reg.personalIdNumber = sPersNr;
                reg.isMainMember = isMainMember;
                reg.isMember = bIsMember;
                reg.mobile = sMobileNo;
                reg.firstName = "";
                reg.lastName = "";

                var json = JsonConvert.SerializeObject(reg);

                using (var content = new StringContent(json, Encoding.UTF8, "application/json"))
                {
                    request.Content = content;

                    var response = await client.SendAsync(request).ConfigureAwait(false);


                    if (response.StatusCode == HttpStatusCode.Conflict ||
                        response.StatusCode == HttpStatusCode.BadRequest ||
                        response.StatusCode == HttpStatusCode.InternalServerError)
                    {
                        var localJson = await response.Content.ReadAsStringAsync();
                        var errors = JsonConvert.DeserializeObject<AbalonErrors>(localJson);

                        if (errors != null)
                            return errors;
                    }
                    else
                    {
                        var localJson = await response.Content.ReadAsStringAsync();
                        var customer = JsonConvert.DeserializeObject<Customer>(localJson);

                        if (customer != null)
                            return customer;
                    }

                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static async Task<ApplicationTokenModel> GetApplicationToken()
        {
            try
            {
                //var client = new HttpClient();
                var client = new HttpClient(new NativeMessageHandler());

                setBasicAuthenticationHttpClient(client, sClientId, sClientSeacret);

                List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();

                list.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));
                list.Add(new KeyValuePair<string, string>("scope", "openid"));

                var content = new FormUrlEncodedContent(list);

                var Response = await client.PostAsync(sTokenURI, content);

                Response.EnsureSuccessStatusCode();

                var json = await Response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<ApplicationTokenModel>(json);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static async Task<Object> GetUserToken(string sUsername, string sPassword)
        {
            try
            {
                //var client = new HttpClient();
                var client = new HttpClient(new NativeMessageHandler());

                setBasicAuthenticationHttpClient(client, sClientId, sClientSeacret);


                List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();

                list.Add(new KeyValuePair<string, string>("grant_type", "password"));
                list.Add(new KeyValuePair<string, string>("username", sUsername));
                list.Add(new KeyValuePair<string, string>("password", sPassword));
                list.Add(new KeyValuePair<string, string>("scope", "openid"));

                var content = new FormUrlEncodedContent(list);

                var Response = await client.PostAsync(sTokenURI, content);

                var json = await Response.Content.ReadAsStringAsync();

                if (Response.StatusCode == HttpStatusCode.Unauthorized ||
                    Response.StatusCode == HttpStatusCode.BadRequest ||
                    Response.StatusCode == HttpStatusCode.InternalServerError)
                {
                    var error = JsonConvert.DeserializeObject<AbalonErrors>(json);

                    if (error != null)
                        return error;
                }

                Response.EnsureSuccessStatusCode();

                tokenModel = JsonConvert.DeserializeObject<TokenModel>(json);

                Helpers.Settings.RefreshToken = tokenModel.refresh_token;
                Helpers.Settings.AccessToken = tokenModel.access_token;

                return tokenModel;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public static async Task<HttpResponseMessage> PostDeviceInformation(string sAppId, string sAppVersion, string sDeviceOS,
            string sUUID, string sName, string sPlatform, string sToken, string sUserToken, string sCustomerId)
        {
            //"appId":          "com.informationfactory.abalon"
            //"appVersion":     Applikationsversion
            //"deviceOs":       Information om vilket OS + version
            //"deviceUid":      Unikt id för denna device och app – Nyckel för att hantera Deviceposter
            //"name":           Kundnamnet
            //"platform":       i vilken plattform sker notifiering -APN / FCM
            //"token":          Den faktiska pushtoken


            Push p = new Push();
            //HttpClient client = new HttpClient();
            var client = new HttpClient(new NativeMessageHandler());

            setBearerAuthenticationHttpClient(client, sUserToken);

            p.appId = sAppId;
            p.appVersion = sAppVersion;
            p.deviceOs = sDeviceOS;
            p.customerId = sCustomerId;
            p.deviceUid = sUUID;
            p.name = sName;
            p.platform = sPlatform;
            p.token = sToken;

            var response = await client.PostAsync(sURI + "devices", new StringContent(JsonConvert.SerializeObject(p), Encoding.UTF8, "application/json"));

            return response;
        }

        public static async Task<Object> GetCustomer(string sEmail, string sUserToken)
        {
            try
            {
                //var client = new HttpClient();
                var client = new HttpClient(new NativeMessageHandler());

                setBearerAuthenticationHttpClient(client, sUserToken);

                var Response = await client.GetAsync(sURI + "customers/" + sEmail);

                var json = await Response.Content.ReadAsStringAsync();

                if (Response.IsSuccessStatusCode)
                {
                    var customer = JsonConvert.DeserializeObject<Customer>(json);
                    return customer;
                }
                else if (Response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    var response = await refreshTokenModel(Helpers.Settings.RefreshToken);
                    //var response = await refreshTokenModel("ebdaa744-2bb2-3acd-ada2-9bb896eba3f4");

                    if (response != null && response is TokenModel)
                    {
                        Helpers.Settings.AccessToken = ((TokenModel)response).access_token;
                        Helpers.Settings.AccessTokenExpiration = ((TokenModel)response).expires_in.ToString();
                        return await GetCustomer(sEmail, Helpers.Settings.AccessToken);
                    }
                    else if (response != null && response is AbalonErrors)
                        return response;
                    else
                        return null;
                }
                else if (Response.StatusCode == HttpStatusCode.NotFound ||
                    Response.StatusCode == HttpStatusCode.InternalServerError)
                {
                    var error = JsonConvert.DeserializeObject<AbalonErrors>(json);

                    if (error != null)
                        return error;
                }

                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static async Task<Object> GetOffer(string sEmail, string sUserToken)
        {
            try
            {
                //var client = new HttpClient();
                var client = new HttpClient(new NativeMessageHandler());

                setBearerAuthenticationHttpClient(client, sUserToken);
             //  System.Diagnostics.Debug.WriteLine("================OFFERS  BEFORE CALL ================ " + DateTime.Now.ToString("HH:mm:ss.fff"));

                var Response = await client.GetAsync(sURI + "customers/" + sEmail + "/offers?ignorePurchaseTerms=true");
            //    System.Diagnostics.Debug.WriteLine("================OFFERS  AFTER CALL ================ " + DateTime.Now.ToString("HH:mm:ss.fff"));

                var json = await Response.Content.ReadAsStringAsync();
              //  System.Diagnostics.Debug.WriteLine("================OFFERS  AFTER READ AS STRING ASYNC ================ " + DateTime.Now.ToString("HH:mm:ss.fff"));

                if (Response.IsSuccessStatusCode)
                {
                    var offer = JsonConvert.DeserializeObject<List<Offer>>(json);
               //     System.Diagnostics.Debug.WriteLine("================OFFERS  BEFORE RETURN ================ " + DateTime.Now.ToString("HH:mm:ss.fff"));

                    return offer.OrderByDescending(o => o.effectiveDate).ToList();
                }
                if (Response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    var response = await refreshTokenModel(Helpers.Settings.RefreshToken);

                    if (response != null && response is TokenModel)
                    {
                        Helpers.Settings.AccessToken = ((TokenModel)response).access_token;
                        return await GetOffer(sEmail, Helpers.Settings.AccessToken);
                    }
                    else if (response != null && response is AbalonErrors)
                        return response;
                    else
                        return null;
                }
                else if (Response.StatusCode == HttpStatusCode.NotFound ||
                    Response.StatusCode == HttpStatusCode.InternalServerError)
                {
                    var error = JsonConvert.DeserializeObject<AbalonErrors>(json);

                    if (error != null)
                        return error;
                }

                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static async Task<Object> GetNews(string sEmail, string sUserToken)
        {
            try
            {

                //var client = new HttpClient();
                var client = new HttpClient(new NativeMessageHandler());

                setBearerAuthenticationHttpClient(client, sUserToken);

                var Response = await client.GetAsync(sURI + "customers/" + sEmail + "/news/");

                var json = await Response.Content.ReadAsStringAsync();
               // System.Diagnostics.Debug.WriteLine("================  NEWS AFTER CALL ================ " + DateTime.Now.ToString("HH:mm:ss.fff"));

                if (Response.IsSuccessStatusCode)
                {
                    var news = JsonConvert.DeserializeObject<List<News>>(json);

                    return news.OrderByDescending(o => o.startDate).ToList();
                }
                if (Response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    var response = await refreshTokenModel(Helpers.Settings.RefreshToken);

                    if (response != null && response is TokenModel)
                    {
                        Helpers.Settings.AccessToken = ((TokenModel)response).access_token;
                        return await GetNews(sEmail, Helpers.Settings.AccessToken);
                    }
                    else if (response != null && response is AbalonErrors)
                        return response;
                    else
                        return null;

                }
                else if (Response.StatusCode == HttpStatusCode.NotFound ||
                    Response.StatusCode == HttpStatusCode.InternalServerError)
                {
                    var error = JsonConvert.DeserializeObject<AbalonErrors>(json);

                    if (error != null)
                        return error;
                }

                return null;

            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public static async Task<List<More>> GetMore(string sUserToken)
        {
            try
            {
               // System.Diagnostics.Debug.WriteLine("================  MORE BFORE CALL ================ " + DateTime.Now.ToString("HH:mm:ss.fff"));

                //var client = new HttpClient();
                var client = new HttpClient(new NativeMessageHandler());
                

                setBearerAuthenticationHttpClient(client, sUserToken);

                // var json = await client.GetStringAsync(sURI + "companyInfo");
                var response = await client.GetAsync(sURI + "companyInfo");
                JsonSerializer _serializer = new JsonSerializer();
                List<More> more = null;
                using (var stream = await response.Content.ReadAsStreamAsync())
                using (var reader = new StreamReader(stream))
                using (var json = new JsonTextReader(reader))
                {
                    more = _serializer.Deserialize<List<More>>(json);
                }
                //more = JsonConvert.DeserializeObject<List<More>>(json);
               // System.Diagnostics.Debug.WriteLine("================  MORE AFTER CALL ================ " + DateTime.Now.ToString("HH:mm:ss.fff"));

                return more.OrderBy(o => o.lineNumber).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private static void setBearerAuthenticationHttpClient(HttpClient client, string sToken)
        {
            try
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + sToken);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static void setBasicAuthenticationHttpClient(HttpClient client, string sUsername, string sPassword)
        {
            try
            {
                var byteArray = Encoding.UTF8.GetBytes(sUsername + ":" + sPassword);
                string strAuth = Convert.ToBase64String(byteArray);
                NetworkCredential n = new NetworkCredential(sUsername, sPassword);
                string userCredentials = String.Format("{0}:{1}", sUsername, sPassword);

                byte[] bytes = Encoding.UTF8.GetBytes(userCredentials);

                string base64 = Convert.ToBase64String(bytes);
                client.DefaultRequestHeaders.Add("Authorization", "Basic " + base64);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        async private static Task<Object> refreshTokenModel(string sRefreshToken)
        {
            try
            {
                var client = new HttpClient();

                setBasicAuthenticationHttpClient(client, sClientId, sClientSeacret);

                List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();

                list.Add(new KeyValuePair<string, string>("grant_type", "refresh_token"));
                list.Add(new KeyValuePair<string, string>("scope", "openid"));
                list.Add(new KeyValuePair<string, string>("refresh_token", sRefreshToken));

                var content = new FormUrlEncodedContent(list);

                var Response = await client.PostAsync(sTokenURI, content);

                var json = await Response.Content.ReadAsStringAsync();

                if (Response.StatusCode == HttpStatusCode.Unauthorized ||
                    Response.StatusCode == HttpStatusCode.BadRequest ||
                    Response.StatusCode == HttpStatusCode.InternalServerError)
                {
                    var error = JsonConvert.DeserializeObject<AbalonErrors>(json);

                    if (error != null)
                        return error;
                }

                Response.EnsureSuccessStatusCode();

                tokenModel = JsonConvert.DeserializeObject<TokenModel>(json);

                Helpers.Settings.RefreshToken = tokenModel.refresh_token;
                Helpers.Settings.AccessToken = tokenModel.access_token;

                return tokenModel;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static async Task<bool> RefreshTokenInBackground(string sEmail)
        {
            try
            {
                var response = await refreshTokenModel(Helpers.Settings.RefreshToken);
                bool bRefreshed = false;
                if (response != null && response is TokenModel)
                {
                    Helpers.Settings.AccessToken = ((TokenModel)response).access_token;
                     Helpers.Settings.AccessTokenExpiration = ((TokenModel)response).expires_in.ToString();
                    Helpers.Settings.RefreshToken = ((TokenModel)response).refresh_token;
                    bRefreshed = true;
                    return bRefreshed;
                }
                else if (response != null && response is AbalonErrors)
                    return bRefreshed;
                else
                    return bRefreshed;
            }
            catch(Exception e)
            {
                return false;
            }


        }
    }

}

