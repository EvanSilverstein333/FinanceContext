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
    public class GetFinancialTransactionByIdQueryHandler : IQueryHandler<GetFinancialTransactionByIdQuery, FinancialTransactionDto>
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public GetFinancialTransactionByIdQueryHandler(IUnitOfWork unitOfWork, MapperConfiguration config)
        {
            _unitOfWork = unitOfWork;
            _mapper = config.CreateMapper();
        }

        public FinancialTransactionDto Handle(GetFinancialTransactionByIdQuery query)
        {
            var transaction = _unitOfWork.FinancialTransactions.Get(query.TransactionId);
            var transactionDto = _mapper.Map<FinancialTransaction, FinancialTransactionDto>(transaction);
            return transactionDto;

        }
    }
}
