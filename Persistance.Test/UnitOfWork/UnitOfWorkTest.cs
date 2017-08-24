using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Persistance.Context;
using Persistance.Repositories;
using System.Transactions;
using Domain.Entities;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Data.Entity.Infrastructure;
using NSubstitute;
using Persistance.UnitOfWork;

namespace Persistance.Test.UnitOfWork
{
    [TestClass]
    public class UnitOfWorkTest
    {
        private Persistance.UnitOfWork.UnitOfWork _uow;
        private FinancialAccountRepository _mockAccountRepo;
        private FinancialTransactionRepository _mockTransactionRepo;

        private FinanceContext _mockContext;


        [TestInitialize]
        public void TestInitialize()
        {
            _mockContext = Substitute.For<FinanceContext>();
            _mockAccountRepo = Substitute.For<FinancialAccountRepository>();
            _mockTransactionRepo = Substitute.For<FinancialTransactionRepository>();

            _uow = new Persistance.UnitOfWork.UnitOfWork(_mockContext,_mockAccountRepo, _mockTransactionRepo);
            
        }

        [TestMethod]
        public void Complete_WithValidInputs()
        {
            _uow.Complete();
            _mockContext.Received().SaveChanges();
        }


        [TestMethod]
        public void Dispose_WithValidInputs()
        {
            _uow.Dispose();
            _mockContext.Received().Dispose();
        }
    }
}
