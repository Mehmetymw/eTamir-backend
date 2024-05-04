using System;
using MongoDB.Bson.Serialization.Attributes;

namespace eTamir.Services.Address.Dtos
{
    public class MechanicAddressDto
    {

        public string? No { get; set; }
        public string Title { get; set; }
        public string? Street { get; set; }
        public int StateId { get; set; }
        public int CityId { get; set; }
        public int CountryId { get; set; }
    }
}