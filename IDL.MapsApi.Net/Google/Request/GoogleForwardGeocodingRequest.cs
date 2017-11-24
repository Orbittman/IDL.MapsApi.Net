using IDL.MapsApi.Net.Google.Models;
using IDL.MapsApi.Net.Google.Response;

namespace IDL.MapsApi.Net.Google.Request
{
    public class GoogleForwardGeocodingRequest : GoogleGeoLocationRequest, IRequest<GoogleGeocodingResponse>
    {
        public GoogleForwardGeocodingRequest(string apiKey = null)
            : base(apiKey)
        {
        }

        public GoogleForwardGeocodingRequest(GoogleCredentials credentials = null)
            : base(credentials)
        {
        }

        public string Address { get; set; }

        protected override void BuildQueryParameters()
        {
            AddQueryParameter("address", Address);
            base.BuildQueryParameters();
        }
    }
}
