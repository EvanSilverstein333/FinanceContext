using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain.Entities;
using ValueObjects.Finance;

namespace Domain.Test
{
    [TestClass]
    public class FinancialAccountTests
    {
        private FinancialAccount _account;

        [TestInitialize]
        public void TestInitialize()
        {
            _account = new FinancialAccount(Guid.NewGuid());

        }

        [TestMethod]
        public void CalculateBalance_Equals30Dollars()
        {
            var transactionDebit50 = new FinancialTransaction(Guid.NewGuid(), _account.Id);
            transactionDebit50.ChangeInfo(new Money(50, null), null, null, TransactionType.Debit, null);
            var transactionCredit20 = new FinancialTransaction(Guid.NewGuid(), _account.Id);
            transactionCredit20.ChangeInfo(new Money(20, null), null, null, TransactionType.Credit, null);

            _account.Transactions.Add(transactionDebit50);
            _account.Transactions.Add(transactionCredit20);
            var balance = _account.CalculateBalance();

            Assert.AreEqual(balance.Amount, 30);

        }

        [TestMethod]
        public void ChangeName_True()
        {
            var newFirstName = "john";
            var newLastName = "smith";
            _account.ChangeName(newFirstName, newLastName, null);
            var newFullName = _account.FirstName + _account.LastName;

            Assert.AreEqual(newFullName, newFirstName + newLastName);

        }
    }
}
