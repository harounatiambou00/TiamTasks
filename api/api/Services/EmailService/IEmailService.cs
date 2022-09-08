using api.Models;

namespace api.Services.EmailService
{
    public interface IEmailService
    {
        ServiceResponse<string?> SendEmail(string email, string subject, string body); 
    }
}
