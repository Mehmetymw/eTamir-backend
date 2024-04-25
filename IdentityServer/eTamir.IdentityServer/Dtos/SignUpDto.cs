using System;
using System.ComponentModel.DataAnnotations;

namespace eTamir.IdentityServer.Dtos
{
    public class SignUpDto
    {
        private string name;
        private string surname;
        private string phoneNumber;
        private string countryCode;

        // [Required]
        public string Name
        {
            get { return name; }
            set { name = value?.Length > 0 ? char.ToUpper(value[0]) + value.Substring(1).ToLower() : value; }
        }

        // [Required]
        public string Surname
        {
            get { return surname; }
            set { surname = value?.ToUpper(); }
        }

        [Required]
        public string Password { get; set; }

        // [Required(ErrorMessage = "Phone number is required.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Phone number must contain only numbers.")]
        [MaxLength(10, ErrorMessage = "Phone number cannot be more than 10 digits.")]

        public string PhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = value; }
        }

        // [Required(ErrorMessage = "Country code is required.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Country code must contain only numbers.")]
        [MaxLength(4, ErrorMessage = "Phone number cannot be more than 3 digits.")]
        public string CountryCode
        {
            get { return countryCode; }
            set { countryCode = value; }
        }

        [Required]
        public string PasswordConfirmation { get; set; }

        [Required]
        public string Email { get; set; }
    }
}
