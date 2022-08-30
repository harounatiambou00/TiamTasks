using api.DTOs.User;
using api.Models;

namespace api.Services.UserService
{
    public interface IUserService
    {
        Task<ServiceResponse<List<GetUserDTO>>> GetAllUsers();
        Task<ServiceResponse<GetUserDTO?>> GetUserById(int id);
        Task<ServiceResponse<User?>> GetUserByEmail(string email);
        Task<ServiceResponse<string?>> SignUp(UserSignUpDTO request);
        Task<ServiceResponse<GetUserDTO>> UpdateUser(UserUpdateDTO request);
        Task<ServiceResponse<string?>> DeleteUser(int id);
        Task<ServiceResponse<String?>> Login(UserLoginDTO request);
        Task<ServiceResponse<string?>> VerifyEmail(string token);
        Task<ServiceResponse<string?>> ForgotPassword(string email);
        Task<ServiceResponse<string?>> ResetPassword(ResetPasswordDTO request);
    }
}
