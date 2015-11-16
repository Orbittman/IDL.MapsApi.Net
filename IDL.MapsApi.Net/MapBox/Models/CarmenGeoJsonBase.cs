using System.Collections.Generic;
using System.Runtime.Serialization;

namespace IDL.MapsApi.Net.MapBox.Models
{
    /// <summary>
    /// This is based on https://github.com/mapbox/carmen/blob/master/carmen-geojson.md that MapBox use as their GeoJson responses
    /// </summary>
    [DataContract]
    public abstract class CarmenGeoJsonBase
    {
        [DataMember(Name = "features")]
        public List<Feature> Features { get; set; }
    }
}