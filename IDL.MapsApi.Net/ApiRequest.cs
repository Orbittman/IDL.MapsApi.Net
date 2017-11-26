using System.Collections.Specialized;
using System.Linq;

namespace IDL.MapsApi.Net
{
    public abstract class ApiRequest
    {
        protected NameValueCollection QueryParameters { get; set; } = new NameValueCollection();

        public virtual string Path
        {
            get
            {
                BuildQueryParameters();
                var parameters = QueryParameters.AllKeys.Select(q => q + "=" + QueryParameters[q]);
                return RequestSpecificPath + "?" + string.Join("&", parameters);
            }
        }

        protected virtual string RequestSpecificPath => string.Empty;

        protected virtual void BuildQueryParameters()
        {
        }

        protected void AddQueryParameter(string key, string value)
        {
            if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(value))
            {
                QueryParameters[key] = value;
            }
        }
    }
}
