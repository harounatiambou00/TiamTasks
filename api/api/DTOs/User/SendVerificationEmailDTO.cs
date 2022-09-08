using System.ComponentModel.DataAnnotations;

namespace api.DTOs.User
{
    public class SendVerificationEmailDTO
    {
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Token { get; set; } = string.Empty;
    }
}
