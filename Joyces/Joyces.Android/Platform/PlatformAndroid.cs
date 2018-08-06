using System.Collections.Generic;

using Android.Content;

using Joyces.Platform;


namespace Joyces.Droid.Platform
{
    class PlatformAndroid : IPlatform
    {
        private Context _context;

        public PlatformAndroid(Context context)
        {
            _context = context;
        }
        
        public string WebServiceUserName { get; set; }
        public string WebServicePassWord { get; set; }
        public string CustomerId { get; set; }
        public string WebServiceURL { get; set; }
        public List<News> NewsList { get; set; }
        public List<Offer> OfferList { get; set; }
        public List<More> MoreList { get; set; }
        public Customer CustomerList { get; set; }
        public string SenderId { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string Authorize_uri { get; set; }
        public string Token_uri { get; set; }
        public string Redirect_uris { get; set; }

        public static void InitAPIs(Context contex)
        {
            AppContext.Instance = new AppContext
            {
                Platform = new PlatformAndroid(contex)
            };
        }
    }
}