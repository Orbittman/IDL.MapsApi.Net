using System.Runtime.Serialization;

namespace IDL.MapsApi.Net.Google.Models
{
    [DataContract]
    public class Geometry
    {
        [DataMember(Name = "bounds")]
        public Bounds Bounds { get; set; }

        [DataMember(Name = "viewport")]
        public Bounds ViewPort { get; set; }

        [DataMember(Name = "location")]
        public Location Location { get; set; }

        [DataMember(Name = "location_type")]
        public string LocationType { get; set; }
    }
}