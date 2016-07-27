using System.Runtime.Serialization;

namespace IDL.MapsApi.Net.Google.Models
{
    [DataContract]
    public class Step
    {
        [DataMember(Name = "travel_mode")]
        public string TravelMode { get; set; }

        [DataMember(Name = "html_instructions")]
        public string HtmlInstructions { get; set; }

        [DataMember(Name = "maneuver")]
        public string Maneuver { get; set; }

        [DataMember(Name = "start_location")]
        public Location Start { get; set; }

        [DataMember(Name = "end_location")]
        public Location End { get; set; }

        [DataMember(Name = "distance")]
        public TextValue Distance { get; set; }

        [DataMember(Name = "duration")]
        public TextValue Duration { get; set; }

        [DataMember(Name = "polyline")]
        public PolyLine PolyLine { get; set; }
    }
}
