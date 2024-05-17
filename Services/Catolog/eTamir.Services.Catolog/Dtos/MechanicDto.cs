using eTamir.Services.Catolog.Models;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;
using Microsoft.CodeAnalysis;

namespace eTamir.Services.Catolog.Dtos
{
    public class MechanicDto
    {
        public string? Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string PhotoUrl { get; set; }
        [Required]
        public string AddressId { get; set; }
        public decimal? Rating { get; set; }
        public DateTime? CreatedAt { get { return DateTime.Now; } }
        [Required]
        public string CategoryId { get; set; }
        [Required]
        public string CatalogId { get; set; }
        public  string WorkingHoursId  { get; set;}
        public string? UserId { get; set; }
        public byte CountryCode { get; set; }
        public long PhoneNumber { get; set; }
        [BsonIgnore]
        public int Total { get; set; }
    }
}
