using System.Runtime.Serialization;

namespace IDL.MapsApi.Net.Google.Models
{
    [DataContract]
    public class Leg
    {
        [DataMember(Name = "steps")]
        public Step[] Steps { get; set; }

        [DataMember(Name = "distance")]
        public TextValue Distance { get; set; }

        [DataMember(Name = "duration")]
        public TextValue Duration { get; set; }

        [DataMember(Name = "end_address")]
        public string EndAddress { get; set; }

        [DataMember(Name = "end_location")]
        public Location End { get; set; }

        [DataMember(Name = "start_address")]
        public string StartAddress { get; set; }

        [DataMember(Name = "start_location")]
        public Location Start { get; set; }
    }
}
