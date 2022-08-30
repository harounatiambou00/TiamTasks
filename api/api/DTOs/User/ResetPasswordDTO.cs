using System.ComponentModel.DataAnnotations;

namespace api.DTOs.User
{
    public class ResetPasswordDTO
    {
        [Required]
        public string token { get; set; } = string.Empty;

        [Required]
        [MinLength(8, ErrorMessage = " The password must be at least 8 characters")]
        public string NewPassword { get; set; } = string.Empty;
    }
}
