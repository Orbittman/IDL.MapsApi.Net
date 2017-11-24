using System.Configuration;

using IDL.MapsApi.Net.Google.Models;

namespace IDL.MapsApi.Net.Google.Request
{
    public abstract class GoogleGeoLocationRequest : GoogleApiRequest
    {
        private string _rootPath;

        protected GoogleGeoLocationRequest(string apiKey = null)
            : base(apiKey)
        {
        }

        protected GoogleGeoLocationRequest(GoogleCredentials credentials)
            : base(credentials)
        {
        }

        protected override string RequestSpecificPath => "geocode/json";

        public string RootPath
        {
            get => _rootPath ?? ConfigurationManager.AppSettings.Get("GoogleMapsGeoApiEndPoint");
            set => _rootPath = value;
        }
    }
}
