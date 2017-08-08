using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValueObjects.ContactInformation;
using System.ComponentModel;

namespace FinanceManager.Contract.Dto
{
    public class FinancialAccountDto
    {

        public FinancialAccountDto() { }
        public FinancialAccountDto(Guid id)
        {
            Id = id;
        }

        [Browsable(false)]
        public Guid Id { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Browsable(false)]
        public byte[] RowVersion { get; set; }
    }
}
