namespace IDL.MapsApi.Net.Models
{
    public class Result
    {
        public Address Address { get; internal set; }

        public BoundingBox Boundry { get; internal set; }

        public string Id { get; internal set; }

        public Location Location { get; internal set; }
    }
}
