using System.Configuration;

namespace IDL.MapsApi.Net.MapBox.Request
{
    public abstract class MapBoxGeoLocationRequest : MapBoxApiRequest
    {
        private string _rootPath;

        protected MapBoxGeoLocationRequest(string apiKey = null)
            : base(apiKey)
        {
        }

        public string RootPath
        {
            get { return _rootPath ?? ConfigurationManager.AppSettings.Get("MapBoxGeoApiEndPoint"); }

            set { _rootPath = value; }
        }
    }
}
