using System.Collections.Specialized;
using System.Configuration;
using System.Linq;

namespace IDL.MapsApi.Net
{
    public abstract class MapBoxApiRequest
    {
        protected MapBoxApiRequest(string apiKey = null)
        {
            QueryParameters = new NameValueCollection { { "access_token", apiKey ?? ConfigurationManager.AppSettings.Get("MapBoxApiKey") } };
        }

        protected NameValueCollection QueryParameters { get; set; }

        public string Path
        {
            get
            {
                var parameters = QueryParameters.AllKeys.Select(q => q + "=" + QueryParameters[q]);
                return RequestSpecificPath + "?" + string.Join("&", parameters);
            }
        }

        protected virtual string RequestSpecificPath => string.Empty;

        protected virtual void ConfigureParameters()
        {
        }
    }
}
