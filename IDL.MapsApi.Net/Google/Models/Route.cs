using System.Runtime.Serialization;

namespace IDL.MapsApi.Net.Google.Models
{
    [DataContract]
    public class Route
    {
        [DataMember(Name = "copyrights")]
        public string Copyrights { get; set; }

        [DataMember(Name = "summary")]
        public string Summary { get; set; }

        [DataMember(Name = "legs")]
        public Leg[] Legs { get; set; }

        [DataMember(Name = "bounds")]
        public Bounds Bounds { get; set; }

        [DataMember(Name = "overview_polyline")]
        public PolyLine PolyLine { get; set; }
    }
}
