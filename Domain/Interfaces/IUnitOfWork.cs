using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUnitOfWork :IDisposable
    {
        IBillsRepository BillsRepository { get; }
        IProductsRepository ProductsRepository { get; }
        IRefreshTokensRepository RefreshTokensRepository { get; }
        IInvoiceRepository InvoiceRepository { get; }

        Task<int> SaveChanges(bool audit = true);
    }
}
