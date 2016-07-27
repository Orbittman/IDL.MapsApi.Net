namespace IDL.MapsApi.Net.Models
{
    public class Location
    {
        public double Latitude { get; internal set; }

        public double Longitude { get; internal set; }

        public override string ToString()
        {
            return $"{Longitude},{Latitude}";
        }
    }
}
