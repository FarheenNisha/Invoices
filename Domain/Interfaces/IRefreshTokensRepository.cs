using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IRefreshTokensRepository : IRepository<RefreshToken>
    {
        Task<RefreshToken> GetRefreshTokenByUserId(string UserId);
        Task<RefreshToken> GetRefreshTokenById(string Id);
    }
}
