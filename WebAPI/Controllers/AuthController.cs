using Application.Dtos;
using Application.Extensions;
using Application.Helpers;
using Application.ServiceInterfaces;
using Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.Text;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        //Post api/Auth/Register
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterDTO modelDto)
        {
            if (modelDto == null)
                return BadRequest(ApiResponseBuilder.GenerateBadRequest("Registration failed", "Invalid Input"));
            if (!ModelState.IsValid)
            {
                var errors = ModelState.GetModelStateError();
                if(errors != null && errors.Count > 0)
                {
                    var msgBuilder = new StringBuilder();
                    foreach (var error in errors) 
                        msgBuilder.AppendLine(error.ToString());
                    return BadRequest(ApiResponseBuilder.GenerateBadRequest("Registration failed", msgBuilder.ToString()));
                }
            }

            var jwtTokenVm = await _authService.Register(modelDto);
            if (jwtTokenVm == null || string.IsNullOrEmpty(jwtTokenVm.JwtToken))
                return BadRequest(ApiResponseBuilder.GenerateBadRequest("Registration failed", $"{jwtTokenVm.Error}"));
            return Ok(ApiResponseBuilder.GenerateOK(jwtTokenVm, "OK", "User registered successfully"));
        }
        //Post api/Auth/Login
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDTO modelDto)
        {
            if (modelDto == null)
                return BadRequest(ApiResponseBuilder.GenerateBadRequest("Login failed", "Invalid Input"));
            if (!ModelState.IsValid)
            {
                var errors = ModelState.GetModelStateError();
                if(errors != null && errors.Count > 0)
                {
                    var msgBuilder = new StringBuilder();
                    foreach (var error in errors) 
                        msgBuilder.AppendLine(error.ToString());
                    return BadRequest(ApiResponseBuilder.GenerateBadRequest("Login failed", msgBuilder.ToString()));
                }
            }
            var jwtTokenVm = await _authService.Login(modelDto);
            if (jwtTokenVm == null || string.IsNullOrEmpty(jwtTokenVm.JwtToken))
                return BadRequest(ApiResponseBuilder.GenerateBadRequest("Login failed", $"{jwtTokenVm.Error}"));
            return Ok(ApiResponseBuilder.GenerateOK(jwtTokenVm, "OK", "User logged in successfully"));
        }

        //Post api/Auth/ChangePassword
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO modelDto)
        {
            if (modelDto == null)
                return BadRequest(ApiResponseBuilder.GenerateBadRequest("Change Password failed", "Invalid Input"));
            if (!ModelState.IsValid)
            {
                var errors = ModelState.GetModelStateError();
                if (errors != null && errors.Count > 0)
                {
                    var msgBuilder = new StringBuilder();
                    foreach (var error in errors)
                        msgBuilder.AppendLine(error.ToString());
                    return BadRequest(ApiResponseBuilder.GenerateBadRequest("Change Password failed", msgBuilder.ToString()));
                }
            }
            var passwordChanged = await _authService.ChangePassword(User.Identity.Name, modelDto.CuurentPassword, modelDto.NewPassword);
            if (passwordChanged)
                return Ok(ApiResponseBuilder.GenerateOK(null, "OK", "Password Changed in successfully"));
            return BadRequest(ApiResponseBuilder.GenerateBadRequest("Change Password failed", "Change Password failed"));
            
        }
        [HttpPost]
        public async Task<IActionResult> RefreshJwtToken([FromBody] JwtTokenVM tokenVm)
        {
            if (tokenVm == null || string.IsNullOrEmpty(tokenVm.JwtToken) || string.IsNullOrEmpty(tokenVm.RefreshToken))
                return BadRequest(ApiResponseBuilder.GenerateBadRequest("Refresh failed", "Invalid input"));

            var jwtTokenVm = await _authService.RefreshJwtToken(tokenVm);
            if(jwtTokenVm != null && string.IsNullOrEmpty(jwtTokenVm.JwtToken))
                return BadRequest(ApiResponseBuilder.GenerateBadRequest("Refresh failed", $"{jwtTokenVm.Error}"));

            return Ok(ApiResponseBuilder.GenerateOK(jwtTokenVm, "OK", "Token refreshed successfully"));
        }
    }
}
