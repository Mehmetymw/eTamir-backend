namespace eTamir.Services.Map.Settings 
{
     public interface IDatabaseSettings

    {
        public string AddressCollectionName {get;set;}
        public string LocationCollectionName {get;set;}
        public string DatabaseName {get;set;}
        public string ConnectionString { get; set; }
    
    }

}