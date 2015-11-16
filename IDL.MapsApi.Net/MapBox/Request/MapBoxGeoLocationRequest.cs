using System.Collections.Specialized;
using System.Configuration;

namespace IDL.MapsApi.Net.MapBox.Request
{
    public abstract class MapBoxGeoLocationRequest : ApiRequest
    {
        private string _rootPath;

        protected MapBoxGeoLocationRequest(string apiKey = null)
        {
            QueryParameters = new NameValueCollection
            {
                { "access_token", apiKey ?? ConfigurationManager.AppSettings.Get("MapBoxApiKey") }
            };
        }

        public string RootPath
        {
            get
            {
                return _rootPath ?? ConfigurationManager.AppSettings.Get("MapBoxGeoApiEndPoint");
            }

            set
            {
                _rootPath = value;
            }
        }
    }
}
