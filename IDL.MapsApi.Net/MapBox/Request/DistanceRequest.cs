using IDL.MapsApi.Net.MapBox.Response;

namespace IDL.MapsApi.Net.MapBox.Request
{
    public class DistanceRequest : MapBoxApiRequest, IRequest<DistanceResponse>
    {
        public DistanceProfile Profile { get; set; }

        protected override string RequestSpecificPath => $"distances/v1/mapbox/{Profile}";
    }
}
