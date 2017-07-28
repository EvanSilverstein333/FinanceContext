using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Controllers.Controllers;

namespace Controllers.ViewInterfaces
{
    public interface IView<TController> : IViewBase where TController : IController
    {
        void SetController(TController controller);
        void Show();
    }
}
