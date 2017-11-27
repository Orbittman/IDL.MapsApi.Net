using System;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;

using IDL.MapsApi.Net.Google.Models;

namespace IDL.MapsApi.Net
{
    public abstract class GoogleApiRequest : ApiRequest
    {
        private string _rootPath;
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

        public override string RootPath
        {
            get => _rootPath ?? ConfigurationManager.AppSettings.Get("GoogleMapsGeoApiEndPoint").TrimEnd('/');
            set => _rootPath = value;
        }

        public override string Path
        {
            get
            {
                var secretKey = _credentials?.PrivateKey ?? ConfigurationManager.AppSettings.Get("GoogleMapsPrivateKey");
                var path = string.IsNullOrEmpty(secretKey) ? base.Path : SignPath(base.Path, secretKey);
                return path;
            }
        }

        private static string SignPath(string url, string key)
        {
            var usablePrivateKey = key.Replace("-", "+").Replace("_", "/");
            var privateKeyBytes = Convert.FromBase64String(usablePrivateKey);
            var uri = new Uri(url, UriKind.Absolute);
            var encodedPathAndQueryBytes = new ASCIIEncoding().GetBytes(uri.LocalPath + uri.Query);
            var algorithm = new HMACSHA1(privateKeyBytes);
            var hash = algorithm.ComputeHash(encodedPathAndQueryBytes);
            var signature = Convert.ToBase64String(hash).Replace("+", "-").Replace("/", "_");

            return uri.Scheme + "://" + uri.Host + uri.LocalPath + uri.Query + "&signature=" + signature;
        }
    }
}
