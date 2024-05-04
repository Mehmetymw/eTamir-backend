using System;
using MongoDB.Bson.Serialization.Attributes;

namespace eTamir.Services.Address.Models
{
    public class MechanicAddress
    {
    
        public string? No { get; set; }
        public string Title { get; set; }
        public string? Street { get; set; }
        public int StateId { get; set; }
        public int CityId { get; set; }
        public int CountryId { get; set; }
    }
}