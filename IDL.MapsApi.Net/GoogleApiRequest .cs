using System;
using System.Collections.Specialized;
using System.Configuration;

using IDL.MapsApi.Net.Google.Models;
using IDL.MapsApi.Net.Google.Request;

namespace IDL.MapsApi.Net
{
    public abstract class GoogleApiRequest : ApiRequest
    {
        protected GoogleApiRequest(string apiKey = null)
        {
            QueryParameters = new NameValueCollection { { "key", apiKey ?? ConfigurationManager.AppSettings.Get("GoogleMapsApiKey") } };
        }

        protected GoogleApiRequest(GoogleCredentials credentials)
        {
            var clientId = credentials?.ClientId ?? ConfigurationManager.AppSettings.Get("GoogleMapsClientId");
            var signature = credentials?.Signature ?? ConfigurationManager.AppSettings.Get("GoogleMapsSignature");
            var apiKey = credentials?.ApiKey ?? ConfigurationManager.AppSettings.Get("GoogleMapsApiKey");

            if (!string.IsNullOrEmpty(clientId) && !string.IsNullOrEmpty(signature))
            {
                QueryParameters = new NameValueCollection
                {
                    { "client", clientId },
                    { "signature", signature }
                };
            }
            else if (!string.IsNullOrEmpty(apiKey))
            {
                QueryParameters = new NameValueCollection { { "key", apiKey } };
            }
            else
            {
                throw new ArgumentException("You must specify a client id and signature or an api key");
            }
        }
    }
}
