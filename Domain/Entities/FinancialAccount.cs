using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValueObjects.ContactInformation;
using ValueObjects.Finance;

namespace Domain.Entities
{
    public class FinancialAccount : IEntity<Guid>
    {
        internal FinancialAccount() { }
        public FinancialAccount(Guid id, string firstName, string lastName)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            //Address = address?? new Address();
        }
        public Guid Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        //public Address Address { get; private set; }
        public virtual ICollection<FinancialTransaction> Transactions { get; private set; }

        public Money CalculateBalance()
        {
            decimal balance = 0;
            foreach(var transaction in Transactions)
            {
                int signValue = (transaction.TransactionType == TransactionType.Credit) ? -1 : 1;
                balance = balance + (signValue * transaction.Money.Amount);
            }
            return new Money(balance, "CAN");
        }

        public void ChangeName(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }


    }
}
