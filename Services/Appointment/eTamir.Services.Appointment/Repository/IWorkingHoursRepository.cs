using AutoMapper;
using eTamir.Services.Catolog.Repository;
using eTamir.Services.Appointment.Dtos;
using eTamir.Services.Appointment.Models;
using eTamir.Shared.Dtos;
using MongoDB.Driver;
using eTamir.Services.Catolog.Models;

namespace eTamir.Services.Appointment.Repository
{
    public interface IWorkingHoursRepository<T> : IRepository<T> where T : WorkingHours
    {
        public IMongoCollection<T> Collection { get; }
        public IMapper Mapper { get; }
    }
}