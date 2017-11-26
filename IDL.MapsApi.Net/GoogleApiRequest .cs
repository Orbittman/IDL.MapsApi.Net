using System;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;

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
            var apiKey = _credentials?.ApiKey ?? ConfigurationManager.AppSettings.Get("GoogleMapsApiKey");

            if (!string.IsNullOrEmpty(clientId))
            {
                AddQueryParameter("client", clientId);
            }
            else if (!string.IsNullOrEmpty(apiKey))
            {
                AddQueryParameter("key", apiKey);
            }
            else
            {
                throw new ArgumentException("You must specify a client id or an api key");
            }
        }

        public override string Path
        {
            get
            {
                var secretKey = _credentials?.SecretKey ?? ConfigurationManager.AppSettings.Get("GoogleMapsSecretKey");
                var path = string.IsNullOrEmpty(secretKey) ? base.Path : SignPath(base.Path, secretKey);
                return path;
            }
        }

        public static string SignPath(string url, string keyString)
        {
            var usablePrivateKey = keyString.Replace("-", "+").Replace("_", "/");
            var privateKeyBytes = Convert.FromBase64String(usablePrivateKey);
            var uri = new Uri(url, UriKind.Relative);
            var encodedPathAndQueryBytes = new ASCIIEncoding().GetBytes(uri.OriginalString);
            var algorithm = new HMACSHA1(privateKeyBytes);
            var hash = algorithm.ComputeHash(encodedPathAndQueryBytes);
            var signature = Convert.ToBase64String(hash).Replace("+", "-").Replace("/", "_");
            
            return uri.OriginalString + "&signature=" + signature;
        }
    }
}
