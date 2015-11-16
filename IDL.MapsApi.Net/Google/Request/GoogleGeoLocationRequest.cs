﻿using System.Collections.Specialized;
using System.Configuration;

namespace IDL.MapsApi.Net.Google.Request
{
    public abstract class GoogleGeoLocationRequest : ApiRequest
    {
        protected GoogleGeoLocationRequest(string apiKey = null)
        {
            QueryParameters = new NameValueCollection { { "key", apiKey ?? ConfigurationManager.AppSettings.Get("GoogleMapsApiKey") } };
        }

        protected override string RequestSpecificPath
        {
            get { return "geocode/json"; }
        }
    }
}
