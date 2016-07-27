using System.Runtime.Serialization;

namespace IDL.MapsApi.Net.MapBox.Models
{
    [DataContract]
    public class Feature
    {
        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "text")]
        public string Text { get; set; }

        [DataMember(Name = "place_name")]
        public string PlaceName { get; set; }

        [DataMember(Name = "bbox")]
        public BBox BBox { get; set; }

        [DataMember(Name = "center")]
        public Point Center { get; set; }

        [DataMember(Name = "geometry")]
        public Geometery Geometry { get; set; }

        [DataMember(Name = "address")]
        public string Address { get; set; }

        [DataMember(Name = "context")]
        public Context[] Context { get; set; }
    }
}
