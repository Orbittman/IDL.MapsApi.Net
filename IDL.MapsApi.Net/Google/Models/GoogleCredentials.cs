namespace IDL.MapsApi.Net.Google.Models
{
    public class GoogleCredentials
    {
        public GoogleCredentials(string apiKey)
        {
            ApiKey = apiKey;
        }

        public GoogleCredentials(string clientId, string privateKey)
        {
            ClientId = clientId;
            PrivateKey = privateKey;
        }

        public string ApiKey { get; }

        public string ClientId { get; }

        public string PrivateKey { get; }
    }
}