using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;

namespace IDL.MapsApi.Net.Client
{
    public abstract class ApiClient : IApiClient
    {
        protected string _endPoint;

        private HttpClient _client;

        private HttpMessageHandler _responseHandler;

        public HttpMessageHandler ResponseHandler
        {
            get { return _responseHandler; }
            set { _responseHandler = value; }
        }

        public async Task<TResponse> GetAsync<TResponse>(IRequest<TResponse> request) where TResponse : class, new()
        {
            var path = _endPoint + (_endPoint.EndsWith("/") ? string.Empty : "/") + request.Path;

            TResponse response = null;
            using (var handler = _responseHandler ?? new HttpClientHandler())
            {
                using (var client = new HttpClient(handler))
                {
                    var requestMessage = new HttpRequestMessage(HttpMethod.Get, path);
                    requestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    using (var httpResponseMessage = await client.SendAsync(requestMessage).ConfigureAwait(false))
                    {
                        if (httpResponseMessage != null)
                        {
                            var jsonSerializer = new DataContractJsonSerializer(typeof(TResponse));
                            var responseStream = await httpResponseMessage.Content.ReadAsStreamAsync().ConfigureAwait(false);
                            response = jsonSerializer.ReadObject(responseStream) as TResponse;
                        }
                    }
                }
            }

            return response;
        }
    }
}
