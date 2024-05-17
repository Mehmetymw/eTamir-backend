namespace eTamir.Services.Appointment.Settings 
{
     public interface IDatabaseSettings
    {
        public string WorkingHoursCollectionName {get;set;}
        public string AppointmentsCollectionName {get;set;}
        public string DatabaseName {get;set;}
        public string ConnectionString { get; set; }
    
    }

}