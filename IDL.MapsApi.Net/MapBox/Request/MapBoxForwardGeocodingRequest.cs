using IDL.MapsApi.Net.MapBox.Models;
using IDL.MapsApi.Net.MapBox.Response;

namespace IDL.MapsApi.Net.MapBox.Request
{
    public class MapBoxForwardGeocodingRequest : PlacesRequest, IRequest<MapBoxGeocodingResponse>
    {
        public MapBoxForwardGeocodingRequest(string apiKey = null)
            : base(apiKey)
        {
        }

        public string Query { get; set; }

        public Point Proximity { get; set; }

        public Types[] Types { get; set; }

        protected override string RequestSpecificPath => $"/geocoding/v5/{DataSet}/{Query}.json";

        protected override void BuildQueryParameters()
        {
            AddQueryParameter("types", string.Join(",", Types).ToLower());
            AddQueryParameter("proximity", Proximity?.ToString());
        }
    }
}
