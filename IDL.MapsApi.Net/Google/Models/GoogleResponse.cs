using System.Runtime.Serialization;

namespace IDL.MapsApi.Net.Google.Models
{
    [DataContract]
    public abstract class GoogleResponse
    {
        [DataMember(Name = "status")]
        public string Status { get; set; }
    }
}
