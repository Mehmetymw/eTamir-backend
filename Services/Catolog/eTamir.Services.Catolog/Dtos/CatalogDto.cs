using System.ComponentModel.DataAnnotations;

namespace eTamir.Services.Catolog.Dtos
{
    public class CatalogDto
    {
        public string? Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public CatalogType CatalogType { get; set; }

    }
}