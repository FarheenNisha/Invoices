using Domain.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _appDbContext;
        public IBillsRepository BillsRepository { get; }
        public IProductsRepository ProductsRepository { get; }
        public IRefreshTokensRepository RefreshTokensRepository { get; }
        public IInvoiceRepository InvoiceRepository { get; }

        public UnitOfWork(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            BillsRepository = new BillsRepository(_appDbContext);
            ProductsRepository = new ProductsRepository(_appDbContext);
            RefreshTokensRepository = new RefreshTokensRepository(_appDbContext);
            InvoiceRepository = new InvoiceRepository(_appDbContext);
        }

        public void Dispose()
        {
            _appDbContext.Dispose();
        }

        //public Task<int> SaveChanges()
        //{
        //    return _appDbContext.SaveChangesAsync();
        //}
        public async Task<int> SaveChanges(bool audit)
        {
            var rows = 0;
            try
            {
                rows = await _appDbContext.SaveChangesAsync();
            }
            catch (System.Exception ex)
            {
                var x = ex.Message;
                var y = ex.InnerException;
            }
            return rows;
        }
    }
}
