using Application.Dtos;
using Application.ViewModels;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ServiceInterfaces
{
    public interface IProductsService : IService<Products, ProductDto, ProductVM>
    {
        Task<ProductVM> GetDuplicate(ProductDto dto);
    }
}
