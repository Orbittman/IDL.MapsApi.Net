using System.Net;

namespace IDL.MapsApi.Net.Client
{
    internal class ApiWebRequest : IWebRequest
    {
        public string Method { get; set; }
        public int ContentLength { get; set; }
        public string ContentType { get; set; }

        public HttpWebResponse GetResponse(string path)
        {
            return (HttpWebResponse)WebRequest.Create(path).GetResponse();
        }
    }
}