using System.Collections.Specialized;
using System.Configuration;
using System.Linq;

namespace IDL.MapsApi.Net
{
    public abstract class MapBoxApiRequest : ApiRequest
    {
        protected MapBoxApiRequest(string apiKey = null)
        {
            QueryParameters = new NameValueCollection { { "access_token", apiKey ?? ConfigurationManager.AppSettings.Get("MapBoxApiKey") } };
        }
    }
}
