using System;
using System.Configuration;

using IDL.MapsApi.Net.Google.Models;

namespace IDL.MapsApi.Net
{
    public abstract class GoogleApiRequest : ApiRequest
    {
        private readonly GoogleCredentials _credentials;

        protected GoogleApiRequest(string apiKey = null)
            : this(new GoogleCredentials(apiKey))
        {
        }

        protected GoogleApiRequest(GoogleCredentials credentials)
        {
            _credentials = credentials;
        }

        protected override void BuildQueryParameters()
        {
            var clientId = _credentials?.ClientId ?? ConfigurationManager.AppSettings.Get("GoogleMapsClientId");
            var signature = _credentials?.Signature ?? ConfigurationManager.AppSettings.Get("GoogleMapsSignature");
            var apiKey = _credentials?.ApiKey ?? ConfigurationManager.AppSettings.Get("GoogleMapsApiKey");

            if (!string.IsNullOrEmpty(clientId) && !string.IsNullOrEmpty(signature))
            {
                AddQueryParameter("client", clientId);
                AddQueryParameter("signature", signature);
            }
            else if (!string.IsNullOrEmpty(apiKey))
            {
                AddQueryParameter("key", apiKey);
            }
            else
            {
                throw new ArgumentException("You must specify a client id and signature or an api key");
            }
        }
    }
}
