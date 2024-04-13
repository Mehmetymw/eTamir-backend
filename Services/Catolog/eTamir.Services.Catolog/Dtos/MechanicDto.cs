using eTamir.Services.Catolog.Models;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace eTamir.Services.Catolog.Dtos
{
    public class MechanicDto
    {
        public string? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }
        public string UserId { get; set; }
        public DateTime? CreatedAt { get { return DateTime.Now; } }
        public string CategoryId { get; set; }
    }
}
