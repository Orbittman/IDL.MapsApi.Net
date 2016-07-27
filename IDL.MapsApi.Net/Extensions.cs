using System;
using System.Collections.Generic;
using System.Linq;

using IDL.MapsApi.Net.Google.Models;
using IDL.MapsApi.Net.Google.Response;
using IDL.MapsApi.Net.MapBox.Models;
using IDL.MapsApi.Net.MapBox.Response;
using IDL.MapsApi.Net.Models;

using Location = IDL.MapsApi.Net.Models.Location;

namespace IDL.MapsApi.Net
{
    public static class Extensions
    {
        public static ForwardGeocodingResult AsMapsApiGeocodingResult(this MapBoxGeocodingResponse value)
        {
            var response = new ForwardGeocodingResult
            {
                Results = value.Features.Select(
                    f =>
                        new Result
                        {
                            Boundry = new BoundingBox
                            {
                                NorthEast = new Location { Latitude = f.BBox.MinX, Longitude = f.BBox.MinY },
                                SouthWest = new Location { Latitude = f.BBox.MaxX, Longitude = f.BBox.MaxY },
                            },
                            Id = f.Id,
                            Location = new Location
                            {
                                Latitude = f.Center.Y,
                                Longitude = f.Center.X
                            },
                            Address = new Address
                            {
                                FullAddress = f.PlaceName,
                                PropertyId = f.Address,
                                Street = f.Text,
                                SubLocality = GetMapBoxAddressComponent(f.Context, "neighborhood"),
                                Locality = GetMapBoxAddressComponent(f.Context, "locality"),
                                Town = GetMapBoxAddressComponent(f.Context, "place"),
                                Region = GetMapBoxAddressComponent(f.Context, "region"),
                                County = GetMapBoxAddressComponent(f.Context, "region"),
                                Country = GetMapBoxAddressComponent(f.Context, "country"),
                                PostalCode = GetMapBoxAddressComponent(f.Context, "postcode"),
                            }
                        }
                    )
            };

            return response;
        }

        public static ForwardGeocodingResult AsMapsApiGeocodingResult(this GoogleGeocodingResponse value)
        {
            var response = new ForwardGeocodingResult
            {
                Results = value.Results.Select(
                    r =>
                        new Result
                        {
                            Boundry = r.Geometry.Bounds == null
                                ? null
                                : new BoundingBox
                                {
                                    NorthEast = new Location
                                    {
                                        Latitude = r.Geometry.Bounds.NorthEast.Latitude,
                                        Longitude = r.Geometry.Bounds.NorthEast.Longitude
                                    },
                                    SouthWest = new Location
                                    {
                                        Latitude = r.Geometry.Bounds.SouthWest.Latitude,
                                        Longitude = r.Geometry.Bounds.SouthWest.Longitude
                                    }
                                },
                            Id = r.PlaceId,
                            Location = new Location
                            {
                                Latitude = r.Geometry.Location.Latitude,
                                Longitude = r.Geometry.Location.Longitude
                            },
                            Address = new Address
                            {
                                FullAddress = r.FormattedAddress,
                                PropertyId = GetGoogleAddressComponent(r.AddressComponents, "street_number"),
                                Street = GetGoogleAddressComponent(r.AddressComponents, "route"),
                                SubLocality = GetGoogleAddressComponent(r.AddressComponents, "sublocality_level_1"),
                                Locality = GetGoogleAddressComponent(r.AddressComponents, "locality"),
                                Town = GetGoogleAddressComponent(r.AddressComponents, "postal_town"),
                                Region = GetGoogleAddressComponent(r.AddressComponents, "administrative_area_level_1"),
                                County = GetGoogleAddressComponent(r.AddressComponents, "administrative_area_level_2"),
                                Country = GetGoogleAddressComponent(r.AddressComponents, "country"),
                                PostalCode = GetGoogleAddressComponent(r.AddressComponents, "postal_code")
                            }
                        })
            };

            return response;
        }

        public static Location[] ToLocationPoints(this string polyLine)
        {
            var binary = 0;
            var shiftCounter = 0;
            var locations = new List<Location>();
            var iteration = 0;
            var currentLatitude = 0d;
            var currentLongitude = 0d;

            foreach (var characterValue in polyLine.Select(t => t - 63))
            {
                var shift = shiftCounter++ * 5;
                if ((characterValue & 0x20) == 0x20)
                {
                    binary |= (characterValue & ~0x20) << shift;
                }
                else
                {
                    binary |= characterValue << shift;
                    var value = Convert.ToDouble(((binary & 1) == 1 ? ~binary : binary) >> 1) / 1E5;
                    if (iteration++ % 2 == 0)
                    {
                        currentLatitude += value;
                    }
                    else
                    {
                        currentLongitude += value;
                        locations.Add(new Location { Latitude = currentLatitude, Longitude = currentLongitude });
                    }

                    binary = 0;
                    shiftCounter = 0;
                }
            }

            return locations.ToArray();
        }

        private static string GetGoogleAddressComponent(IEnumerable<AddressComponent> addressComponents, string componentName)
        {
            var component = addressComponents.FirstOrDefault(c => c.Types.Contains(componentName));
            return component?.LongName;
        }

        private static string GetMapBoxAddressComponent(IEnumerable<Context> context, string componentName)
        {
            var component = context?.FirstOrDefault(c => c.Id.Split('.')[0] == componentName);
            return component?.Text;
        }
    }
}
