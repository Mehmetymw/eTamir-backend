using System;
using MongoDB.Bson.Serialization.Attributes;

namespace eTamir.Services.Catolog.Models
{
    public class WorkingHours
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public WorkingDate[] WorkingDates { get; set; }
        public int MaxAppointmentsPerDay { get; set; }
        public double AppointmentInterval { get; set; }
        public bool IsActive { get; set; }
    }
}