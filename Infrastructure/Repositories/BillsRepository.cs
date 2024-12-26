using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class BillsRepository : Repository<Bills>, IBillsRepository
    {
        public BillsRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public Task<Bills> GetDuplicate(Bills model)
        {
            return _appDbContext.Bills.FirstOrDefaultAsync(x => x.Id == model.Id);
        }
    }
}
