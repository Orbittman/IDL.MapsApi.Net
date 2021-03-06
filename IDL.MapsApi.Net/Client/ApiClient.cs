﻿using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;

namespace IDL.MapsApi.Net.Client
{
    public class ApiClient : IApiClient
    {
        private readonly string _endPoint;

        public ApiClient(string rootPath = null)
        {
            _endPoint = rootPath;
        }

        public HttpMessageHandler ResponseHandler { get; set; }

        public virtual async Task<TResponse> GetAsync<TResponse>(IRequest<TResponse> request) where TResponse : class, new()
        {
            TResponse response = null;
            using (var handler = ResponseHandler ?? new HttpClientHandler())
            {
                using (var client = new HttpClient(handler))
                {
                    var requestMessage = new HttpRequestMessage(HttpMethod.Get, request.Path);
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
