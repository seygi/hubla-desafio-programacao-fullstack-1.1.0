using Hubla.Sales.Application.Shared.Notifications;

namespace Hubla.Sales.Application.Shared.Validator
{
    public interface IValidatorService<in TInput>
        where TInput : class
    {
        bool ValidateAndNotifyIfError(TInput input);

        bool Validate(TInput input, out NotificationErrors notificationErrors);
    }
}