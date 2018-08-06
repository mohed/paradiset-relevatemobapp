using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using Joyces;
using Joyces.Platform;

namespace Joyces.iOS.Platform
{
    class PlatformIos : IPlatform
    {
        

        public PlatformIos()
        {
            
        }

        private string webserviceusername;
        private string webservicepassword;
        private string webserviceurl;

        private string customerid;
        private Customer customerlist;
        private List<Joyces.News> newslist;
        private List<Offer> offerlist;
        private List<More> morelist;
        private string senderid;

        private string clientId;
        private string clientSecret;
        private string authorize_uri;
        private string token_uri;
        private string redirect_uris;


        public string WebServiceUserName
        {

            get { return webserviceusername; }

            set { webserviceusername = value; }

        }
        public string WebServicePassWord
        {
            get { return webservicepassword; }

            set { webservicepassword = value; }
        }

        public string CustomerId
        {
            get { return customerid; }

            set { customerid = value; }
        }

        public string WebServiceURL
        {

            get { return webserviceurl; }

            set { webserviceurl = value; }
        }

        public List<Joyces.News> NewsList
        {
            get { return newslist; }

            set { newslist = value; }
        }
        public List<Offer> OfferList
        {
            get { return offerlist; }

            set { offerlist = value; }
        }
        public List<More> MoreList
        {
            get { return morelist; }

            set { morelist = value; }
        }
        public Customer CustomerList
        {
            get { return customerlist; }

            set { customerlist = value; }
        }
        public string SenderId
        {
            get { return senderid; }

            set { senderid = value; }
        }

        public string ClientId
        {
            get { return clientId; }

            set { clientId = value; }
        }
        public string ClientSecret
        {
            get { return clientSecret; }

            set { clientSecret = value; }
        }
        public string Authorize_uri
        {
            get { return authorize_uri; }

            set { authorize_uri = value; }
        }
        public string Token_uri
        {
            get { return token_uri; }

            set { token_uri = value; }
        }
        public string Redirect_uris
        {
            get { return redirect_uris; }

            set { redirect_uris = value; }
        }


        public static void InitAPIs()
        {
            Joyces.Platform.AppContext.Instance = new Joyces.Platform.AppContext();
            Joyces.Platform.AppContext.Instance.Platform = new PlatformIos();
        }
    }
}