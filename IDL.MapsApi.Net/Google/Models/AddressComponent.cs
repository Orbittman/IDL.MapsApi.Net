using System.Runtime.Serialization;

namespace IDL.MapsApi.Net.Google.Models
{
    [DataContract]
    public class AddressComponent
    {
        [DataMember(Name = "long_name")]
        public string LongName { get; set; }

        [DataMember(Name = "short_name")]
        public string ShortName { get; set; }

        [DataMember(Name = "types")]
        public string[] Types { get; set; }
    }
}
