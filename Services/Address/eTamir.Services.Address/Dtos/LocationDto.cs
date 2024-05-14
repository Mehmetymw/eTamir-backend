

using MongoDB.Driver.GeoJsonObjectModel;

namespace eTamir.Services.Address.Dtos
{
    public class LocationDto
    {
        public double[] Coordinates { get; set; }
        public MechanicAddressDto Address { get; set; }
    }
}