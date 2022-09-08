using api.DTOs.User;
using api.Models;
using api.Services.EmailService;
using api.Services.JwtService;
using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;
using System;
using System.Security.Cryptography;

namespace api.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly DataContext _db;
        private readonly IJwtService _jwtService;
        private readonly IEmailService _emailService;

        public UserService(DataContext db, IJwtService jwtService, IEmailService emailService)
        {
            _db = db;
            _jwtService = jwtService;
            _emailService = emailService;
        }

        public async Task<ServiceResponse<string?>> DeleteUser(int id)
        {
            var user = await _db.Users.FindAsync(id);
            if (user == null)
            {
                return new ServiceResponse<string?>
                {
                    Data = null,
                    Success = false,
                    Message = "USER_NOT_FOUND"
                };
            }
            else
            {
                _db.Users.Remove(user);
                await _db.SaveChangesAsync();

                return new ServiceResponse<string?>
                {
                    Data = null,
                    Success = true,
                    Message = "USER_DELETED_SUCCESSFULLY"
                };
            }
        }

        public async Task<ServiceResponse<List<GetUserDTO>>> GetAllUsers()
        {
            var users = await _db.Users.ToListAsync();
            var serviceResponse = new ServiceResponse<List<GetUserDTO>>();
            serviceResponse.Data = new List<GetUserDTO>();
            foreach (var user in users)
                serviceResponse.Data.Add(new GetUserDTO
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                });

            return serviceResponse;
        }

        public async Task<ServiceResponse<User?>> GetUserByEmail(string email)
        {
            var serviceResponse = new ServiceResponse<User>();
            serviceResponse.Data = await _db.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (serviceResponse.Data == default(User))
            {
                serviceResponse.Data = null;
                serviceResponse.Success = false;
                serviceResponse.Message = "USER_NOT_FOUND";
            }
            serviceResponse.Success = true;
            serviceResponse.Message = "USER_SUCCESSFULLY_FOUND";
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetUserDTO?>> GetUserById(int id)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == id);
            var serviceResponse = new ServiceResponse<GetUserDTO>();
            serviceResponse.Data = new GetUserDTO();

            if (user != default(User))
            {
                serviceResponse.Data = new GetUserDTO
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                };
                serviceResponse.Success = false;
                serviceResponse.Message = "USER_NOT_FOUND";
            }
            serviceResponse.Success = true;
            serviceResponse.Message = "USER_SUCCESSFULLY_FOUND";
            return serviceResponse;
        }

        public async Task<ServiceResponse<string?>> Login(UserLoginDTO request)
        {
            var serviceResponse = await GetUserByEmail(request.Email);
            if (serviceResponse.Success)
            {
                if(serviceResponse.Data.VerifiedAt == null)
                {
                    return new ServiceResponse<string?>
                    {
                        Data = null,
                        Success = false,
                        Message = "USER_NOT_VERIFIED"
                    };
                }
                else
                {
                    //If the cryped version of the request's password is noit equal to the one related the email we send a bad request
                    if (!BCrypt.Net.BCrypt.Verify(request.Password, serviceResponse.Data.PasswordHash))
                    {
                        return new ServiceResponse<string?>
                        {
                            Data = null,
                            Success = false,
                            Message = "INCORRECT_PASSWORD"
                        };
                    }

                    string clientLoginJWT = _jwtService.GenerateToken(serviceResponse.Data.Id, request.RemenberMe);

                    return new ServiceResponse<string?>
                    {
                        Data = clientLoginJWT,
                        Success = true,
                        Message = "USER_LOGGED_IN_SUCCESSFULLY"
                    };
                }
            }
            else
            {
                return new ServiceResponse<string?>
                {
                    Data = null,
                    Success = false,
                    Message = "USER_NOT_FOUND"
                };
            }
        }

        public async Task<ServiceResponse<string?>> SignUp(UserSignUpDTO request)
        {
            ServiceResponse<string?> serviceResponse;

            //We check if the emil address is not used yet by another client 
            var userWithTheSameEmail = _db.Users.FirstOrDefault(u => u.Email.ToLower() == request.Email.ToLower());
            if (userWithTheSameEmail == null) //Which means that there is no client with the same email
            {
                var verificationToken = Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
                while(_db.Users.FirstOrDefault(u => u.VerificationToken == verificationToken) != default(User))
                    verificationToken = Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
                await _db.Users.AddAsync(
                  new User
                  {
                        FirstName = request.FirstName,
                        LastName = request.LastName,
                        Email = request.Email,
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                        PasswordSalt = BCrypt.Net.BCrypt.GenerateSalt(12),
                        //Here we are generating verification token from a random array of 64 bytes.
                        VerificationToken = verificationToken
                  }
                 );
                await _db.SaveChangesAsync();

                serviceResponse = new ServiceResponse<string?>
                {
                    Data = verificationToken,
                    Success = true,
                    Message = " "
                };
            }
            else
            {
                serviceResponse = new ServiceResponse<string?>
                {
                    Data = null,
                    Success = false,
                    Message = "EMAIL_ALREADY_EXISTS"
                };
            }


            return serviceResponse;
        }

        public async Task<ServiceResponse<GetUserDTO?>> UpdateUser(UserUpdateDTO request)
        {
            var user = await _db.Users.FindAsync(request.Id);
            if (user == null)
            { 
                return new ServiceResponse<GetUserDTO?>
                {
                    Data = null,
                    Success = false,
                    Message = "USER_NOT_FOUND"
                };
            }
            else
            {
                user.FirstName = request.FirstName;
                user.LastName = request.LastName;
                user.Email = request.Email;
                await _db.SaveChangesAsync();

                var updatedUser = new GetUserDTO
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email
                };
                return new ServiceResponse<GetUserDTO?>
                {
                    Data = updatedUser,
                    Success = true,
                    Message = "USER_UPDATED_SUCCESSFULLY"
                };
            }
        }

        public async Task<ServiceResponse<string?>> VerifyEmail(string token)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.VerificationToken == token);
            if (user != default(User))
            {
                user.VerifiedAt = DateTime.Now;
                user.VerificationToken = null;
                await _db.SaveChangesAsync();
                return new ServiceResponse<string?>
                {
                    Data = null,
                    Success = false,
                    Message = "USER_VERIFIED_SUCCESSFULLY"
                };
            }
            else
            {
                return new ServiceResponse<string?>
                {
                    Data = null,
                    Success = false,
                    Message = "INVALID_TOKEN"
                };
            }
        }

        public async Task<ServiceResponse<string?>> ForgotPassword(string email)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user != default(User))
            {
                user.PasswordResetToken = Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
                while(await _db.Users.FirstOrDefaultAsync(u => u.PasswordResetToken == user.PasswordResetToken) != default(User))
                    user.PasswordResetToken = Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
                user.PasswordResetTokenExpiresAt = DateTime.Now.AddDays(1);
                await _db.SaveChangesAsync();
                return new ServiceResponse<string?>
                {
                    Data = null,
                    Success = false,
                    Message = "YOU_CAN_NOW_RESET_THE_PASSWORD"
                };
            }
            else
            {
                return new ServiceResponse<string?>
                {
                    Data = null,
                    Success = false,
                    Message = "USER_NOT_FOUND"
                };
            }
        }

        public async Task<ServiceResponse<string?>> ResetPassword(ResetPasswordDTO request)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.PasswordResetToken == request.token);
            if (user != default(User)  && user.PasswordResetTokenExpiresAt >= DateTime.Now)
            {
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
                user.PasswordResetToken = null;
                user.PasswordResetTokenExpiresAt = null;
                await _db.SaveChangesAsync();
                return new ServiceResponse<string?>
                {
                    Data = null,
                    Success = false,
                    Message = "PASSWORD_UPDATED_SUCCESSFULLY"
                };
            }
            else
            {
                return new ServiceResponse<string?>
                {
                    Data = null,
                    Success = false,
                    Message = "INVALID_TOKEN"
                };
            }
        }

        public ServiceResponse<string?> SendVerificationEmail(SendVerificationEmailDTO request)
        {
            string email_body = "Please confirm your email address <a href=\"#URL#\"> Click here </a>";
            string url = "http://localhost:3000/confirm-email/" + request.Token;

            string body = email_body.Replace("#URL#", System.Text.Encodings.Web.HtmlEncoder.Default.Encode(url));

            var response = _emailService.SendEmail(request.Email, "Email Verification", body);

            return response;
        }
    }
}
