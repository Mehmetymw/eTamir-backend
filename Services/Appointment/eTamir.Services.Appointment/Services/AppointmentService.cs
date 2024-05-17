using eTamir.Services.Catolog.Repository;
using eTamir.Services.Appointment.Dtos;
using eTamir.Services.Appointment.Models;
using eTamir.Services.Appointment.Repository;
using eTamir.Services.Appointment.Settings;
using eTamir.Shared.Dtos;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace eTamir.Services.Appointment.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository<Models.Appointment> appointmentsRepository;
        private readonly IOptions<IDatabaseSettings> databaseSettings;

        public AppointmentService(IAppointmentRepository<Models.Appointment> appointmentRepository, IOptions<IDatabaseSettings> databaseSettings)
        {
            this.appointmentsRepository = appointmentRepository;
            this.databaseSettings = databaseSettings;
        }

        public async Task<Response<NoContent>> Send(string userId, AppointmentDto appointmentDto)
        {
            try
            {
                var today = DateTime.UtcNow.Date;
                var tenDaysFromNow = today.AddDays(10);

                if (appointmentDto.DateTime < today || appointmentDto.DateTime > tenDaysFromNow)
                {
                    return Response<NoContent>.Fail("Appointment date must be between today and the next 10 days", 400);
                }

                var existingAppointment = await appointmentsRepository.Collection
                    .Find(x => x.UserId == userId && x.MechanicId == appointmentDto.MechanicId)
                    .FirstOrDefaultAsync();

                if (existingAppointment != null)
                {
                    return Response<NoContent>.Fail("This mechanic appointment already exists", 400);
                }

                var newAppointment = new Models.Appointment
                {
                    UserId = userId,
                    MechanicId = appointmentDto.MechanicId,
                    DateTime = appointmentDto.DateTime,
                    AppointmentStatus = AppointmentStatus.Pending,
                    DayOfWeek = appointmentDto.DateTime.DayOfWeek
                };

                await appointmentsRepository.Collection.InsertOneAsync(newAppointment);
                return Response<NoContent>.Success(200);
            }
            catch (Exception ex)
            {
                return Response<NoContent>.Fail("Error while adding appointment", 400);
            }
        }

        public async Task<Response<NoContent>> Delete(string userId, string Id)
        {
            try
            {
                var appointment = await appointmentsRepository.Collection
                    .Find(x => x.Id == Id && x.UserId == userId)
                    .FirstOrDefaultAsync();

                if (appointment == null)
                {
                    return Response<NoContent>.Fail("Appointment not found", 404);
                }

                await appointmentsRepository.Collection.DeleteOneAsync(x => x.UserId == userId && x.Id == Id);
                return Response<NoContent>.Success(200);
            }
            catch
            {
                return Response<NoContent>.Fail("Error while deleting appointment", 500);
            }
        }

        public async Task<Response<List<Models.Appointment>>> GetAll(string userId)
        {
            try
            {
                var appointments = await appointmentsRepository.Collection
                    .Find(x => x.UserId == userId)
                    .ToListAsync();

                return Response<List<Models.Appointment>>.Success(200, appointments.OrderBy(x => x.DateTime).ToList());
            }
            catch (Exception ex)
            {
                return Response<List<Models.Appointment>>.Fail("Error while retrieving appointments", 500);
            }
        }

        public async Task<Response<List<Models.Appointment>>> GetAllByMechanic(string mechanicId)
        {
            try
            {
                var appointments = await appointmentsRepository.Collection
                    .Find(x => x.MechanicId == mechanicId)
                    .ToListAsync();


                return Response<List<Models.Appointment>>.Success(200, appointments);
            }
            catch
            {
                return Response<List<Models.Appointment>>.Fail("Error while retrieving appointments", 500);
            }

        }

        public async Task<Response<NoContent>> Update(AppointmentUpdateDto appointmentUpdateDto)
        {
            try
            {
                var appointment = await appointmentsRepository.Collection
                    .Find(x => x.Id == appointmentUpdateDto.Id)
                    .FirstOrDefaultAsync();

                if (appointment == null)
                {
                    return Response<NoContent>.Fail("Appointment not found", 404);
                }

                appointment.AppointmentStatus = appointmentUpdateDto.AppointmentStatus;
                await appointmentsRepository.Collection.ReplaceOneAsync(x => x.Id == appointmentUpdateDto.Id, appointment);
                return Response<NoContent>.Success(200);
            }
            catch
            {
                return Response<NoContent>.Fail("Error while updating appointment", 500);
            }
        }
    }
}
