using System.Runtime.Serialization;

using IDL.MapsApi.Net.Google.Models;

namespace IDL.MapsApi.Net.Google.Response
{
    [DataContract]
    public class GoogleGeocodingResponse : GoogleResponse
    {
        [DataMember(Name = "results")]
        public GeoCodingResult[] Results { get; set; }
    }
}
