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
    public class ProductsRepository : Repository<Products>, IProductsRepository
    {
        public ProductsRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public Task<Products> GetDuplicate(Products model)
        {
            return _appDbContext.Products.FirstOrDefaultAsync(x => x.ProductName.ToLower() == model.ProductName.ToLower());
        }
    }
}
