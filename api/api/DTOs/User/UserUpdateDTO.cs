using System.ComponentModel.DataAnnotations;

namespace api.DTOs.User
{
    public class UserUpdateDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "FirstName must be less than 50 characters.")]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(50, ErrorMessage ="LastName must be less than 50 characters.")]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [MaxLength(100, ErrorMessage = " The email must be less than 100 characters")]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
    }
}
