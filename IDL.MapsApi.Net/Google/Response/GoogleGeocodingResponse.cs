using System.Runtime.Serialization;

using IDL.MapsApi.Net.Google.Models;

namespace IDL.MapsApi.Net.Google.Response
{
    [DataContract]
    public class GoogleGeocodingResponse
    {
        [DataMember(Name = "status")]
        public string Status { get; set; }

        [DataMember(Name = "results")]
        public GoogleGeoCodingResult[] Results { get; set; }
    }
}