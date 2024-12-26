using Application.Dtos;
using Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ServiceInterfaces
{
    public interface IAuthService
    {
        Task<JwtTokenVM> Register(RegisterDTO registerDTO);
        Task<JwtTokenVM> Login(LoginDTO loginDTO);
        Task<JwtTokenVM> RefreshJwtToken(JwtTokenVM jwtTokenVm);
        Task<bool> ChangePassword(string userName, string currentPassword, string newPassword);
    }
}
