using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistance.UnitOfWork;
using Domain.Entities;
using AutoMapper;
using FinanceManager.Contract.Queries;
using FinanceManager.Contract.Dto;


namespace ApplicationServices.QueryHandlers
{
    public class GetFinancialAccountByIdQueryHandler : IQueryHandler<GetFinancialAccountByIdQuery, FinancialAccountDto>
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public GetFinancialAccountByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public FinancialAccountDto Handle(GetFinancialAccountByIdQuery query)
        {
            var account = _unitOfWork.FinancialAccounts.Get(query.AccountId);
            var accountDto = _mapper.Map<FinancialAccount, FinancialAccountDto>(account);
            return accountDto;

        }
    }
}
