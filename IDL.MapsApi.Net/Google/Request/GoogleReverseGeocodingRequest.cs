using IDL.MapsApi.Net.Google.Response;

namespace IDL.MapsApi.Net.Google.Request
{
    public class GoogleReverseGeocodingRequest : GoogleGeoLocationRequest, IRequest<GoogleGeocodingResponse>
    {
        private double _longitude;

        private double _latitude;

        public GoogleReverseGeocodingRequest(string apiKey = null)
            : base(apiKey)
        {
        }

        public double Latitude
        {
            get
            {
                return _latitude;
            }
            set
            {
                _latitude = value;
                QueryParameters["latlng"] = string.Format("{0},{1}", value, _longitude);
            }
        }

        public double Longitude
        {
            get
            {
                return _longitude;
            }
            set
            {
                _longitude = value;
                QueryParameters["latlng"] = string.Format("{0},{1}", _latitude, value);
            }
        }   
    }
}
