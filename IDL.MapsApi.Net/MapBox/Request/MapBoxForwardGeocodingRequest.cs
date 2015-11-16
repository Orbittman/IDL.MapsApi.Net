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

        public Point Proximity
        {
            get { return Point.Parse(QueryParameters["proximity"]); }
            set { QueryParameters["proximity"] = value.ToString(); }
        }

        public Types[] Types { get; set; }

        protected override void ConfigureParameters()
        {
            QueryParameters["types"] = string.Join(",", Types).ToLower();
        }

        protected override string RequestSpecificPath
        {
            get
            {
                return string.Format("geocoding/v5/{0}/{1}.json", DataSet, Query);
            }
        }
    }
}