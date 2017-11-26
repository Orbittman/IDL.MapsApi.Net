using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IDL.MapsApi.Net.Client;
using IDL.MapsApi.Net.Google.Models;
using IDL.MapsApi.Net.Google.Request;
using IDL.MapsApi.Net.Google.Response;
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

        private byte[] GetDirectionsResult()
        {
            const string data =
                "{\"geocoded_waypoints\":[{\"geocoder_status\":\"OK\",\"place_id\":\"ChIJyYfhZ79ZwokRMtXcL6CYxkA\",\"types\":[\"premise\"]},{\"geocoder_status\":\"OK\",\"partial_match\":true,\"place_id\":\"ChIJ8YWMWnz4wokRCOVf1CcJCbY\",\"types\":[\"street_address\"]}],\"routes\":[{\"bounds\":{\"northeast\":{\"lat\":40.8171321,\"lng\":-73.99449150000001},\"southwest\":{\"lat\":40.7416627,\"lng\":-74.0728354}},\"copyrights\":\"Map data ©2015 Google\",\"legs\":[{\"distance\":{\"text\":\"9.7 mi\",\"value\":15653},\"duration\":{\"text\":\"25 mins\",\"value\":1480},\"end_address\":\"1 MetLife Stadium Dr, East Rutherford, NJ 07073, USA\",\"end_location\":{\"lat\":40.814505,\"lng\":-74.07272910000002},\"start_address\":\"75 Ninth Ave, New York, NY 10011, USA\",\"start_location\":{\"lat\":40.7428759,\"lng\":-74.00584719999999},\"steps\":[{\"distance\":{\"text\":\"440 ft\",\"value\":134},\"duration\":{\"text\":\"1 min\",\"value\":34},\"end_location\":{\"lat\":40.7422925,\"lng\":-74.004457},\"html_instructions\":\"Head\u003cb\u003esoutheast\u003c/b\u003e on\u003cb\u003eW 16th St\u003c/b\u003e toward\u003cb\u003eNinth Ave\u003c/b\u003e\",\"polyline\":{\"points\":\"_rtwFpgubMtBuG\"},\"start_location\":{\"lat\":40.7428759,\"lng\":-74.00584719999999},\"travel_mode\":\"DRIVING\"},{\"distance\":{\"text\":\"49 ft\",\"value\":15},\"duration\":{\"text\":\"1 min\",\"value\":29},\"end_location\":{\"lat\":40.7421744,\"lng\":-74.0045361},\"html_instructions\":\"Turn\u003cb\u003eright\u003c/b\u003e at the 1st cross street onto\u003cb\u003eNinth Ave\u003c/b\u003e\",\"maneuver\":\"turn-right\",\"polyline\":{\"points\":\"intwFz~tbMVN\"},\"start_location\":{\"lat\":40.7422925,\"lng\":-74.004457},\"travel_mode\":\"DRIVING\"},{\"distance\":{\"text\":\"226 ft\",\"value\":69},\"duration\":{\"text\":\"1 min\",\"value\":24},\"end_location\":{\"lat\":40.7416627,\"lng\":-74.0049708},\"html_instructions\":\"Slight\u003cb\u003eright\u003c/b\u003e to stay on\u003cb\u003eNinth Ave\u003c/b\u003e\",\"maneuver\":\"turn-slight-right\",\"polyline\":{\"points\":\"qmtwFj_ubMDN@@?@JFXP`@VTN\"},\"start_location\":{\"lat\":40.7421744,\"lng\":-74.0045361},\"travel_mode\":\"DRIVING\"},{\"distance\":{\"text\":\"0.2 mi\",\"value\":266},\"duration\":{\"text\":\"1 min\",\"value\":74},\"end_location\":{\"lat\":40.74282729999999,\"lng\":-74.00773459999999},\"html_instructions\":\"Turn\u003cb\u003eright\u003c/b\u003e onto\u003cb\u003eW 15th St\u003c/b\u003e\",\"maneuver\":\"turn-right\",\"polyline\":{\"points\":\"kjtwF`bubMIVeBlFEN{A|EWr@\"},\"start_location\":{\"lat\":40.7416627,\"lng\":-74.0049708},\"travel_mode\":\"DRIVING\"},{\"distance\":{\"text\":\"1.1 mi\",\"value\":1815},\"duration\":{\"text\":\"6 mins\",\"value\":349},\"end_location\":{\"lat\":40.7571135,\"lng\":-73.9973176},\"html_instructions\":\"Turn\u003cb\u003eright\u003c/b\u003e at the 1st cross street onto\u003cb\u003e10th Ave\u003c/b\u003e\",\"maneuver\":\"turn-right\",\"polyline\":{\"points\":\"uqtwFhsubMo@ ...points truncated in this example\"},\"start_location\":{\"lat\":40.74282729999999,\"lng\":-74.00773459999999},\"travel_mode\":\"DRIVING\"},{\"distance\":{\"text\":\"0.2 mi\",\"value\":273},\"duration\":{\"text\":\"1 min\",\"value\":59},\"end_location\":{\"lat\":40.7559092,\"lng\":-73.99449150000001},\"html_instructions\":\"Turn\u003cb\u003eright\u003c/b\u003e onto\u003cb\u003eW 38th St\u003c/b\u003e\",\"maneuver\":\"turn-right\",\"polyline\":{\"points\":\"}jwwFfrsbMzA}E@CNe@Tu@d@uAHYHU@AHWDM^iA\"},\"start_location\":{\"lat\":40.7571135,\"lng\":-73.9973176},\"travel_mode\":\"DRIVING\"},{\"distance\":{\"text\":\"377 ft\",\"value\":115},\"duration\":{\"text\":\"1 min\",\"value\":38},\"end_location\":{\"lat\":40.7550018,\"lng\":-73.9951569},\"html_instructions\":\"Turn\u003cb\u003eright\u003c/b\u003e at the 1st cross street onto\u003cb\u003eNinth Ave\u003c/b\u003e\",\"maneuver\":\"turn-right\",\"polyline\":{\"points\":\"mcwwFp`sbMtBtA~@n@\"},\"start_location\":{\"lat\":40.7559092,\"lng\":-73.99449150000001},\"travel_mode\":\"DRIVING\"},{\"distance\":{\"text\":\"0.1 mi\",\"value\":226},\"duration\":{\"text\":\"1 min\",\"value\":39},\"end_location\":{\"lat\":40.7560886,\"lng\":-73.9950563},\"html_instructions\":\"Slight\u003cb\u003eright\u003c/b\u003e onto the\u003cb\u003eLincoln Tunnel\u003c/b\u003e ramp to\u003cb\u003eNew Jersey\u003c/b\u003e\",\"polyline\":{\"points\":\"w}vwFvdsbMJb@ ...points truncated in this example\"},\"start_location\":{\"lat\":40.7550018,\"lng\":-73.9951569},\"travel_mode\":\"DRIVING\"},{\"distance\":{\"text\":\"112 ft\",\"value\":34},\"duration\":{\"text\":\"1 min\",\"value\":6},\"end_location\":{\"lat\":40.7563822,\"lng\":-73.9949785},\"html_instructions\":\"Merge onto\u003cb\u003eNY-495 W\u003c/b\u003e\",\"maneuver\":\"merge\",\"polyline\":{\"points\":\"qdwwFbdsbMECECCAEAEAE?EAE?E?E?\"},\"start_location\":{\"lat\":40.7560886,\"lng\":-73.9950563},\"travel_mode\":\"DRIVING\"},{\"distance\":{\"text\":\"0.9 mi\",\"value\":1453},\"duration\":{\"text\":\"2 mins\",\"value\":124},\"end_location\":{\"lat\":40.7631296,\"lng\":-74.00948579999999},\"html_instructions\":\"Keep\u003cb\u003eright\u003c/b\u003e at the fork to stay on\u003cb\u003eNY-495 W\u003c/b\u003e\u003cdiv style=\\\"font - size:0.9em\\\"\u003eEntering New Jersey\u003c/div\u003e\",\"maneuver\":\"fork-right\",\"polyline\":{\"points\":\"kfwwFrcsbMG? ...points truncated in this example\"},\"start_location\":{\"lat\":40.7563822,\"lng\":-73.9949785},\"travel_mode\":\"DRIVING\"},{\"distance\":{\"text\":\"2.8 mi\",\"value\":4516},\"duration\":{\"text\":\"5 mins\",\"value\":301},\"end_location\":{\"lat\":40.7767179,\"lng\":-74.042813},\"html_instructions\":\"Continue onto\u003cb\u003eNJ-495 W\u003c/b\u003e\",\"polyline\":{\"points\":\"qpxwFh~ubMs ...points truncated in this example\"},\"start_location\":{\"lat\":40.7631296,\"lng\":-74.00948579999999},\"travel_mode\":\"DRIVING\"},{\"distance\":{\"text\":\"0.3 mi\",\"value\":435},\"duration\":{\"text\":\"1 min\",\"value\":21},\"end_location\":{\"lat\":40.7797406,\"lng\":-74.04597939999999},\"html_instructions\":\"Keep\u003cb\u003eright\u003c/b\u003e at the fork to continue on\u003cb\u003eNJ-3 W\u003c/b\u003e, follow signs for\u003cb\u003eNew Jersey 3 W\u003c/b\u003e/\u003cb\u003eGarden State Parkway\u003c/b\u003e/\u003cb\u003eSecaucus\u003c/b\u003e\",\"maneuver\":\"fork-right\",\"polyline\":{\"points\":\"oe{wFpn|bMc@ ...points truncated in this example\"},\"start_location\":{\"lat\":40.7767179,\"lng\":-74.042813},\"travel_mode\":\"DRIVING\"},{\"distance\":{\"text\":\"2.2 mi\",\"value\":3560},\"duration\":{\"text\":\"2 mins\",\"value\":141},\"end_location\":{\"lat\":40.8041562,\"lng\":-74.0717843},\"html_instructions\":\"Keep\u003cb\u003eleft\u003c/b\u003e to stay on\u003cb\u003eNJ-3 W\u003c/b\u003e, follow signs for\u003cb\u003eNew Jersey 3 W\u003c/b\u003e/\u003cb\u003eClifton\u003c/b\u003e\",\"maneuver\":\"keep-left\",\"polyline\":{\"points\":\"kx{wFjb}bMMJ ...points truncated in this example\"},\"start_location\":{\"lat\":40.7797406,\"lng\":-74.04597939999999},\"travel_mode\":\"DRIVING\"},{\"distance\":{\"text\":\"0.2 mi\",\"value\":249},\"duration\":{\"text\":\"1 min\",\"value\":13},\"end_location\":{\"lat\":40.8062905,\"lng\":-74.07245929999999},\"html_instructions\":\"Take the\u003cb\u003eNJ-120 N\u003c/b\u003e exit toward\u003cb\u003eE Rutherford\u003c/b\u003e\",\"maneuver\":\"ramp-right\",\"polyline\":{\"points\":\"_q`xFrcbcMSB ...points truncated in this example\"},\"start_location\":{\"lat\":40.8041562,\"lng\":-74.0717843},\"travel_mode\":\"DRIVING\"},{\"distance\":{\"text\":\"410 ft\",\"value\":125},\"duration\":{\"text\":\"1 min\",\"value\":6},\"end_location\":{\"lat\":40.807415,\"lng\":-74.07250909999999},\"html_instructions\":\"Keep\u003cb\u003eleft\u003c/b\u003e, follow signs for\u003cb\u003eState Route 503 N\u003c/b\u003e/\u003cb\u003eNew Jersey 120 N\u003c/b\u003e/\u003cb\u003eWashington Avenue\u003c/b\u003e/\u003cb\u003eMoonachie\u003c/b\u003e/\u003cb\u003ePaterson Plank Road\u003c/b\u003e\",\"maneuver\":\"keep-left\",\"polyline\":{\"points\":\"i~`xFzgbcMaFH\"},\"start_location\":{\"lat\":40.8062905,\"lng\":-74.07245929999999},\"travel_mode\":\"DRIVING\"},{\"distance\":{\"text\":\"0.5 mi\",\"value\":869},\"duration\":{\"text\":\"1 min\",\"value\":38},\"end_location\":{\"lat\":40.8145647,\"lng\":-74.06878929999999},\"html_instructions\":\"Continue onto\u003cb\u003eNJ-120 N\u003c/b\u003e\",\"polyline\":{\"points\":\"keaxFdhbcMc@ ...points truncated in this example\"},\"start_location\":{\"lat\":40.807415,\"lng\":-74.07250909999999},\"travel_mode\":\"DRIVING\"},{\"distance\":{\"text\":\"0.2 mi\",\"value\":339},\"duration\":{\"text\":\"1 min\",\"value\":16},\"end_location\":{\"lat\":40.8170874,\"lng\":-74.0665298},\"html_instructions\":\"Continue straight onto\u003cb\u003eWashington Ave\u003c/b\u003e\",\"maneuver\":\"straight\",\"polyline\":{\"points\":\"_rbxF|pacMm@c@ ...points truncated in this example\"},\"start_location\":{\"lat\":40.8145647,\"lng\":-74.06878929999999},\"travel_mode\":\"DRIVING\"},{\"distance\":{\"text\":\"0.2 mi\",\"value\":358},\"duration\":{\"text\":\"1 min\",\"value\":38},\"end_location\":{\"lat\":40.8143541,\"lng\":-74.0648063},\"html_instructions\":\"Turn\u003cb\u003eright\u003c/b\u003e onto\u003cb\u003ePaterson Plank Rd\u003c/b\u003e\",\"maneuver\":\"turn-right\",\"polyline\":{\"points\":\"yacxFxbacM ...points truncated in this example\"},\"start_location\":{\"lat\":40.8170874,\"lng\":-74.0665298},\"travel_mode\":\"DRIVING\"},{\"distance\":{\"text\":\"79 ft\",\"value\":24},\"duration\":{\"text\":\"1 min\",\"value\":4},\"end_location\":{\"lat\":40.8141899,\"lng\":-74.06498189999999},\"html_instructions\":\"Turn\u003cb\u003eright\u003c/b\u003e toward\u003cb\u003eN Connection Rd\u003c/b\u003e\",\"maneuver\":\"turn-right\",\"polyline\":{\"points\":\"upbxF`x`cMTVHH\"},\"start_location\":{\"lat\":40.8143541,\"lng\":-74.0648063},\"travel_mode\":\"DRIVING\"},{\"distance\":{\"text\":\"0.1 mi\",\"value\":177},\"duration\":{\"text\":\"1 min\",\"value\":25},\"end_location\":{\"lat\":40.8138003,\"lng\":-74.06693709999999},\"html_instructions\":\"Turn\u003cb\u003eright\u003c/b\u003e onto\u003cb\u003eN Connection Rd\u003c/b\u003e\",\"maneuver\":\"turn-right\",\"polyline\":{\"points\":\"uobxFby`cMFHFFFHPPDzATtDFfA\"},\"start_location\":{\"lat\":40.8141899,\"lng\":-74.06498189999999},\"travel_mode\":\"DRIVING\"},{\"distance\":{\"text\":\"0.3 mi\",\"value\":548},\"duration\":{\"text\":\"1 min\",\"value\":73},\"end_location\":{\"lat\":40.8149711,\"lng\":-74.0728354},\"html_instructions\":\"Continue straight onto\u003cb\u003eRd D\u003c/b\u003e\",\"maneuver\":\"straight\",\"polyline\":{\"points\":\"gmbxFjeacMDjA ...points truncated in this example\"},\"start_location\":{\"lat\":40.8138003,\"lng\":-74.06693709999999},\"travel_mode\":\"DRIVING\"},{\"distance\":{\"text\":\"174 ft\",\"value\":53},\"duration\":{\"text\":\"1 min\",\"value\":28},\"end_location\":{\"lat\":40.814505,\"lng\":-74.07272910000002},\"html_instructions\":\"Turn\u003cb\u003eleft\u003c/b\u003e onto\u003cb\u003eMetLife Stadium Dr\u003c/b\u003e\u003cdiv style=\\\"font - size:0.9em\\\"\u003eDestination will be on the right\u003c/div\u003e\",\"maneuver\":\"turn-left\",\"polyline\":{\"points\":\"qtbxFfjbcMf@AHAFA^MBA\"},\"start_location\":{\"lat\":40.8149711,\"lng\":-74.0728354},\"travel_mode\":\"DRIVING\"}],\"via_waypoint\":[]}],\"overview_polyline\":{\"points\":\"_rtwFpgubMt ...overview polyline truncated in this example\"},\"summary\":\"NJ-495 W and NJ-3 W\",\"warnings\":[],\"waypoint_order\":[]}], \"status\":\"OK\"}";
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
        public void CheckThatTheApiClientCorrectlySerialisesAGoogleDirectionsResponse()
        {
            var key = Guid.NewGuid().ToString();
            var request = new GoogleDirectionsRequest(key)
            {
                Origin = "Bristol",
                Destination = "Bath"
            };

            var apiResponse = GetResponse(new ApiClient(), request, GetDirectionsResult()).Result;

            Assert.That(request.Path, Is.StringContaining($"key={key}"));
            Assert.That(apiResponse, Is.Not.Null);
            Assert.That(apiResponse.Routes.Length, Is.EqualTo(1));

            Assert.That(apiResponse.Routes[0].PolyLine.Points, Is.Not.Empty);
            Assert.That(apiResponse.Routes[0].PolyLine.StepLocations.Length, Is.EqualTo(1));
            Assert.That(apiResponse.Routes[0].PolyLine.StepLocations[0].Latitude, Is.EqualTo(40.74288));
            Assert.That(apiResponse.Routes[0].PolyLine.StepLocations[0].Longitude, Is.EqualTo(-74.00585));

            Assert.That(apiResponse.Routes[0].Bounds.NorthEast.Latitude, Is.EqualTo(40.8171321));
            Assert.That(apiResponse.Routes[0].Bounds.NorthEast.Longitude, Is.EqualTo(-73.99449150000001));
            Assert.That(apiResponse.Routes[0].Bounds.SouthWest.Latitude, Is.EqualTo(40.7416627));
            Assert.That(apiResponse.Routes[0].Bounds.SouthWest.Longitude, Is.EqualTo(-74.0728354));

            Assert.That(apiResponse.Routes[0].Legs.Length, Is.EqualTo(1));
            Assert.That(apiResponse.Routes[0].Legs[0].Distance.Text, Is.EqualTo("9.7 mi"));
            Assert.That(apiResponse.Routes[0].Legs[0].Distance.Value, Is.EqualTo(15653));
            Assert.That(apiResponse.Routes[0].Legs[0].Duration.Text, Is.EqualTo("25 mins"));
            Assert.That(apiResponse.Routes[0].Legs[0].Duration.Value, Is.EqualTo(1480));
            Assert.That(apiResponse.Routes[0].Legs[0].StartAddress, Is.EqualTo("75 Ninth Ave, New York, NY 10011, USA"));
            Assert.That(apiResponse.Routes[0].Legs[0].Start.Latitude, Is.EqualTo(40.7428759));
            Assert.That(apiResponse.Routes[0].Legs[0].Start.Longitude, Is.EqualTo(-74.00584719999999));
            Assert.That(apiResponse.Routes[0].Legs[0].EndAddress, Is.EqualTo("1 MetLife Stadium Dr, East Rutherford, NJ 07073, USA"));
            Assert.That(apiResponse.Routes[0].Legs[0].End.Latitude, Is.EqualTo(40.814505));
            Assert.That(apiResponse.Routes[0].Legs[0].End.Longitude, Is.EqualTo(-74.07272910000002));

            Assert.That(apiResponse.Routes[0].Legs[0].Steps.Length, Is.EqualTo(22));
            Assert.That(apiResponse.Routes[0].Legs[0].Steps[0].Distance.Text, Is.EqualTo("440 ft"));
            Assert.That(apiResponse.Routes[0].Legs[0].Steps[0].Distance.Value, Is.EqualTo(134));
            Assert.That(apiResponse.Routes[0].Legs[0].Steps[0].Duration.Text, Is.EqualTo("1 min"));
            Assert.That(apiResponse.Routes[0].Legs[0].Steps[0].Duration.Value, Is.EqualTo(34));
            Assert.That(apiResponse.Routes[0].Legs[0].Steps[0].End.Latitude, Is.EqualTo(40.7422925));
            Assert.That(apiResponse.Routes[0].Legs[0].Steps[0].End.Longitude, Is.EqualTo(-74.004457));
            Assert.That(apiResponse.Routes[0].Legs[0].Steps[0].Start.Latitude, Is.EqualTo(40.7428759));
            Assert.That(apiResponse.Routes[0].Legs[0].Steps[0].Start.Longitude, Is.EqualTo(-74.00584719999999));
            Assert.That(apiResponse.Routes[0].Legs[0].Steps[0].PolyLine.Points, Is.EqualTo("_rtwFpgubMtBuG"));
            Assert.That(apiResponse.Routes[0].Legs[0].Steps[0].TravelMode, Is.EqualTo("DRIVING"));

            Assert.That(apiResponse.Waypoints.Length, Is.EqualTo(2));
            Assert.That(apiResponse.Waypoints[1].PlaceId, Is.EqualTo("ChIJ8YWMWnz4wokRCOVf1CcJCbY"));
            Assert.That(apiResponse.Waypoints[1].Types, Contains.Item("street_address"));
            Assert.That(apiResponse.Waypoints[1].PartialMatch, Is.True);
        }

        [Test]
        public async void CheckThatTheApiClientCorrectlySerialisesAGoogleForwardGeoCodingResponse()
        {
            var key = Guid.NewGuid().ToString();
            var request = new GoogleForwardGeocodingRequest(key)
            {
                Address = "BS5 6DR"
            };

            var apiResponse = await GetResponse(new ApiClient(), request, GetForwardGeocodingGoogleResult());

            Assert.That(request.Path, Is.StringContaining($"key={key}"));
            Assert.That(apiResponse, Is.Not.Null);
            Assert.That(apiResponse.Results.Length, Is.EqualTo(1));
            Assert.That(apiResponse.Results[0].PlaceId, Is.EqualTo("ChIJLySUNTiOcUgRKFx4O3Om9Vs"));
        }

        [Test]
        public async void CheckThatTheApiClientCorrectlySerialisesAGoogleReverseGeoCodingResponse()
        {
            var key = Guid.NewGuid().ToString();
            var request = new GoogleReverseGeocodingRequest(key)
            {
                Latitude = 0,
                Longitude = 0
            };

            var apiResponse = await GetResponse(new ApiClient(), request, GetReverseGeocodingGoogleResult());

            Assert.That(request.Path, Is.StringContaining($"key={key}"));
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

            var apiResponse = Task.Run(() => GetResponse(new ApiClient(), request, GetForwardGeoCodingMapBoxResponseResult())).Result;

            Assert.That(request.Path, Is.StringContaining($"access_token={key}"));
            Assert.That(apiResponse, Is.Not.Null);
            Assert.That(apiResponse.Features.Count, Is.EqualTo(5));
            Assert.That(apiResponse.Features[0].Id, Is.EqualTo("address.39053333360279"));
        }

        [Test]
        public async void CheckThatTheApiClientCorrectlySerialisesAMapBoxReversGeoCodingResponse()
        {
            var key = Guid.NewGuid().ToString();
            var request = new MapBoxReverseGeocodingRequest(key)
            {
                Latitude = 0,
                Longitude = 0
            };

            var apiResponse = await GetResponse(new ApiClient(), request, GetReversGeoCodingMapBoxResponseResult());

            Assert.That(request.Path, Is.StringContaining($"access_token={key}"));
            Assert.That(apiResponse, Is.Not.Null);
            Assert.That(apiResponse.Features.Count, Is.EqualTo(5));
            Assert.That(apiResponse.Features[0].Id, Is.EqualTo("address.5703004565802462"));
        }

        [Test]
        public void CheckThatTheClientThowsAnExceptionWhenNoRootPathIsSupplied()
        {
            var key = Guid.NewGuid().ToString();
            var request = new GoogleForwardGeocodingRequest(key)
            {
                Address = "BS5 6DR",
                RootPath = string.Empty
            };

            var exception = Assert.Throws<AggregateException>(() => { var bob = new ApiClient().GetAsync(request).Result; });
            Assert.That(exception.InnerExceptions.Count, Is.EqualTo(1));
            Assert.That(exception.InnerExceptions.First().GetType(), Is.EqualTo(typeof(NullReferenceException)));
        }

        [Test]
        public void CheckThatTheDirectionApiRequestHasAcorrectlyFormattedUrl()
        {
            var key = Guid.NewGuid().ToString("N");
            var origin = Guid.NewGuid().ToString("N");
            var destination = Guid.NewGuid().ToString("N");
            var request = new GoogleDirectionsRequest(key)
            {
                Origin = origin,
                Destination = destination
            };

            Assert.That(request.Path, Is.EqualTo($"directions/json?destination={destination}&origin={origin}&key={key}"));
        }

        [Test]
        public void CheckThatTheForwardGeoCodingRequestReturnsValidResults()
        {
            var client = Substitute.For<IApiClient>();

            var provider = new MappingDataProvider(client);
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
        public void CheckThatTheGoogleDirectionsRequestReturnsValidResults()
        {
            var client = Substitute.For<IApiClient>();
            var provider = new MappingDataProvider(client);
            var request = new GoogleDirectionsRequest
            {
                Origin = "Bristol",
                Destination = "Bath"
            };

            var response = new GoogleDirectionsResponse();
            client.GetAsync(request).Returns(info => response);

            var apiResponse = provider.GetAsync(request).Result;
            Assert.That(response, Is.SameAs(apiResponse));
        }

        [Test]
        public async void CheckThatTheReverseGeoCodingRequestReturnsValidResults()
        {
            var client = Substitute.For<IApiClient>();

            var provider = new MappingDataProvider(client);
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

        [Test]
        public void CheckThatTheGoogleDirectionsHandlesTheGoogleCredentialsConstructorParameterCorrectly()
        {
            var key = Guid.NewGuid().ToString("N");
            var clientId = "FixedId";
            var secretKey = "vNIXE0xscrmjlyV-12Nj_BvUPaw=";
            var signture = "ZZg46DhQazg8Vb9hQNs42OVhuvs=";

            var directionsRequest = new GoogleDirectionsRequest(new GoogleCredentials(key)){RootPath = "http://test.com"};
            Assert.That(directionsRequest.Path, Is.StringContaining($"key={key}"));
            Assert.That(directionsRequest.Path, Is.Not.StringContaining("client="));
            Assert.That(directionsRequest.Path, Is.Not.StringContaining("signature="));

            directionsRequest = new GoogleDirectionsRequest(new GoogleCredentials(clientId, secretKey));
            Assert.That(directionsRequest.Path, Is.Not.StringContaining($"key={key}"));
            Assert.That(directionsRequest.Path, Is.StringContaining($"client={clientId}"));
            Assert.That(directionsRequest.Path, Is.StringContaining($"signature={signture}"));

            Assert.Throws<ArgumentException>(
                () =>
                {
                    var request = new GoogleDirectionsRequest((GoogleCredentials)null);
                    var path = request.Path;
                });
        }
    }
}
