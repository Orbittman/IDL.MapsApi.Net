using System.Collections.Generic;
using System.Runtime.Serialization;

namespace IDL.MapsApi.Net.MapBox.Models
{
    [DataContract]
    public class Geometery
    {
        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "coordinates")]
        public List<double> Coordinates { get; set; }
    }
}
