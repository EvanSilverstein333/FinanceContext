using Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Persistance.Context;
using Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Test.Repositories
{
    [TestClass]
    public class FinancialTransactionRepositoryTest
    {
        private FinancialTransactionRepository _repo;
        private FinancialAccount _account;
        private IEnumerable<FinancialTransaction> _transactionCollection;
        private FinanceContext _mockContext;
        private DbSet<FinancialTransaction> _mockDbSet;

        [TestInitialize]
        public void TestInitialize()
        {
            _repo = new FinancialTransactionRepository();
            _account = new FinancialAccount(Guid.NewGuid());
            _transactionCollection = new List<FinancialTransaction>() {
                new FinancialTransaction(Guid.NewGuid(), _account.Id),
                new FinancialTransaction(Guid.NewGuid(), _account.Id)
            };

            _mockContext = Substitute.For<FinanceContext>();
            _mockDbSet = MockDbSet.GetFakeDbSet(_transactionCollection.AsQueryable());
            _mockContext.Set<FinancialTransaction>().Returns(_mockDbSet);
            _repo.SetContext(_mockContext);
        }

        [TestMethod]
        public void GetByAccountId_WithValidInputs()
        {
            _repo.GetByAccountId(_account.Id);
            _mockDbSet.Received().Where(transaction => transaction.AccountId == _account.Id).ToArray();
        }

        
    }
}
