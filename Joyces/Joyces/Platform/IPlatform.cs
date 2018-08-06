using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Joyces.Platform
{
    public interface IPlatform
    {
        //Relevate REST login
        string WebServiceUserName { get; set; }
        string WebServicePassWord { get; set; }
        string WebServiceURL { get; set; }

        //Google Cloud Messaging
        string SenderId { get; set; }

        //Relevate
        string CustomerId { get; set; }
        Customer CustomerList { get; set; }
        List<News> NewsList { get; set; }
        List<Offer> OfferList { get; set; }
        List<More> MoreList { get; set; }

        //WSO2 OAuth2
        string ClientId { get; set; }
        string ClientSecret { get; set; }
        string Authorize_uri { get; set; }
        string Token_uri { get; set; }
        string Redirect_uris { get; set; }

    }
}
