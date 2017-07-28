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
    public class FindFinancialTransactionsByDateQueryHandler : IQueryHandler<FindFinancialTransactionsByDateQuery, FinancialTransactionDto[]>
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public FindFinancialTransactionsByDateQueryHandler(IUnitOfWork unitOfWork, MapperConfiguration config)
        {
            _unitOfWork = unitOfWork;
            _mapper = config.CreateMapper();
        }

        public FinancialTransactionDto[] Handle(FindFinancialTransactionsByDateQuery query)
        {
            var transactions = _unitOfWork.FinancialTransactions.Find((account) => (account.Date == query.Date));
            var transactionDtos = _mapper.Map<IEnumerable<FinancialTransaction>, IEnumerable<FinancialTransactionDto>>(transactions);
            return transactionDtos.ToArray();

        }
    }
}
