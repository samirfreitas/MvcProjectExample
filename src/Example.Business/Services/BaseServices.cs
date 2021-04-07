using Example.Business.Intefaces;
using Example.Business.Models;
using Example.Business.Notifications;
using FluentValidation;
using FluentValidation.Results;

namespace Example.Business.Services
{
    public abstract class BaseServices
    {
        private readonly INotificator _notificator;

        public BaseServices(INotificator notificator)
        {
            _notificator = notificator;
        }

        protected void Notify(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                Notify(error.ErrorMessage);
            }
        }
        protected void Notify(string message)
        {
            _notificator.Handle(new Notification(message));
        }

        protected bool ExecuteValidation<TV,TE>(TV validation, TE entity) where TV : AbstractValidator<TE> where TE : Entity
        {
            var validator = validation.Validate(entity);

            if (validator.IsValid) return true;

            Notify(validator);

            return false;
        }
    }
}
