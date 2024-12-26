using Application.Dtos;
using Application.ServiceInterfaces;
using Application.ViewModels;
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
    public class ProductsService : Service<Products, ProductDto, ProductVM>, IProductsService
    {
        public ProductsService(
            IUnitOfWork unitOfWork, 
            IMapper mapper, 
            IRepository<Products> repository) 
            : base(unitOfWork, mapper, repository)
        {
        }

        public async Task<ProductVM> GetDuplicate(ProductDto dto)
        {
            if (dto == null) return null;
            var model = _mapper.Map<Products>(dto);
            var matchingModel = await _unitOfWork.ProductsRepository.GetDuplicate(model);
            if (matchingModel == null) return null;
            var vm = _mapper.Map<ProductVM>(matchingModel);
            return vm;
        }
    }
}
