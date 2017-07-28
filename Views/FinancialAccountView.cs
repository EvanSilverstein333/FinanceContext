using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Controllers.Controllers;
using Controllers.ViewInterfaces;
using FinanceManager.Contract.Dto;


namespace Views
{
    public partial class FinancialAccountView : Form, IFinancialAccountView
    {
        private FinancialAccountController _controller;
        public FinancialAccountView()
        {
            InitializeComponent();
            btnAddAccount.Click += BtnAddAccount_Click;
        }

        private void BtnAddAccount_Click(object sender, EventArgs e)
        {
            var account = new FinancialAccountDto();
            account.Id = Guid.NewGuid();
            account.FirstName = "Evand";
            account.LastName = "Silverstein";
            _controller.AddFinancialAccount(account);
        }


        //public void Initialize()
        //{
        //    _controller.get
        //}



        public void SetController(FinancialAccountController controller)
        {
            _controller = controller;
        }
    }
}
