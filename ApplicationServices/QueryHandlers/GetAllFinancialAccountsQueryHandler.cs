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
    public class GetAllFinancialAccountsQueryHandler : IQueryHandler<GetAllFinancialAccountsQuery, FinancialAccountDto[]>
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public GetAllFinancialAccountsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public FinancialAccountDto[] Handle(GetAllFinancialAccountsQuery query)
        {
            var accounts = _unitOfWork.FinancialAccounts.GetAll();
            var accountsDtos = _mapper.Map<IEnumerable<FinancialAccount>, IEnumerable<FinancialAccountDto>>(accounts);
            return accountsDtos.ToArray();

        }
    }
}
