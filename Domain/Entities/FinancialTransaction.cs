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
        public FinancialTransaction(Guid id, Guid accountId)
        {
            Id = id;
            AccountId = accountId;
        }
        public Guid Id { get; private set; }
        public Money Money { get; private set; }
        public string Notes { get; private set; }
        public DateTime? Date { get; private set; }
        public Guid AccountId { get; private set; }
        public TransactionType TransactionType { get; private set; }
        public byte[] RowVersion { get; private set; }

        public void ChangeInfo(Money money, string notes, DateTime? date, TransactionType transactionType, byte[] rowVersion)
        {
            Money = money;
            Notes = notes;
            Date = date;
            TransactionType = transactionType;
            RowVersion = rowVersion;
        }
    }
}
