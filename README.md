[![idl-public MyGet Build Status](https://www.myget.org/BuildSource/Badge/idl-public?identifier=95c60c91-9607-478d-b51a-679426498ab6)](https://www.myget.org/)
# IDL.MapsApi.Net

This is a .Net library for accessing the Google Maps and MapBox geolocation APIs. At the moment there is only support for the forward lookup where you get a lat/long from a location string, reverse where you get an address from a lat/long and Google directions. 
The library is based on simple requests to the MapsApi client for the service that you requre:

````c#
IApiClient googleClient = new new ApiClient();
var request = new GoogleForwardGeocodingRequest(new GoogleCredentials(key))
    {
        Query = "BS13RW"
    };
var googleResponse = googleClient.GetAsync(request);

IApiClient mapBoxClient = new new ApiClient();
var request = new MapBoxForwardGeocodingRequest(key)
    {
        Query = "BS13RW"
    };
var mapBoxResponse = mapBoxClient.GetAsync(request);
````

Each request has a sepcific response that represents the data that the provider supplies, Google Maps and MapBox both return different data for the same type of request. MapsApi contains extension methods that convert these into a common format with a basic generic fields for what is required.
`````c#
var response = client.GetAsync(request).Result.AsMapsApiGeocodingResult();
`````
This returns a collection of Result objects.
````c#
public class Result
    {
        public Address Address { get; internal set; }

        public BoundingBox Boundry { get; internal set; }

        public string Id { get; internal set; }

        public Location Location { get; internal set; }
    }
````
By default the path and API keys are picked up from the AppSettings configuration section with the following keys:
- MapBoxApiKey
- MapBoxGeoApiEndPoint
- GoogleMapsApiKey
- GoogleMapsGeoApiEndPoint

This can be overridden by passing the root path to the ApiClient constructor or setting the RootPath of a request and passing in the api key or in the case of the Google requests a GoogleCredentials class to the request constructor. This is because from expreience the path formats can be different when using different requests to the same provider.

If you would rather use a different client than the built in one you can create a wrapper for libraries such as RestSharp.
````c#
public class RestSharpClientWrapper : ApiClient
  {
      private readonly IRestClient _client;

      public RestSharpClientWrapper(IRestClient client = null)
      {
          _client = client ?? new RestClient();
          _client.ClearHandlers();
          _client.AddHandler("application/vnd.geo+json", new JsonDeserializer());
      }

      public override Task<TResponse> GetAsync<TResponse>(IRequest<TResponse> request)
      {
          _client.BaseUrl = new Uri(request.RootPath);
          var response = _client.GetTaskAsync<TResponse>(new RestRequest(request.Path) { RequestFormat = DataFormat.Json });
          return response;
      }
  }
  ````
