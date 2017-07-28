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
using ValueObjects.Finance;

namespace ApplicationServices.QueryHandlers
{
    public class GetFinancialAccountBalanceQueryHandler : IQueryHandler<GetFinancialAccountBalanceQuery, Money>
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public GetFinancialAccountBalanceQueryHandler(IUnitOfWork unitOfWork, MapperConfiguration config)
        {
            _unitOfWork = unitOfWork;
            _mapper = config.CreateMapper();
        }

        public Money Handle(GetFinancialAccountBalanceQuery query)
        {
            
            var account = _unitOfWork.FinancialAccounts.GetWithTransactions(query.AccountId);
            var balance = account.CalculateBalance();
            //var accountDto = _mapper.Map<FinancialAccount, FinancialAccountDto>(account);
            return balance;

        }
    }
}
