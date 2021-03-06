﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public static IEnumerable<Location> Decode(this string polyLine)
        {
            var binary = 0;
            var shiftCounter = 0;
            var locations = new List<Location>();
            var state = false;
            var currentLatitude = 0d;
            var currentLongitude = 0d;
            var charArray = polyLine.ToCharArray();

            foreach (var charx in charArray)
            {
                var character = charx - 63;
                if ((character & 32) == 32)
                {
                    binary |= (character & ~32) << shiftCounter;
                    shiftCounter += 5;
                }
                else
                {
                    binary |= character << shiftCounter;
                    if (state = !state)
                    {
                        currentLatitude += (binary & 1) == 1 ? ~binary >> 1 : binary >> 1;
                    }
                    else
                    {
                        currentLongitude += (binary & 1) == 1 ? ~binary >> 1 : binary >> 1;
                        locations.Add(new Location
                        {
                            Latitude = currentLatitude / 1E5,
                            Longitude = currentLongitude / 1E5
                        });
                    }

                    binary = 0;
                    shiftCounter = 0;
                }
            }

            return locations;
        }

        public static string Encode(this IEnumerable<Location> locations)
        {
            var polylineBuilder = new StringBuilder();
            void EncodeValue(int value)
            {
                var returnValue = value << 1;
                if (returnValue < 0)
                {
                    returnValue = ~returnValue;
                }

                while (returnValue >= 0x20)
                {
                    polylineBuilder.Append((char)((0x20 | (returnValue & 0x1f)) + 63));
                    returnValue >>= 5;
                }

                polylineBuilder.Append((char)(returnValue + 63));
            }

            var lastLatitude = 0;
            var lastLongitude = 0;
            const float multiplier = 1E5F;

            foreach (var location in locations)
            {
                var latitudeValue = (int)Math.Round(location.Latitude * multiplier);
                var longitudeValue = (int)Math.Round(location.Longitude * multiplier);

                EncodeValue(latitudeValue - lastLatitude);
                EncodeValue(longitudeValue - lastLongitude);

                lastLatitude = latitudeValue;
                lastLongitude = longitudeValue;
            }

            return polylineBuilder.ToString();
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
