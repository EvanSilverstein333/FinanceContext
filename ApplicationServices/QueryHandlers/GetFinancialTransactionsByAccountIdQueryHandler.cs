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
    public class GetFinancialTransactionsByAccountIdQueryHandler : IQueryHandler<GetFinancialTransactionsByAccountIdQuery, FinancialTransactionDto[]>
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public GetFinancialTransactionsByAccountIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public FinancialTransactionDto[] Handle(GetFinancialTransactionsByAccountIdQuery query)
        {
            var transactions = _unitOfWork.FinancialTransactions.GetByAccountId(query.AccountId);
            var transactionDtos = _mapper.Map<FinancialTransaction[], FinancialTransactionDto[]>(transactions);
            return transactionDtos;

        }
    }
}
