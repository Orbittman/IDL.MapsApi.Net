using System.Runtime.Serialization;

namespace IDL.MapsApi.Net.Google.Models
{
    [DataContract]
    public class PolyLine
    {
        [DataMember(Name = "points")]
        public string Points { get; set; }

        public Net.Models.Location[] StepLocations => Points.ToLocationPoints();
    }
}
