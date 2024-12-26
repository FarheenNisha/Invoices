using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class RefreshTokensRepository : Repository<RefreshToken>, IRefreshTokensRepository
    {
        public RefreshTokensRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

		public Task<RefreshToken> GetRefreshTokenById(string Id)
		{
			return _appDbContext.RefreshTokens
				.Where(x => x.Id.ToLower() == Id.ToLower())
				.FirstOrDefaultAsync();
		}

		public Task<RefreshToken> GetRefreshTokenByUserId(string UserId)
        {
            return _appDbContext.RefreshTokens
                .Where(x => x.UserId.ToLower() == UserId.ToLower())
                .OrderByDescending(x => x.IssuedUtc)
                .FirstOrDefaultAsync();
        }
    }
}
