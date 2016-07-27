using System.Runtime.Serialization;

namespace IDL.MapsApi.Net.MapBox.Models
{
    [DataContract]
    public class Context
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "text")]
        public string Text { get; set; }
    }
}
