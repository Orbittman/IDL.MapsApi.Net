using System.Configuration;

using IDL.MapsApi.Net.Google.Models;

namespace IDL.MapsApi.Net.Google.Request
{
    public abstract class GoogleGeoLocationRequest : GoogleApiRequest
    {
        protected GoogleGeoLocationRequest(string apiKey = null)
            : base(apiKey)
        {
        }

        protected GoogleGeoLocationRequest(GoogleCredentials credentials)
            : base(credentials)
        {
        }

        protected override string RequestSpecificPath => "/geocode/json";
    }
}
