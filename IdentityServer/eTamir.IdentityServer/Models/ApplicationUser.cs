using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace eTamir.IdentityServer.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        // [Required]
        public string Name{ get; set;}
        // [Required]
        public string Surname{ get; set;}

        // [Required]
        public string CountryCode { get; set; }

    }
}
