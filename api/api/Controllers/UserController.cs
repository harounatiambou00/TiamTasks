using api.DTOs.User;
using api.Models;
using api.Services.JwtService;
using api.Services.UserService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IJwtService _jwtService;
        private readonly IUserService _userService; 
        public UserController(IJwtService jwtService, IUserService userService)
        {
            _jwtService = jwtService;
            _userService = userService;
        }

        [HttpPost("sign-up")]
        public async Task<ActionResult<ServiceResponse<string?>>> Register(UserSignUpDTO request)
        {
            var serviceResponse = await _userService.SignUp(request);

            return Ok(serviceResponse);
        }

        [HttpPost("verify-email")]
        public async Task<ActionResult<ServiceResponse<string?>>> VerifyEmail(string token)
        {
            var serviceResponse = await _userService.VerifyEmail(token);

            return Ok(serviceResponse);
        }

        [HttpPost("sign-in")]
        public async Task<ServiceResponse<String?>> Login(UserLoginDTO request)
        {
            //We will try to log the user
            var login = await _userService.Login(request);

            //if the user is logged succesfully, we create a cookie that contains the token of the login
            if (login.Success)
            {
                Response.Cookies.Append("userLoginJWT", login.Data, new CookieOptions
                {
                    HttpOnly = true //This means that the frontend can only get it but cannot access/modify it. 
                });
            }

            /*
             * Finally we will return a service response to inform if the login was successful or not(if not why)
             * We return a Service Response with a null data because we don't want the frontend to access the token.
            */
            return new ServiceResponse<string?>
            {
                Data = null,
                Success = login.Success,
                Message = login.Message,
            };
        }

        [HttpPost("forgot-password")]
        public async Task<ActionResult<ServiceResponse<string?>>> ForgotPassword(string email)
        {
            var serviceResponse = await _userService.ForgotPassword(email);

            return Ok(serviceResponse);
        }

        [HttpPost("reset-password")]
        public async Task<ActionResult<ServiceResponse<string?>>> ResetPassword(ResetPasswordDTO request)
        {
            var serviceResponse = await _userService.ResetPassword(request);

            return Ok(serviceResponse);
        }

        [HttpGet("get-user")]
        public async Task<ActionResult<ServiceResponse<GetUserDTO>>> Get()
        {
            try
            {
                var userLoginJwtFromCookies = Request.Cookies["userLoginJWT"];

                var validatedUserLoginJwt = _jwtService.Verify(userLoginJwtFromCookies);

                int userId = int.Parse(validatedUserLoginJwt.Issuer);

                var serviceResponse = await _userService.GetUserById(userId);

                if (serviceResponse.Data == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "INVALID_TOKEN";
                }

                return serviceResponse;
            }
            catch (Exception _)
            {
                return Unauthorized();
            }
        }

        [HttpPost("logout")]
        public ActionResult<ServiceResponse<User>> Logout()
        {
            if (Request.Cookies["userLoginJWT"] != null)
            {
                Response.Cookies.Delete("userLoginJWT");
                return (new ServiceResponse<User>
                {
                    Data = null,
                    Success = true,
                    Message = "LOGGED_OUT_SUCCESSFULLY"
                });
            }
            else
            {
                return (new ServiceResponse<User>
                {
                    Data = null,
                    Success = false,
                    Message = "LOG_OUT_FAILED"
                });
            }
        }

        [HttpDelete("delete")]
        public async Task<ActionResult<ServiceResponse<string?>>> DeleteUser(int id)
        {
            var serviceResponse = await _userService.DeleteUser(id);
            return serviceResponse;
        }

        [HttpPut("update")]
        public async Task<ActionResult<ServiceResponse<GetUserDTO>>> UpdateUser(UserUpdateDTO request)
        {
            var serviceResponse = await _userService.UpdateUser(request);
            return serviceResponse;
        }
    }
}
