using System.Threading.Tasks;

using IDL.MapsApi.Net.Client;

namespace IDL.MapsApi.Net
{
    public class GoogleGeoLocationProvider : IGeoLocationProvider
    {
        private readonly IGoogleApiClient _client;

        public GoogleGeoLocationProvider(IGoogleApiClient client = null)
        {
            _client = client ?? new GoogleClient();
        }

        public Task<TResponse> GetAsync<TResponse>(IRequest<TResponse> request)
            where TResponse : class, new() 
        {
            return _client.GetAsync(request);
        }
    }
}
