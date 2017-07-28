using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValueObjects.Finance;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class FinancialTransaction : IEntity<Guid>
    {
        internal FinancialTransaction() { }
        public FinancialTransaction(Guid id, Guid accountId, Money money, string notes, DateTime? date, TransactionType transactionType)
        {
            Id = id;
            AccountId = accountId;
            Money = money ?? new Money();
            Notes = notes;
            Date = date;
            TransactionType = transactionType;
        }
        public Guid Id { get; private set; }
        public Money Money { get; private set; }
        public string Notes { get; private set; }
        public DateTime? Date { get; private set; }
        public Guid AccountId { get; private set; }
        public TransactionType TransactionType { get; private set; }
        //public virtual FinancialAccount Account { get; private set;}
        public void ChangeInfo(Money money, string notes, DateTime? date, TransactionType transactionType)
        {
            Money = money;
            Notes = notes;
            Date = date;
            TransactionType = transactionType;
        }
    }
}
