using Example.Business.Intefaces;
using Microsoft.AspNetCore.Mvc;

namespace Example.App.Controllers
{
    public abstract class BaseController : Controller
    {
        private readonly INotificator _notificator;

        public BaseController( INotificator notificator)
        {
            _notificator = notificator;
        }

        protected bool ValidOperation()
        {
            return !_notificator.HaveNotication();
        }

    }
}
