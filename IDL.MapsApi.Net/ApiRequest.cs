using System;
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
                if (string.IsNullOrEmpty(RootPath))
                {
                    throw new NullReferenceException($"No root parameter was supplied for the request");
                }
                
                BuildQueryParameters();
                var parameters = QueryParameters.AllKeys.Select(q => q + "=" + QueryParameters[q]);
                var relativePath = RequestSpecificPath + "?" + string.Join("&", parameters);
                return RootPath + (RootPath.EndsWith("/") || relativePath.StartsWith("/") ? string.Empty : "/") + relativePath;
            }
        }

        protected virtual string RequestSpecificPath => string.Empty;

        public abstract string RootPath { get; set; }

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
