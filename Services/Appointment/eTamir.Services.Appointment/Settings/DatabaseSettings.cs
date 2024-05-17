namespace eTamir.Services.Appointment.Settings
{
    internal class DatabaseSettings : IDatabaseSettings
    {
        public string DatabaseName { get; set; }
        public string ConnectionString { get; set; }
        public string WorkingHoursCollectionName { get; set; }
        public string AppointmentsCollectionName { get; set; }
    }

}