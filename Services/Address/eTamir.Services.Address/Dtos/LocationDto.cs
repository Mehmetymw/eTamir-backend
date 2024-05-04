using System;
using eTamir.Services.Address.Models;

namespace eTamir.Services.Address.Dtos
{
    public class LocationDto
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public MechanicAddressDto Address { get; set; }
    }
}