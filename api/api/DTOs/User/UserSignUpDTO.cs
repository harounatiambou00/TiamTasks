using System.ComponentModel.DataAnnotations;

namespace api.DTOs.User
{
    public class UserSignUpDTO
    {

        [Required]
        [MaxLength(50, ErrorMessage = " The firstname must be less than 50 characters")]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(50, ErrorMessage = " The lastname must be less than 50 characters")]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [MaxLength(100, ErrorMessage = " The email must be less than 100 characters")]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MinLength(8, ErrorMessage = " The password must be at least 8 characters")]
        public string Password { get; set; } = string.Empty;
    }
}
