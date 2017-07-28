using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationServices.CommandHandlers;
using FinanceManager.Contract.Commands;
using FinanceManager.Contract.Dto;
using Controllers.ViewInterfaces;
using System.Windows.Forms;

namespace Controllers.Controllers
{
    public class FinancialAccountController : IController
    {
        private ICommandHandler<AddFinancialAccountCommand> _handler;
        private IFinancialAccountView _view;

        public FinancialAccountController(IFinancialAccountView view, ICommandHandler<AddFinancialAccountCommand> handler)
        {
            _view = view;
            _view.SetController(this);
            _handler = handler;
        }

        public void ApplicationStart()
        {
            Application.Run((Form)_view);
        }

        //public FinancialAccountDto[] GetFinancialAccounts()
        //{

        //}

        public void AddFinancialAccount(FinancialAccountDto account)
        {
            var command = new AddFinancialAccountCommand(account);
            _handler.Execute(command);
        }
    }
}
