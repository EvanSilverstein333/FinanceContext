using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Configuration.Conventions;
using AutoMapper.Mappers;
using Domain.Entities;
using FinanceManager.Contract.Dto;
namespace Infrastructure.Configuration.Mappers
{
    public class FinancialAccountProfile: Profile
    {
        public FinancialAccountProfile()
        {
            CreateMap<FinancialAccount, FinancialAccountDto>();
            CreateMap<FinancialAccountDto, FinancialAccount>();
        }
    }
}
