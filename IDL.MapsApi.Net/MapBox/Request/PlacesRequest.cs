namespace IDL.MapsApi.Net.MapBox.Request
{
    public abstract class PlacesRequest : MapBoxGeoLocationRequest
    {
        protected PlacesRequest(string apiKey = null)
            : base(apiKey)
        {
        }

        protected string DataSet
        {
            get { return "mapbox.places"; }
        }
    }
}