using System.Configuration;

using IDL.MapsApi.Net.Google.Models;
using IDL.MapsApi.Net.Google.Response;

namespace IDL.MapsApi.Net.Google.Request
{
    public class GoogleDirectionsRequest : GoogleApiRequest, IRequest<GoogleDirectionsResponse>
    {
        public GoogleDirectionsRequest(string apiKey = null)
            : base(apiKey)
        {
        }

        public GoogleDirectionsRequest(GoogleCredentials credentials)
            : base(credentials)
        {
        }

        protected override string RequestSpecificPath => "directions/json";

        public string Origin { get; set; }

        public string Destination { get; set; }

        protected override void BuildQueryParameters()
        {
            AddQueryParameter("destination", Destination);
            AddQueryParameter("origin", Origin);
            base.BuildQueryParameters();
        }
    }
}
