using IDL.MapsApi.Net.MapBox.Models;
using IDL.MapsApi.Net.MapBox.Response;

namespace IDL.MapsApi.Net.MapBox.Request
{
    public class MapBoxReverseGeocodingRequest : PlacesRequest, IRequest<MapBoxGeocodingResponse>
    {
        public MapBoxReverseGeocodingRequest(string apiKey = null)
            : base(apiKey)
        {
        }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public Point Proximity { get; set; }

        public Types[] Types { get; set; }

        protected override string RequestSpecificPath => $"geocoding/v5/{DataSet}/{Longitude},{Latitude}.json";

        protected override void BuildQueryParameters()
        {
            AddQueryParameter("types", string.Join(",", Types ?? new Types[0]).ToLower());
            AddQueryParameter("proximity", Proximity?.ToString());
        }
    }
}
