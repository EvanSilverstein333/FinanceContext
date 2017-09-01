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

namespace Persistance.Test.Repositories
{
    [TestClass]
    public class FinanceAccountRepositoryTest
    {
        private FinancialAccountRepository _repo;
        private FinancialAccount _account;
        private IEnumerable<FinancialAccount> _accountCollection;
        private FinanceContext _mockContext;
        private DbSet<FinancialAccount> _mockDbSet;


        [TestInitialize]
        public void TestInitialize()
        {
            _repo = new FinancialAccountRepository();
            _account = new FinancialAccount(Guid.NewGuid());
            _accountCollection = new List<FinancialAccount>() { _account, new FinancialAccount(Guid.NewGuid())};
            _mockContext = Substitute.For<FinanceContext>();
            _mockDbSet = MockDbSet.GetFakeDbSet(_accountCollection.AsQueryable());
            _mockContext.Set<FinancialAccount>().Returns(_mockDbSet);
            _repo.SetContext(_mockContext);
        }



        [TestMethod]
        public void Add_WithValidInputs()
        {
            _repo.Add(_account);
            _mockDbSet.Received().Add(_account);

        }

        [TestMethod]
        public void AddRange_WithValidInputs()
        {
            _repo.AddRange(_accountCollection);
            _mockDbSet.Received().AddRange(_accountCollection);
        }


        [TestMethod]
        public void Remove_WithValidInputs()
        {
            _repo.Remove(_account);
            _mockContext.Received().Entry(_account);
        }

        [TestMethod]
        public void RemoveRange_WithValidInputs()
        {
            _repo.RemoveRange(_accountCollection);
            //_repo.Received(_accountCollection.Count()).Remove(Arg.Any<FinancialAccount>());
            foreach(var account in _accountCollection)
            {
                _mockContext.Received().Entry(account);
            }

        }

        [TestMethod]
        public void Update_WithValidInputs()
        {
            _repo.Update(_account);
            _mockContext.Received().Entry(_account);
        }

        [TestMethod]
        public void Find_WithValidInputs()
        {
            Expression<Func<FinancialAccount, bool>> predicate = account => account.Id == Guid.NewGuid();
            _repo.Find(predicate);
            _mockDbSet.Received().Where(predicate);
        }

        [TestMethod]
        public void Get_WithValidInputs()
        {
            _repo.Get(_account.Id);
            _mockDbSet.Received().Find(_account.Id);
        }

        public void GetAll_WithValidInputs()
        {
            _repo.GetAll();
            _mockDbSet.Received().ToList();
        }


        [TestMethod]
        public void Attach_WithValidInputs()
        {
            _repo.Attach(_account);
            _mockDbSet.Received().Attach(_account);
        }

        [TestMethod]
        public void Detach_WithValidInputs()
        {
            _repo.Detach(_account);
            _mockContext.Received().Entry(_account);

        }

        [TestMethod]
        public void GetWithTransactions_WithValidInputs()
        {
            var mockDbSet2 = MockDbSet.GetFakeDbSet(_accountCollection.AsQueryable()); // for include
            _mockDbSet.Include(account => account.Transactions).Returns(mockDbSet2);

            _repo.GetWithTransactions(_account.Id);
            mockDbSet2.Received().Where(account => account.Id == _account.Id).FirstOrDefault(); 
        }

    }
}
