using System.Runtime.Serialization;

using IDL.MapsApi.Net.Google.Models;

namespace IDL.MapsApi.Net.Google.Response
{
    [DataContract]
    public class GoogleDirectionsResponse
    {
        [DataMember(Name = "geocoded_waypoints")]
        public Waypoint[] Waypoints { get; set; }

        [DataMember(Name = "routes")]
        public Route[] Routes { get; set; }
    }
}
