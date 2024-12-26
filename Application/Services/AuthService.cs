using Application.AppSettings;
using Application.Dtos;
using Application.ServiceInterfaces;
using Application.ViewModels;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtSettings _jwtSettings;
        private readonly IUnitOfWork _unitOfWork;
        public AuthService(
            UserManager<ApplicationUser> userManager,
            IOptions<JwtSettings> jwtSettings,
            IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings.Value;
            _unitOfWork = unitOfWork;
        }
        public async Task<JwtTokenVM> Register(RegisterDTO registerDTO)
        {
            var jwtTokenVm = new JwtTokenVM();
            if (registerDTO == null)
            {
                jwtTokenVm.Error = "Invalid Input";
                return jwtTokenVm;
            }
            if (string.IsNullOrEmpty(registerDTO.UserName))
            {
                jwtTokenVm.Error = "UserName is required";
                return jwtTokenVm;
            }
            if (string.IsNullOrEmpty(registerDTO.Password))
            {
                jwtTokenVm.Error = "PassWord is required";
                return jwtTokenVm;
            }
            var existingUser = await _userManager.FindByNameAsync(registerDTO.UserName);
            if (existingUser != null)
            {
                jwtTokenVm.Error = "User already exists";
                return jwtTokenVm;
            }
            var newUser = new ApplicationUser
            {
                UserName = registerDTO.UserName,
                Name = registerDTO.Name,
                Email = registerDTO.Email,
                EmailConfirmed = true,
                PhoneNumber = registerDTO.PhoneNumber,
                PhoneNumberConfirmed = true,
                ProfileImage = string.IsNullOrEmpty(registerDTO.ProfileImage) ? "default-image-jpg" : registerDTO.ProfileImage,
            };
            var identityResult = await _userManager.CreateAsync(newUser, registerDTO.Password);
            if (identityResult.Errors.Any())
            {
                jwtTokenVm.Error = string.Join(", ", identityResult.Errors);
                return jwtTokenVm;
            }
            if (!identityResult.Succeeded)
            {
                jwtTokenVm.Error = "User regidtration failed";
                return jwtTokenVm;
            }
            //Generate Claims Identity
            var ClaimsIdentity = GenerateClaimsIdentity(newUser);
            //Generate Jwt Token
            var jwtToken = GenerateAccessToken(ClaimsIdentity);
            //Generate Refrese Token
            var refreshToken = await GenerateRefreshToken(newUser.Id);
            //Set these into jwtTokenVm
            jwtTokenVm.JwtToken = jwtToken;
            jwtTokenVm.RefreshToken = refreshToken;
            jwtTokenVm.Error = string.Empty;

            return jwtTokenVm;
        }
        public async Task<bool> ChangePassword(string userName, string currentPassword, string newPassword )
        {
            var user = await _userManager.FindByNameAsync(userName);
            var changePasswordResult = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
            return changePasswordResult.Succeeded;
        }
        private static ClaimsIdentity GenerateClaimsIdentity(ApplicationUser newUser)
        {
            var secondsFromUnixEpoch = (DateTime.UtcNow - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds;
            var unixEpochDateStr = ((long)Math.Round(secondsFromUnixEpoch)).ToString();
            var claims = new List<Claim>
            {
                new Claim("usrid", newUser.Id),
                new Claim("usrnm", newUser.UserName),
                new Claim("email", newUser.Email),
                new Claim("phone", newUser.PhoneNumber),
                new Claim("image", newUser.ProfileImage),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, unixEpochDateStr, ClaimValueTypes.Integer64)
            };
            if (!string.IsNullOrEmpty(newUser.Name))
                claims.Add(new Claim("cname", newUser.Name));
            return new ClaimsIdentity(new GenericIdentity(newUser.UserName, "Token"), claims);
        }

        private string GenerateAccessToken(ClaimsIdentity claimsIdentity)
        {
            var jwtToken = new JwtSecurityToken(
                _jwtSettings.Issuer,
                _jwtSettings.Audience,
                claimsIdentity.Claims,
                DateTime.UtcNow,
                DateTime.UtcNow.Add(TimeSpan.FromMinutes(_jwtSettings.AccessTokenExpiryMinutes)),
                new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SigningKey)),
                SecurityAlgorithms.HmacSha512));
            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }

        private async Task<string> GenerateRefreshToken(string userId)
        {
            var existingRefreshToken = await _unitOfWork.RefreshTokensRepository.GetRefreshTokenByUserId(userId);
            if (existingRefreshToken != null)
                _unitOfWork.RefreshTokensRepository.Delete(existingRefreshToken);
            var newRefreshToken = new RefreshToken
            {
                Id = Guid.NewGuid().ToString(),
                UserId = userId,
                IssuedUtc = DateTime.UtcNow,
                ExpiresUtc = DateTime.UtcNow.AddMinutes(Convert.ToInt32(_jwtSettings.RefreshTokenExpiryMinutes)),
            };
            _unitOfWork.RefreshTokensRepository.Create(newRefreshToken);
            await _unitOfWork.SaveChanges();
            return newRefreshToken.Id;
        }
        public async Task<JwtTokenVM> Login(LoginDTO loginDTO)
        {
            var jwtTokenVm = new JwtTokenVM();
            if (loginDTO == null)
            {
                jwtTokenVm.Error = "Invalid Input";
                return jwtTokenVm;
            }
            if (string.IsNullOrEmpty(loginDTO.UserName))
            {
                jwtTokenVm.Error = "UserName is required";
                return jwtTokenVm;
            }
            if (string.IsNullOrEmpty(loginDTO.Password))
            {
                jwtTokenVm.Error = "PassWord is required";
                return jwtTokenVm;
            }
            var existingUser = await _userManager.FindByNameAsync(loginDTO.UserName);
            if (existingUser == null)
            
            {
                jwtTokenVm.Error = "User not found";
                return jwtTokenVm;
            }
            var lockedOut = await _userManager.IsLockedOutAsync(existingUser);
            if (lockedOut)
            {
                jwtTokenVm.Error = "Account locked out";
                return jwtTokenVm;
            }
            var passwordValid = await _userManager.CheckPasswordAsync(existingUser, loginDTO.Password);
            if(!passwordValid)
            {
                await _userManager.AccessFailedAsync(existingUser);
                jwtTokenVm.Error = "Username or password is invalid";
                return jwtTokenVm;
            }

            //Generate Claims Identity
            var ClaimsIdentity = GenerateClaimsIdentity(existingUser);
            //Generate Jwt Token
            var jwtToken = GenerateAccessToken(ClaimsIdentity);
            //Generate Refrese Token
            var refreshToken = await GenerateRefreshToken(existingUser.Id);
            //Set these into jwtTokenVm
            jwtTokenVm.JwtToken = jwtToken;
            jwtTokenVm.RefreshToken = refreshToken;
            jwtTokenVm.Error = string.Empty;

            return jwtTokenVm;
        }

        public async Task<JwtTokenVM> RefreshJwtToken(JwtTokenVM jwtTokenVm)
        {
            if (jwtTokenVm == null || string.IsNullOrEmpty(jwtTokenVm.RefreshToken))
                return new JwtTokenVM { Error = "Invalid input" };
            
            //var principal = GetPrincipalFromJwtToken(jwtTokenVm.JwtToken);
            //var userName = principal.Identity?.Name;

            //if (string.IsNullOrEmpty(userName))
            //    return new JwtTokenVM { Error = "Username is required" };

            var refreshTokenDb = await _unitOfWork.RefreshTokensRepository.GetRefreshTokenById(jwtTokenVm.RefreshToken);
            if (refreshTokenDb == null || DateTime.UtcNow > refreshTokenDb.ExpiresUtc)
                return new JwtTokenVM { Error = "Invalid input" };

            var existingUser = await _userManager.FindByIdAsync(refreshTokenDb.UserId);
            if (existingUser == null)
                return new JwtTokenVM { Error = "User not found" };
            var lockedOut = await _userManager.IsLockedOutAsync(existingUser);
            if (lockedOut) 
                return new JwtTokenVM { Error = "Account locked out" };
            

            //Generate Claims Identity
            var ClaimsIdentity = GenerateClaimsIdentity(existingUser);
            //Generate Jwt Token
            var jwtToken = GenerateAccessToken(ClaimsIdentity);
            
            //Set these into jwtTokenVm
            jwtTokenVm.JwtToken = jwtToken;
            jwtTokenVm.Error = string.Empty;

            return jwtTokenVm;

        }

        private ClaimsPrincipal GetPrincipalFromJwtToken(string jwtToken)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ClockSkew = TimeSpan.Zero,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SigningKey)),
                ValidateIssuerSigningKey = true,
                ValidIssuer = _jwtSettings.Issuer,
                ValidAudience = _jwtSettings.Audience,
                ValidateLifetime = false //important
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(jwtToken,tokenValidationParameters, out SecurityToken securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if(jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha512, StringComparison.OrdinalIgnoreCase))
                throw new SecurityTokenExpiredException();
            return principal;
        }
    }
}
