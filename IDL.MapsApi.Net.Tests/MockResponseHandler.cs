using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace IDL.MapsApi.Net.Tests
{
    internal class MockResponseHandler : HttpMessageHandler
    {
        private readonly Stream _responseStream;

        public MockResponseHandler(Stream responseStream)
        {
            _responseStream = responseStream;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Task.FromResult(
                new HttpResponseMessage
                {
                    Content = new StreamContent(_responseStream)
                });
        }
    }
}