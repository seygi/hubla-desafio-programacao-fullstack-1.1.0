using FluentValidation;
using Hubla.Sales.Application.Shared.Notifications;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Hubla.Sales.Application.Shared.Validator
{
    public class ValidatorService<TInput> : IValidatorService<TInput>
        where TInput : class
    {
        private readonly IValidator<TInput> _validator;
        private readonly INotificationContext _notificationContext;
        private readonly ILogger<ValidatorService<TInput>> _logger;

        public ValidatorService(IValidator<TInput> validator, INotificationContext notificationContext, ILogger<ValidatorService<TInput>> logger) => (_validator, _notificationContext, _logger) = (validator, notificationContext, logger);

        public bool ValidateAndNotifyIfError(TInput input)
        {
            var notificationErrors = NotificationErrors.Empty;
            var result = _validator.Validate(input);

            if (result.IsValid)
                return result.IsValid;

            foreach (var error in result.Errors)
            {
                notificationErrors.Add(error.PropertyName, error.ErrorMessage);
            }

            _logger.LogInformation("Input {Input} inválido", nameof(input));
            _notificationContext.Create(HttpStatusCode.BadRequest, notificationErrors);

            return result.IsValid;
        }

        public bool Validate(TInput input, out NotificationErrors notificationErrors)
        {
            notificationErrors = NotificationErrors.Empty;
            var result = _validator.Validate(input);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    notificationErrors.Add(error.PropertyName, error.ErrorMessage);
                }

                _logger.LogInformation("Input {Input} inválido", nameof(input));

                _notificationContext.Create(HttpStatusCode.BadRequest, notificationErrors);
            }

            return result.IsValid;
        }
    }
}