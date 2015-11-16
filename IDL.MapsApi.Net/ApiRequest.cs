using System.Collections.Specialized;
using System.Linq;

namespace IDL.MapsApi.Net
{
    public abstract class ApiRequest
    {
        protected ApiRequest()
        {
            QueryParameters = new NameValueCollection();
        }

        protected NameValueCollection QueryParameters { get; set; }

        public string Path
        {
            get
            {
                return RequestSpecificPath + "?" + string.Join("&", QueryParameters.AllKeys.Select(q => q + "=" + QueryParameters[(string) q]));
            }
        }

        protected virtual void ConfigureParameters()
        {
        }

        protected virtual string RequestSpecificPath
        {
            get
            {
                return string.Empty;
            }
        }
    }
}