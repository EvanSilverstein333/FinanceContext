using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValueObjects.Finance;

namespace FinanceManager.Contract.Dto
{
    public class FinancialTransactionDto
    {
        public FinancialTransactionDto() { }
        public FinancialTransactionDto(Guid id, Guid accountId)
        {
            Id = id;
            AccountId = accountId;
        }

        [Browsable(false)]
        public Guid Id { get; set; }
        [Browsable(false)]
        public Guid AccountId { get; set; }
        public DateTime? Date { get; set; }
        public Money Money { get; set; }
        public TransactionType TransactionType { get; set; }
        public string Notes { get; set; }
        [Browsable(false)]
        public byte[] RowVersion { get; set; }

    }
}
