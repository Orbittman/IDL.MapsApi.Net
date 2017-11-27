using System.Configuration;

namespace IDL.MapsApi.Net.MapBox.Request
{
    public abstract class MapBoxGeoLocationRequest : MapBoxApiRequest
    {
        protected MapBoxGeoLocationRequest(string apiKey = null)
            : base(apiKey)
        {
        }
    }
}
