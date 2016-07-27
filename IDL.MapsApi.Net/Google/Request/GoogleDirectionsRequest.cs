using System.Configuration;

using IDL.MapsApi.Net.Google.Response;

namespace IDL.MapsApi.Net.Google.Request
{
    public class GoogleDirectionsRequest : GoogleApiRequest, IRequest<GoogleDirectionsResponse>
    {
        private string _destination;
        private string _origin;
        private string _rootPath;

        public GoogleDirectionsRequest(string apiKey = null)
            : base(apiKey)
        {
        }

        protected override string RequestSpecificPath => "directions/json";

        public string Origin
        {
            get { return _origin; }
            set
            {
                QueryParameters["origin"] = $"{value}{_origin}";
                _origin = value;
            }
        }

        public string Destination
        {
            get { return _destination; }
            set
            {
                QueryParameters["destination"] = $"{value}{_destination}";
                _destination = value;
            }
        }

        public string RootPath
        {
            get { return _rootPath ?? ConfigurationManager.AppSettings.Get("GoogleMapsGeoApiEndPoint"); }

            set { _rootPath = value; }
        }
    }
}
