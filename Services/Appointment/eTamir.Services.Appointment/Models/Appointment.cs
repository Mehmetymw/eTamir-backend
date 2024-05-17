using MongoDB.Bson.Serialization.Attributes;

namespace eTamir.Services.Appointment.Models
{
    public class Appointment
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public string MechanicId { get; set; }
        public string UserId { get; set; }
        public AppointmentStatus AppointmentStatus { get; set; } = AppointmentStatus.Pending;
        public DateTime DateTime { get; set; }
        public DayOfWeek DayOfWeek { get; set; }

    }
}