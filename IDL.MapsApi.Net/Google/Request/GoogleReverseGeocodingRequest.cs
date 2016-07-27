using IDL.MapsApi.Net.Google.Response;

namespace IDL.MapsApi.Net.Google.Request
{
    public class GoogleReverseGeocodingRequest : GoogleGeoLocationRequest, IRequest<GoogleGeocodingResponse>
    {
        private double _latitude;
        private double _longitude;

        public GoogleReverseGeocodingRequest(string apiKey = null)
            : base(apiKey)
        {
        }

        public double Latitude
        {
            get { return _latitude; }
            set
            {
                _latitude = value;
                QueryParameters["latlng"] = $"{value},{_longitude}";
            }
        }

        public double Longitude
        {
            get { return _longitude; }
            set
            {
                _longitude = value;
                QueryParameters["latlng"] = $"{_latitude},{value}";
            }
        }
    }
}
