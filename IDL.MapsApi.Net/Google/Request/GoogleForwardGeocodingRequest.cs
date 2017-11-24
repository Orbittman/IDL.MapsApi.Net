﻿using IDL.MapsApi.Net.Google.Models;
using IDL.MapsApi.Net.Google.Response;

namespace IDL.MapsApi.Net.Google.Request
{
    public class GoogleForwardGeocodingRequest : GoogleGeoLocationRequest, IRequest<GoogleGeocodingResponse>
    {
        public GoogleForwardGeocodingRequest(string apiKey = null)
            : base(apiKey)
        {
        }

        public GoogleForwardGeocodingRequest(GoogleCredentials credentials = null)
            : base(credentials)
        {
        }

        public string Query
        {
            get => QueryParameters["address"];

            set => QueryParameters["address"] = value;
        }
    }
}
