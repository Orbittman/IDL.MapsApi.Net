namespace IDL.MapsApi.Net.Client
{
    internal class GoogleClient : ApiClient, IGoogleApiClient
    {
        internal GoogleClient(string rootPath = null)
        {
            _endPoint = rootPath ?? System.Configuration.ConfigurationManager.AppSettings.Get("GoogleGeoLocationEndPoint") ?? "https://maps.googleapis.com/maps/api/";
        }
    }
}
