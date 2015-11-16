using IDL.MapsApi.Net.MapBox.Response;

namespace IDL.MapsApi.Net.MapBox.Request
{
    public class DistanceRequest : ApiRequest, IRequest<DistanceResponse>
    {
        public DistanceProfile Profile { get; set; }

        protected override string RequestSpecificPath
        {
            get { return string.Format("distances/v1/mapbox/{0}", Profile); }
        }

        public string RootPath { get; private set; }
    }
}
