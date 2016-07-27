using IDL.MapsApi.Net.Google.Response;

namespace IDL.MapsApi.Net.Google.Request
{
    public class GoogleForwardGeocodingRequest : GoogleGeoLocationRequest, IRequest<GoogleGeocodingResponse>
    {
        public GoogleForwardGeocodingRequest(string apiKey = null)
            : base(apiKey)
        {
        }

        public string Query
        {
            get { return QueryParameters["address"]; }

            set { QueryParameters["address"] = value; }
        }
    }
}
