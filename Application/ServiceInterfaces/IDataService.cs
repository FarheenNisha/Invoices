﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ServiceInterfaces
{
    public interface IDataService
    {
        IBillsService BillsService { get; }
        IProductsService ProductsService { get; }
        IInvoiceService InvoiceService { get; }
        //IInvoiceItemService InvoiceItemService { get; }
    }
}
