using System.ComponentModel.DataAnnotations;

namespace eTamir.IdentityServer.Dtos
{
    public class SignUpDto
    {
       private string name;
        private string surname;

        [Required]
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value?.Length > 0 ? char.ToUpper(value[0]) + value[1..] : value;
            }
        }

        [Required]
        public string Surname
        {
            get
            {
                return surname;
            }
            set
            {
                surname = value?.ToUpper();
            }
        }

        [Required]
        public string Password { get; set; }
        
        [Required]
        public string PasswordConfirmation { get; set; }
        
        [Required]
        public string Email { get; set; }
    }

}