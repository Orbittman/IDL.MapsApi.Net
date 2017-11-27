using System.Linq;

using FluentAssertions;

using IDL.MapsApi.Net.Models;

using NUnit.Framework;

namespace IDL.MapsApi.Net.Tests
{
    [TestFixture]
    public class PolyLineExtensionTests
    {
        [Test]
        public void CanExtensionInterpretMultipleValues()
        {
            var expected = new[]
            {
                new Location { Latitude = 38.5, Longitude = -120.2 },
                new Location { Latitude = 40.7, Longitude = -120.95 },
                new Location { Latitude = 43.252, Longitude = -126.453 }
            };

            const string polyLine = "_p~iF~ps|U_ulLnnqC_mqNvxq`@";
            var locations = polyLine.Decode();

            locations.Count().Should().Be(3);
            for (var i = 0; i < expected.Length; i++)
            {
                locations.ElementAt(i).ShouldBeEquivalentTo(expected[i]);
            }
        }

        [Test]
        public void CanDecodePolyLineString()
        {
            var input = new[]
            {
                new Location { Latitude = 38.5, Longitude = -120.2 },
                new Location { Latitude = 40.7, Longitude = -120.95 },
                new Location { Latitude = 43.252, Longitude = -126.453 }
            };

            const string polyLine = "_p~iF~ps|U_ulLnnqC_mqNvxq`@";
            var output = input.Encode();

            output.Should().Match(polyLine);
        }
    }
}
