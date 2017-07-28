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
    public class FindFinancialAccountsBySearchTextQueryHandler : IQueryHandler<FindFinancialAccountsBySearchTextQuery, FinancialAccountDto[]>
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public FindFinancialAccountsBySearchTextQueryHandler(IUnitOfWork unitOfWork, MapperConfiguration config)
        {
            _unitOfWork = unitOfWork;
            _mapper = config.CreateMapper();
        }

        public FinancialAccountDto[] Handle(FindFinancialAccountsBySearchTextQuery query)
        {
            var accounts = _unitOfWork.FinancialAccounts.Find((account) => (account.FirstName + " " + account.LastName).ToLower().Contains(query.SearchText));
            var accountDtos = _mapper.Map<IEnumerable<FinancialAccount>, IEnumerable<FinancialAccountDto>>(accounts);
            return accountDtos.ToArray();

        }
    }
}
