using AutoMapper;
using eTamir.Services.Appointment.Settings;
using eTamir.Services.Appointment.Models;
using MongoDB.Driver;
using eTamir.Services.Catolog.Models;
using eTamir.Services.Catolog.Repository;

namespace eTamir.Services.Appointment.Repository
{
    public class WorkingHoursRepository : IWorkingHoursRepository<WorkingHours>
    {
        public IMongoCollection<WorkingHours> Collection { get; }

        public IMapper Mapper { get; }
        public WorkingHoursRepository(IMapper mapper, IDatabaseSettings dbSettins)
        {
            var client = new MongoClient(dbSettins.ConnectionString);
            var database = client.GetDatabase(dbSettins.DatabaseName);

            Collection = database.GetCollection<WorkingHours>(dbSettins.WorkingHoursCollectionName);
            Mapper = mapper;
        }
    }
}
