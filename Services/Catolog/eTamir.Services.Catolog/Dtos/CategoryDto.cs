using eTamir.Services.Catolog.Models;

namespace eTamir.Services.Catolog.Dtos
{
    public class CategoryDto
    {
        public string? Id { get; set; }
        public string Name { get; set; }
        public string CatalogId {get;set;}
    }
}
