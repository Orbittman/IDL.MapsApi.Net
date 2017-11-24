namespace IDL.MapsApi.Net.Google.Models
{
    public class GoogleCredentials
    {
        public GoogleCredentials(string apiKey)
        {
            ApiKey = apiKey;
        }

        public GoogleCredentials(string clientId, string signature)
        {
            ClientId = clientId;
            Signature = signature;
        }

        public string ApiKey { get; }

        public string ClientId { get; }

        public string Signature { get; }
    }
}