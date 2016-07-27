using System.Runtime.Serialization;

namespace IDL.MapsApi.Net.Google.Models
{
    [DataContract]
    public class Bounds
    {
        [DataMember(Name = "northeast")]
        public Location NorthEast { get; set; }

        [DataMember(Name = "southwest")]
        public Location SouthWest { get; set; }
    }
}
