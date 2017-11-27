using System.Collections.Specialized;
using System.Configuration;
using System.Linq;

namespace IDL.MapsApi.Net
{
    public abstract class MapBoxApiRequest : ApiRequest
    {
        private string _rootPath;

        protected MapBoxApiRequest(string apiKey = null)
        {
            QueryParameters = new NameValueCollection { { "access_token", apiKey ?? ConfigurationManager.AppSettings.Get("MapBoxApiKey") } };
        }

        public override string RootPath
        {
            get => _rootPath ?? ConfigurationManager.AppSettings.Get("MapBoxGeoApiEndPoint");
            set => _rootPath = value;
        }
    }
}
