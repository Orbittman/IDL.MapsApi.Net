using System.Collections.Specialized;
using System.Configuration;

namespace IDL.MapsApi.Net.MapBox.Request
{
    public abstract class MapBoxGeoLocationRequest : ApiRequest
    {
        protected MapBoxGeoLocationRequest(string apiKey = null)
        {
            QueryParameters = new NameValueCollection
            {
                { "access_token", apiKey ?? ConfigurationManager.AppSettings.Get("MapBoxApiKey") }
            };
        }
    }
}
