using api.Models;
using RestSharp;
using RestSharp.Authenticators;
using System.Text.RegularExpressions;

namespace api.Services.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public ServiceResponse<string?> SendEmail(string email, string subject, string body)
        {
            //create client
            RestClient client = new RestClient(_config.GetSection("EmailConfig:BASE_URL").Value);

            client.Authenticator =new HttpBasicAuthenticator("api",
                                       _config.GetSection("EmailConfig:API_KEY").Value);

            //create request
            var request = new RestRequest();

            request.AddParameter("domain", "sandbox4ec5d77af24448c9b5bfbead64ae5bd6.mailgun.org", ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", "Excited User <mailgun@YOUR_DOMAIN_NAME>");
            request.AddParameter("to", email);
            request.AddParameter("subject", subject);
            request.AddParameter("html", "<html>" + body + "</html>");
            request.Method = Method.Post;

            var response = client.Execute(request);

            if (response.IsSuccessful)
            {
                return new ServiceResponse<string?>
                {
                    Data = null,
                    Success = true,
                    Message = "EMAIL_SENT_SUCCESSFULLY"
                };
            }
            else
            {
                return new ServiceResponse<string?>
                {
                    Data = null,
                    Success = false,
                    Message = response.Content
                };
            }
        }
    }
}
