using System.Runtime.Serialization;

namespace IDL.MapsApi.Net.Google.Models
{
    [DataContract]
    public class TextValue
    {
        [DataMember(Name = "text")]
        public string Text { get; set; }

        [DataMember(Name = "value")]
        public int Value { get; set; }
    }
}
