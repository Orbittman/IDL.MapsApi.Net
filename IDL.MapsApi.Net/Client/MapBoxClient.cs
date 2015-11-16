namespace IDL.MapsApi.Net.Client
{
    internal class MapBoxClient : ApiClient, IMapBoxApiClient
    {
        internal MapBoxClient(string rootPath = null)
        {
            _endPoint = rootPath ?? System.Configuration.ConfigurationManager.AppSettings.Get("MapBoxEndPoint") ?? "https://api.mapbox.com/";
        }
    }
}
