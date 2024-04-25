using MongoDB.Bson.Serialization.Attributes;

namespace eTamir.Services.Appointment.Models
{
    public class Appointments
    {
        [BsonId]
        public string UserId { get; set; }
        [BsonElement("FavItems")]
        public List<AppointmentItem> AppointmentItems { get; set; }
        public Appointments()
        {
            AppointmentItems = new List<AppointmentItem>();
        }
    }
}
