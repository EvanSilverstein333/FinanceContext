using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain.Entities;
using ValueObjects.Finance;

namespace Domain.Test
{
    [TestClass]
    public class FinancialTransactionTest
    {
        private FinancialTransaction _transaction;

        [TestInitialize]
        public void TestInitialize()
        {
            _transaction = new FinancialTransaction(Guid.NewGuid(),Guid.NewGuid());
        }

        [TestMethod]
        public void ChangeTransactionData_True()
        {
            var newMoney = new Money(10, null);
            var newNotes = "notes";
            var newDate = DateTime.Now;
            var newTransactionType = TransactionType.Credit;

            _transaction.ChangeInfo(newMoney, newNotes, newDate, newTransactionType, null);

            Assert.IsTrue(
                _transaction.Money == newMoney &&
                _transaction.Notes == newNotes &&
                _transaction.Date == newDate &&
                _transaction.TransactionType == newTransactionType
                 );

        }

    }
}
