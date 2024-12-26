using Application.Dtos;
using Application.ViewModels;
using AutoMapper;
using Domain.Models;

namespace Application.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //Bills
            CreateMap<BillDto, Bills>();
            CreateMap<Bills, BillVM>();
            CreateMap<Bills, BillDto>();

            //Products
            CreateMap<ProductDto, Products>();
            CreateMap<Products, ProductVM>();
            CreateMap<Products, ProductDto>();
        }
    }
}
