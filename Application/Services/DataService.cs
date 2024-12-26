using Application.ServiceInterfaces;
using AutoMapper;
using Domain.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class DataService : IDataService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public IBillsService BillsService { get; }
        public IProductsService ProductsService { get; }

        public IInvoiceService InvoiceService { get; }

        //public IInvoiceItemService InvoiceItemService { get; }

        public DataService(
            IUnitOfWork unitOfWork, 
            IMapper mapper,
            IRepository<Bills> BillsRepo,
            IRepository<Products> ProductsRepo
            )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

            BillsService = new BillsService(_unitOfWork, _mapper, BillsRepo);
            ProductsService = new ProductsService(_unitOfWork, _mapper, ProductsRepo);
        }
    }
}
