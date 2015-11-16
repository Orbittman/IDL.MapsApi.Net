using System.Net;

namespace IDL.MapsApi.Net.Client
{
    public interface IWebRequest
    {
        string Method { get; set; }
        int ContentLength { get; set; }
        string ContentType { get; set; }

        HttpWebResponse GetResponse(string path);
    }
}