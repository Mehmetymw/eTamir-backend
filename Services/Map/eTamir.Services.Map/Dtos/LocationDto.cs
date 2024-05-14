using eTamir.Services.Map.Models;

namespace eTamir.Services.Map.Dtos
{
    public class LocationDto
    {
        public string UserId { get; set; }
        public double[] Coordinates { get; set; }
        public LocationNbResponse Details { get; set; }

    }
}
