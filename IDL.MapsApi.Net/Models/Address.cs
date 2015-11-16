namespace IDL.MapsApi.Net.Models
{
    public class Address
    {
        public string Country { get; internal set; }

        public string County { get; internal set; }

        public string FullAddress { get; internal set; }

        public string Locality { get; internal set; }

        public string PostalCode { get; internal set; }

        public string PropertyId { get; internal set; }

        public string Region { get; internal set; }

        public string Street { get; internal set; }

        public string SubLocality { get; internal set; }

        public string Town { get; internal set; }

        public override string ToString()
        {
            return FullAddress;
        }
    }
}