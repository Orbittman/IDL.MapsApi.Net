using System.Runtime.Serialization;

namespace IDL.MapsApi.Net.Google.Models
{
    [DataContract]
    public class Waypoint
    {
        [DataMember(Name = "geocoder_status")]
        public string Status { get; set; }

        [DataMember(Name = "place_id")]
        public string PlaceId { get; set; }

        [DataMember(Name = "types")]
        public string[] Types { get; set; }

        [DataMember(Name = "partial_match")]
        public bool PartialMatch { get; set; }
    }
}
