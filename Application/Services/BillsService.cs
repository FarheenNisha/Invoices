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
    public class BillsService : Service<Bills, BillDto, BillVM>, IBillsService
    {
        public BillsService(
            IUnitOfWork unitOfWork, 
            IMapper mapper, 
            IRepository<Bills> repository) 
            : base(unitOfWork, mapper, repository)
        {
        }

        public async Task<BillVM> GetDuplicate(BillDto dto)
        {
            if (dto == null) return null;
            var model = _mapper.Map<Bills>(dto);
            var matchingModel = await _unitOfWork.BillsRepository.GetDuplicate(model);
            if (matchingModel == null) return null;
            var vm = _mapper.Map<BillVM>(matchingModel);
            return vm;
        }
    }
}
