using IDL.MapsApi.Net.Google.Models;
using IDL.MapsApi.Net.Google.Response;

namespace IDL.MapsApi.Net.Google.Request
{
    public class GoogleReverseGeocodingRequest : GoogleGeoLocationRequest, IRequest<GoogleGeocodingResponse>
    {
        public GoogleReverseGeocodingRequest(string apiKey = null)
            : base(apiKey)
        {
        }

        public GoogleReverseGeocodingRequest(GoogleCredentials credentials)
            : base(credentials)
        {
        }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        protected override void BuildQueryParameters()
        {
            AddQueryParameter("latlng", $"{Latitude},{Longitude}");
            base.BuildQueryParameters();
        }
    }
}
