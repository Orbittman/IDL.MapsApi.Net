using System.Configuration;

namespace IDL.MapsApi.Net.Google.Request
{
    public abstract class GoogleGeoLocationRequest : GoogleApiRequest
    {
        private string _rootPath;

        protected GoogleGeoLocationRequest(string apiKey = null)
            : base(apiKey)
        {
        }

        protected override string RequestSpecificPath => "geocode/json";

        public string RootPath
        {
            get { return _rootPath ?? ConfigurationManager.AppSettings.Get("GoogleMapsGeoApiEndPoint"); }

            set { _rootPath = value; }
        }
    }
}
