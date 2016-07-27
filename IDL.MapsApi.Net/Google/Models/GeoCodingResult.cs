using System.Runtime.Serialization;

namespace IDL.MapsApi.Net.Google.Models
{
    [DataContract]
    public class GeoCodingResult
    {
        [DataMember(Name = "formatted_address")]
        public string FormattedAddress { get; set; }

        [DataMember(Name = "address_components")]
        public AddressComponent[] AddressComponents { get; set; }

        [DataMember(Name = "geometry")]
        public Geometry Geometry { get; set; }

        [DataMember(Name = "place_id")]
        public string PlaceId { get; set; }
    }
}
