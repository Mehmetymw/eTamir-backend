using eTamir.Services.Appointment.Repository;
using eTamir.Services.Catolog.Models;
using eTamir.Shared.Dtos;
using eTamir.Services.Appointment.Settings;
using Microsoft.Extensions.Options;
using AutoMapper;
using MongoDB.Driver;
using System.ComponentModel.Design;

namespace eTamir.Services.Appointment.Services
{
    public class WorkingHoursService : IWorkingHoursService
    {
        private readonly IWorkingHoursRepository<WorkingHours> workinghoursRepo;
        private readonly IOptions<IDatabaseSettings> databaseSettings;

        public WorkingHoursService(IWorkingHoursRepository<WorkingHours> workinghoursRepo, IOptions<IDatabaseSettings> databaseSettings)
        {
            this.databaseSettings = databaseSettings;
            this.workinghoursRepo = workinghoursRepo;
        }

        public async Task<Response<WorkingHours>> AddAsync(WorkingHoursDto workingHoursDto)
        {
            try
            {
                var workingHours = workinghoursRepo.Mapper.Map<WorkingHours>(workingHoursDto);
                await workinghoursRepo.Collection.InsertOneAsync(workingHours);
                return Response<WorkingHours>.Success(200, workingHours);
            }
            catch (Exception ex)
            {
                return Response<WorkingHours>.Fail("Error while adding working hours", 400);
            }
        }

        public async Task<Response<WorkingHours>> GetByIdAsync(string id)
        {
            try
            {
                var workingHours = await workinghoursRepo.Collection.Find(x => x.Id == id).FirstOrDefaultAsync();
                if (workingHours == null)
                {
                    return Response<WorkingHours>.Fail("Working hours not found", 404);
                }
                return Response<WorkingHours>.Success(200, workingHours);
            }
            catch (Exception ex)
            {
                return Response<WorkingHours>.Fail("Error while getting working hours", 400);
            }
        }
    }
}