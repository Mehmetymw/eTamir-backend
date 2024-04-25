using AutoMapper;
using eTamir.Services.Catolog.Repository;
using eTamir.Services.Appointment.Dtos;
using eTamir.Services.Appointment.Models;
using eTamir.Services.Appointment.Settings;
using MongoDB.Driver;

namespace eTamir.Services.Appointment.Repository
{
   public class AppointmentRepository : IAppointmentRepository<Appointments>
    {
        public IMongoCollection<Appointments> Collection { get; }

        public IMapper Mapper {  get; }
        public AppointmentRepository(IMapper mapper, IDatabaseSettings dbSettins)
        {
            var client = new MongoClient(dbSettins.ConnectionString);
            var database = client.GetDatabase(dbSettins.DatabaseName);

            Collection = database.GetCollection<Appointments>(dbSettins.FavoritesCollectionName);
            Mapper = mapper;
        }
    }
}