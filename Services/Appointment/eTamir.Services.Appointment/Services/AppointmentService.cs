using eTamir.Services.Catolog.Repository;
using eTamir.Services.Appointment.Dtos;
using eTamir.Services.Appointment.Models;
using eTamir.Services.Appointment.Repository;
using eTamir.Services.Appointment.Settings;
using eTamir.Shared.Dtos;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace eTamir.Services.Appointment.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository<Appointments> appointmentsRepository;
        private readonly IOptions<IDatabaseSettings> databaseSettings;

        public AppointmentService(IAppointmentRepository<Appointments> appointmentsRepository, IOptions<IDatabaseSettings> databaseSettings)
        {
            this.databaseSettings = databaseSettings;
            this.appointmentsRepository = appointmentsRepository;
        }

        public async Task<Response<NoContent>> Add(string userId,AppointmentDto appointmentDto)
        {
            try
            {
                var appointments = await appointmentsRepository.Collection
                    .Find(x => x.UserId == userId)
                    .FirstOrDefaultAsync();

                if (appointments != null && appointments.AppointmentItems.Any(t => t.MechanicId == appointmentDto.AppointmentItem.MechanicId))
                {
                    return Response<NoContent>.Fail("This mechanic is already in your favorites", 400);
                }

                if (appointments == null)
                {
                    appointments = new Appointments();
                    appointments.UserId = userId;
                }

                appointments.AppointmentItems.Add(appointmentsRepository.Mapper.Map<AppointmentItem>(appointmentDto.AppointmentItem));

                await appointmentsRepository.Collection.ReplaceOneAsync(x => x.UserId == appointments.UserId, appointments, new ReplaceOptions { IsUpsert = true });

                return Response<NoContent>.Success(204);
            }
            catch (Exception ex)
            {
                return Response<NoContent>.Fail("Error while adding fav", 400);
            }
        }



        public async Task<Response<NoContent>> Delete(string userId, AppointmentDto appointmentDto)
        {
            try
            {
                var appointments = await appointmentsRepository.Collection
                    .Find(x => x.UserId == userId)
                    .FirstOrDefaultAsync();

                if (appointments == null)
                {
                    return Response<NoContent>.Fail("No favorite mechanic found for the given user", 404);
                }

                var favItemToRemove = appointments.AppointmentItems.FirstOrDefault(t => t.MechanicId == appointmentDto.AppointmentItem.MechanicId);
                if (favItemToRemove != null)
                {
                    appointments.AppointmentItems.Remove(favItemToRemove);
                    await appointmentsRepository.Collection.ReplaceOneAsync(x => x.UserId == appointments.UserId, appointments);
                    return Response<NoContent>.Success(204);
                }

                return Response<NoContent>.Fail("Favorite mechanic not found for the given user", 404);
            }
            catch
            {
                return Response<NoContent>.Fail("Error while deleting favorite mechanic", 500);
            }
        }


        public async Task<Response<bool>> IsAvailible(string userId,AppointmentDto appointmentDto)
        {
            try
            {
                var appointments = await appointmentsRepository.Collection
                    .Find(x => x.UserId == userId && x.AppointmentItems.Any(t => t.MechanicId == appointmentDto.AppointmentItem.MechanicId))
                    .FirstOrDefaultAsync();

                return Response<bool>.Success(200, appointments != null);
            }
            catch
            {
                return Response<bool>.Fail("Error while retrieving favorite mechanic", 500);
            }
        }

        public async Task<Response<List<AppointmentsDto>>> GetAll(string userId)
        {
            try
            {
                var appointmentsList = await appointmentsRepository.Collection
                    .Find(x => x.UserId == userId)
                    .ToListAsync();

                return Response<List<AppointmentsDto>>.Success(200, appointmentsRepository.Mapper.Map<List<AppointmentsDto>>(appointmentsList));
            }
            catch
            {
                return Response<List<AppointmentsDto>>.Fail("Error while retrieving favorite mechanics", 500);
            }
        }

    }
}
