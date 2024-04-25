using AutoMapper;
using eTamir.Services.Catolog.Repository;
using eTamir.Services.Appointment.Dtos;
using eTamir.Services.Appointment.Models;
using eTamir.Shared.Dtos;
using MongoDB.Driver;

namespace eTamir.Services.Appointment.Repository
{
    public interface IAppointmentRepository<T> : IRepository<T> where T : Appointments
    {
        public IMongoCollection<T> Collection { get; }
        public IMapper Mapper { get; }
    }
}