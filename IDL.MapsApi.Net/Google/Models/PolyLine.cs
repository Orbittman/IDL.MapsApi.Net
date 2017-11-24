using System.Collections.Generic;
using System.Runtime.Serialization;

namespace IDL.MapsApi.Net.Google.Models
{
    [DataContract]
    public class PolyLine
    {
        [DataMember(Name = "points")]
        public string Points { get; set; }

        public IEnumerable<Net.Models.Location> StepLocations => Points.ToLocationPoints();
    }
}
