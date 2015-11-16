using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using IDL.MapsApi.Net.Client;
using IDL.MapsApi.Net.Google.Request;
using IDL.MapsApi.Net.MapBox.Request;
using IDL.MapsApi.Net.MapBox.Response;

using NSubstitute;

using NUnit.Framework;

namespace IDL.MapsApi.Net.Tests
{
    [TestFixture]
    public class RequestTests
    {
        private byte[] GetReversGeoCodingMapBoxResponseResult()
        {
            const string data =
                "{\"type\":\"FeatureCollection\",\"query\":[10,50],\"features\":[{\"id\":\"address.5703004565802462\",\"type\":\"Feature\",\"text\":\"B26a\",\"place_name\":\"B26a, Arnstein, Bayern 97450, Germany\",\"relevance\":1,\"properties\":{},\"bbox\":[9.968342799999997,49.976788899999995,10.044921699999998,50.007632099999995],\"center\":[10.000421637812478,49.997772682303044],\"geometry\":{\"type\":\"Point\",\"coordinates\":[10.000421637812478,49.997772682303044]},\"context\":[{\"id\":\"place.4731\",\"text\":\"Arnstein\"},{\"id\":\"postcode.30860\",\"text\":\"97450\"},{\"id\":\"region.4238174215\",\"text\":\"Bayern\"},{\"id\":\"country.593090590\",\"text\":\"Germany\",\"short_code\":\"de\"}]},{\"id\":\"place.4731\",\"type\":\"Feature\",\"text\":\"Arnstein\",\"place_name\":\"Arnstein, Bayern, Germany\",\"relevance\":1,\"properties\":{},\"bbox\":[9.864815999999998,49.93074019999999,10.045934999999997,50.057948999999994],\"center\":[9.9638,49.978],\"geometry\":{\"type\":\"Point\",\"coordinates\":[9.9638,49.978]},\"context\":[{\"id\":\"postcode.30860\",\"text\":\"97450\"},{\"id\":\"region.4238174215\",\"text\":\"Bayern\"},{\"id\":\"country.593090590\",\"text\":\"Germany\",\"short_code\":\"de\"}]},{\"id\":\"postcode.30860\",\"type\":\"Feature\",\"text\":\"97450\",\"place_name\":\"97450, Bayern, Germany\",\"relevance\":1,\"properties\":{},\"bbox\":[9.864815999999998,49.93074019999999,10.045934999999997,50.057948999999994],\"center\":[9.945599,49.99471],\"geometry\":{\"type\":\"Point\",\"coordinates\":[9.945599,49.99471]},\"context\":[{\"id\":\"region.4238174215\",\"text\":\"Bayern\"},{\"id\":\"country.593090590\",\"text\":\"Germany\",\"short_code\":\"de\"}]},{\"id\":\"region.4238174215\",\"type\":\"Feature\",\"text\":\"Bayern\",\"place_name\":\"Bayern, Germany\",\"relevance\":1,\"properties\":{},\"bbox\":[8.975925999999784,47.270351499999826,13.83939649999979,50.56373349999983],\"center\":[12.041375,48.913129],\"geometry\":{\"type\":\"Point\",\"coordinates\":[12.041375,48.913129]},\"context\":[{\"id\":\"country.593090590\",\"text\":\"Germany\",\"short_code\":\"de\"}]},{\"id\":\"country.593090590\",\"type\":\"Feature\",\"text\":\"Germany\",\"place_name\":\"Germany\",\"relevance\":1,\"properties\":{\"short_code\":\"de\"},\"bbox\":[5.866002999999806,47.27067999999984,15.041428499999792,55.05125499999981],\"center\":[10.582125,51.160195],\"geometry\":{\"type\":\"Point\",\"coordinates\":[10.582125,51.160195]}}],\"attribution\":\"NOTICE: © 2015 Mapbox and its suppliers. All rights reserved. Use of this data is subject to the Mapbox Terms of Service (https://www.mapbox.com/about/maps/). This response and the information it contains may not be retained.\"}";
            return Encoding.ASCII.GetBytes(data);
        }

        private byte[] GetReverseGeocodingGoogleResult()
        {
            const string data =
                "{\"results\":[{\"address_components\":[{\"long_name\":\"Cleeve Road\",\"short_name\":\"Cleeve Rd\",\"types\":[\"route\"]},{\"long_name\":\"Winterbourne\",\"short_name\":\"Winterbourne\",\"types\":[\"locality\",\"political\"]},{\"long_name\":\"Bristol\",\"short_name\":\"Bristol\",\"types\":[\"postal_town\"]},{\"long_name\":\"South Gloucestershire\",\"short_name\":\"South Gloucestershire\",\"types\":[\"administrative_area_level_2\",\"political\"]},{\"long_name\":\"United Kingdom\",\"short_name\":\"GB\",\"types\":[\"country\",\"political\"]},{\"long_name\":\"BS16\",\"short_name\":\"BS16\",\"types\":[\"postal_code_prefix\",\"postal_code\"]}],\"formatted_address\":\"Cleeve Rd, Winterbourne, Bristol, South Gloucestershire BS16, UK\",\"geometry\":{\"bounds\":{\"northeast\":{\"lat\":51.4988022,\"lng\":-2.5140046},\"southwest\":{\"lat\":51.4972003,\"lng\":-2.5166892}},\"location\":{\"lat\":51.4979803,\"lng\":-2.5153016},\"location_type\":\"GEOMETRIC_CENTER\",\"viewport\":{\"northeast\":{\"lat\":51.4993502302915,\"lng\":-2.513997919708498},\"southwest\":{\"lat\":51.49665226970851,\"lng\":-2.516695880291502}}},\"place_id\":\"ChIJO7QcmhqQcUgRV02ITMpiPrw\",\"types\":[\"route\"]},{\"address_components\":[{\"long_name\":\"Winterbourne\",\"short_name\":\"Winterbourne\",\"types\":[\"locality\",\"political\"]},{\"long_name\":\"Bristol\",\"short_name\":\"Bristol\",\"types\":[\"postal_town\"]},{\"long_name\":\"Winterbourne\",\"short_name\":\"Winterbourne\",\"types\":[\"administrative_area_level_4\",\"political\"]},{\"long_name\":\"South Gloucestershire\",\"short_name\":\"South Gloucestershire\",\"types\":[\"administrative_area_level_2\",\"political\"]},{\"long_name\":\"England\",\"short_name\":\"England\",\"types\":[\"administrative_area_level_1\",\"political\"]},{\"long_name\":\"United Kingdom\",\"short_name\":\"GB\",\"types\":[\"country\",\"political\"]}],\"formatted_address\":\"Winterbourne, Bristol, South Gloucestershire, UK\",\"geometry\":{\"bounds\":{\"northeast\":{\"lat\":51.5401447,\"lng\":-2.4865308},\"southwest\":{\"lat\":51.492441,\"lng\":-2.5441765}},\"location\":{\"lat\":51.52432,\"lng\":-2.501044},\"location_type\":\"APPROXIMATE\",\"viewport\":{\"northeast\":{\"lat\":51.5401447,\"lng\":-2.4865308},\"southwest\":{\"lat\":51.492441,\"lng\":-2.5441765}}},\"place_id\":\"ChIJYdB6PWOQcUgRHf-XVS-Mdmk\",\"types\":[\"locality\",\"political\"]}],\"status\":\"OK\"}";
            return Encoding.ASCII.GetBytes(data);
        }

        private byte[] GetForwardGeoCodingMapBoxResponseResult()
        {
            const string data =
                "{\"type\":\"FeatureCollection\",\"query\":[\"1600\",\"pennsylvania\",\"ave\",\"nw\"],\"features\":[{\"id\":\"address.39053333360279\",\"type\":\"Feature\",\"text\":\"Pennsylvania Ave NW\",\"place_name\":\"1600 Pennsylvania Ave NW, Washington, District of Columbia 20004, United States\",\"relevance\":0.99,\"properties\":{},\"bbox\":[-77.05781199999998,38.89252299999999,-77.01844799999999,38.905058999999994],\"center\":[-77.034389,38.897693],\"geometry\":{\"type\":\"Point\",\"coordinates\":[-77.034389,38.897693]},\"address\":\"1600\",\"context\":[{\"id\":\"neighborhood.3800\",\"text\":\"Franklin Mcpherson Square\"},{\"id\":\"place.57972\",\"text\":\"Washington\"},{\"id\":\"postcode.858369517\",\"text\":\"20004\"},{\"id\":\"region.1190806886\",\"text\":\"District of Columbia\"},{\"id\":\"country.4150104525\",\"text\":\"United States\",\"short_code\":\"us\"}]},{\"id\":\"address.6772084528974606\",\"type\":\"Feature\",\"text\":\"Pennsylvania Ave\",\"place_name\":\"1600 Pennsylvania Ave, Miami Beach, Florida 33139, United States\",\"relevance\":0.74,\"properties\":{},\"bbox\":[-80.13474099999999,25.776914999999992,-80.13365999999999,25.789956999999994],\"center\":[-80.13459,25.789245],\"geometry\":{\"type\":\"Point\",\"coordinates\":[-80.13459,25.789245]},\"address\":\"1600\",\"context\":[{\"id\":\"neighborhood.9802\",\"text\":\"South Miami Beach\"},{\"id\":\"place.34076\",\"text\":\"Miami Beach\"},{\"id\":\"postcode.260297597\",\"text\":\"33139\"},{\"id\":\"region.3998021366\",\"text\":\"Florida\"},{\"id\":\"country.4150104525\",\"text\":\"United States\",\"short_code\":\"us\"}]},{\"id\":\"address.3015245837509073\",\"type\":\"Feature\",\"text\":\"Pennsylvania Ave\",\"place_name\":\"1600 Pennsylvania Ave, Baltimore, Maryland 21217, United States\",\"relevance\":0.74,\"properties\":{},\"bbox\":[-76.64626799999999,39.295034999999984,-76.62387699999998,39.313258999999995],\"center\":[-76.634388,39.30307],\"geometry\":{\"type\":\"Point\",\"coordinates\":[-76.634388,39.30307]},\"address\":\"1600\",\"context\":[{\"id\":\"neighborhood.13930\",\"text\":\"Upton\"},{\"id\":\"place.2706\",\"text\":\"Baltimore\"},{\"id\":\"postcode.1084729612\",\"text\":\"21217\"},{\"id\":\"region.928365533\",\"text\":\"Maryland\"},{\"id\":\"country.4150104525\",\"text\":\"United States\",\"short_code\":\"us\"}]},{\"id\":\"address.4831595508876739\",\"type\":\"Feature\",\"text\":\"Pennsylvania Ave\",\"place_name\":\"1600 Pennsylvania Ave, Wilmington, Delaware 19805, United States\",\"relevance\":0.74,\"properties\":{},\"bbox\":[-75.57983099999998,39.75238499999999,-75.55711499999998,39.76292599999999],\"center\":[-75.563876,39.754014],\"geometry\":{\"type\":\"Point\",\"coordinates\":[-75.563876,39.754014]},\"address\":\"1600\",\"context\":[{\"id\":\"place.60167\",\"text\":\"Wilmington\"},{\"id\":\"postcode.643940110\",\"text\":\"19805\"},{\"id\":\"region.468787775\",\"text\":\"Delaware\"},{\"id\":\"country.4150104525\",\"text\":\"United States\",\"short_code\":\"us\"}]},{\"id\":\"address.4903554018147496\",\"type\":\"Feature\",\"text\":\"Pennsylvania Ave\",\"place_name\":\"1600 Pennsylvania Ave, Los Angeles, California 90033, United States\",\"relevance\":0.74,\"properties\":{},\"bbox\":[-118.22153699999997,34.041169999999994,-118.20192399999998,34.05242499999999],\"center\":[-118.220304,34.049093],\"geometry\":{\"type\":\"Point\",\"coordinates\":[-118.220304,34.049093]},\"address\":\"1600\",\"context\":[{\"id\":\"neighborhood.12333\",\"text\":\"Arroyo Seco\"},{\"id\":\"place.30976\",\"text\":\"Los Angeles\"},{\"id\":\"postcode.256695399\",\"text\":\"90033\"},{\"id\":\"region.1112151813\",\"text\":\"California\"},{\"id\":\"country.4150104525\",\"text\":\"United States\",\"short_code\":\"us\"}]}],\"attribution\":\"NOTICE: © 2015 Mapbox and its suppliers. All rights reserved. Use of this data is subject to the Mapbox Terms of Service (https://www.mapbox.com/about/maps/). This response and the information it contains may not be retained.\"}";
            return Encoding.ASCII.GetBytes(data);
        }

        private byte[] GetForwardGeocodingGoogleResult()
        {
            const string data =
                "{\"results\":[{\"address_components\":[{\"long_name\":\"BS5 6DR\",\"short_name\":\"BS5 6DR\",\"types\":[\"postal_code\"]},{\"long_name\":\"Easton\",\"short_name\":\"Easton\",\"types\":[\"sublocality_level_1\",\"sublocality\",\"political\"]},{\"long_name\":\"Bristol\",\"short_name\":\"Bristol\",\"types\":[\"locality\",\"political\"]},{\"long_name\":\"Bristol\",\"short_name\":\"Bristol\",\"types\":[\"postal_town\"]},{\"long_name\":\"City of Bristol\",\"short_name\":\"City of Bristol\",\"types\":[\"administrative_area_level_2\",\"political\"]},{\"long_name\":\"United Kingdom\",\"short_name\":\"GB\",\"types\":[\"country\",\"political\"]}],\"formatted_address\":\"Bristol, Bristol, City of Bristol BS5 6DR, UK\",\"geometry\":{\"bounds\":{\"northeast\":{\"lat\":51.4689388,\"lng\":-2.5608721},\"southwest\":{\"lat\":51.4680817,\"lng\":-2.5618701}},\"location\":{\"lat\":51.468526,\"lng\":-2.5614786},\"location_type\":\"APPROXIMATE\",\"viewport\":{\"northeast\":{\"lat\":51.4698592302915,\"lng\":-2.560022119708498},\"southwest\":{\"lat\":51.4671612697085,\"lng\":-2.562720080291502}}},\"place_id\":\"ChIJLySUNTiOcUgRKFx4O3Om9Vs\",\"types\":[\"postal_code\"]}],\"status\":\"OK\"}";
            return Encoding.ASCII.GetBytes(data);
        }

        private async Task<TResponse> GetResponse<TResponse>(ApiClient client, IRequest<TResponse> request, byte[] streamResponse)
            where TResponse : class, new()
        {
            Stream responseStream = new MemoryStream(streamResponse);
            client.ResponseHandler = new MockResponseHandler(responseStream);
            return await client.GetAsync(request);
        }

        [Test]
        public async Task CheckThatTheApiClientCorrectlySerialisesAGoogleForwardGeoCodingResponse()
        {
            var key = Guid.NewGuid().ToString();
            var request = new GoogleForwardGeocodingRequest(key)
            {
                Query = "BS5 6DR"
            };

            var apiResponse = await GetResponse(new GoogleClient(), request, GetForwardGeocodingGoogleResult());

            Assert.That(request.Path, Is.StringContaining(string.Format("key={0}",key)));
            Assert.That(apiResponse, Is.Not.Null);
            Assert.That(apiResponse.Results.Length, Is.EqualTo(1));
            Assert.That(apiResponse.Results[0].PlaceId, Is.EqualTo("ChIJLySUNTiOcUgRKFx4O3Om9Vs"));
        }

        [Test]
        public async Task CheckThatTheApiClientCorrectlySerialisesAGoogleReverseGeoCodingResponse()
        {
            var key = Guid.NewGuid().ToString();
            var request = new GoogleReverseGeocodingRequest(key)
            {
                Latitude = 0,
                Longitude = 0
            };

            var apiResponse = await GetResponse(new GoogleClient(), request, GetReverseGeocodingGoogleResult());

            Assert.That(request.Path, Is.StringContaining(string.Format("key={0}", key)));
            Assert.That(apiResponse, Is.Not.Null);
            Assert.That(apiResponse.Results.Length, Is.EqualTo(2));
            Assert.That(apiResponse.Results[0].PlaceId, Is.EqualTo("ChIJO7QcmhqQcUgRV02ITMpiPrw"));
        }

        [Test]
        public void CheckThatTheApiClientCorrectlySerialisesAMapBoxForwardGeoCodingResponse()
        {
            var key = Guid.NewGuid().ToString();
            var request = new MapBoxForwardGeocodingRequest(key)
            {
                Query = "BS5 6DR",
                Types = new[] { Types.postcode }
            };

            var apiResponse = Task.Run(() => GetResponse(new MapBoxClient(), request, GetForwardGeoCodingMapBoxResponseResult())).Result;

            Assert.That(request.Path, Is.StringContaining(string.Format("access_token={0}", key)));
            Assert.That(apiResponse, Is.Not.Null);
            Assert.That(apiResponse.Features.Count, Is.EqualTo(5));
            Assert.That(apiResponse.Features[0].Id, Is.EqualTo("address.39053333360279"));
        }

        [Test]
        public async Task CheckThatTheApiClientCorrectlySerialisesAMapBoxReversGeoCodingResponse()
        {
            var key = Guid.NewGuid().ToString();
            var request = new MapBoxReverseGeocodingRequest(key)
            {
                Latitude = 0,
                Longitude = 0
            };

            var apiResponse = await GetResponse(new MapBoxClient(), request, GetReversGeoCodingMapBoxResponseResult());

            Assert.That(request.Path, Is.StringContaining(string.Format("access_token={0}", key)));
            Assert.That(apiResponse, Is.Not.Null);
            Assert.That(apiResponse.Features.Count, Is.EqualTo(5));
            Assert.That(apiResponse.Features[0].Id, Is.EqualTo("address.5703004565802462"));
        }

        [Test]
        public void CheckThatTheForwardGeoCodingRequestReturnsValidResults()
        {
            var client = Substitute.For<IMapBoxApiClient>();

            var provider = new MapBoxGeoLocationProvider(client);
            var request = new MapBoxForwardGeocodingRequest
            {
                Query = "BS5 6DR",
                Types = new[] { Types.postcode }
            };

            var response = new MapBoxGeocodingResponse();
            client.GetAsync(request).Returns(info => response);

            var apiResponse = provider.GetAsync(request).Result;
            Assert.That(response, Is.SameAs(apiResponse));
        }

        [Test]
        public async Task CheckThatTheReverseGeoCodingRequestReturnsValidResults()
        {
            var client = Substitute.For<IMapBoxApiClient>();

            var provider = new MapBoxGeoLocationProvider(client);
            var request = new MapBoxReverseGeocodingRequest
            {
                Latitude = 51.4500,
                Longitude = -2.5833
            };

            var response = new MapBoxGeocodingResponse();
            client.GetAsync(request).Returns(info => response);

            var apiResponse = await provider.GetAsync(request);
            Assert.That(apiResponse, Is.SameAs(response));
        }
    }

    internal class MockResponseHandler : HttpMessageHandler
    {
        private readonly Stream _responseStream;

        public MockResponseHandler(Stream responseStream)
        {
            _responseStream = responseStream;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Task.FromResult(
                new HttpResponseMessage
                {
                    Content = new StreamContent(_responseStream)
                });
        }
    }
}
