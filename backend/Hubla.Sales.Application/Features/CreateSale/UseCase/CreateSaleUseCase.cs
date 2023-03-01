using Hubla.Sales.Application.Shared.Notifications;
using Hubla.Sales.Application.Shared.Sales.Entities;
using Hubla.Sales.Application.Shared.Sales.Enums;
using Hubla.Sales.Application.Shared.Sales.Repositories;
using Hubla.Sales.Application.Shared.UseCase;
using Hubla.Sales.Application.Shared.Validator;
using System.Net;

namespace Hubla.Sales.Application.Features.CreateSale.UseCase
{
    internal sealed class CreateSaleUseCase : IUseCase<CreateSaleInput, CreateSaleOutput>
    {
        private readonly IValidatorService<CreateSaleInput> _validatorService;
        private readonly INotificationContext _notificationContext;
        private readonly ISaleRepository _saleRepository;

        public CreateSaleUseCase(IValidatorService<CreateSaleInput> validatorService, INotificationContext notificationContext, ISaleRepository saleRepository)
        {
            _validatorService = validatorService;
            _notificationContext = notificationContext;
            _saleRepository = saleRepository;
        }

        public async Task<CreateSaleOutput> ExecuteAsync(CreateSaleInput input, CancellationToken cancellationToken)
        {
            if (!_validatorService.ValidateAndNotifyIfError(input))
                return CreateSaleOutput.Empty;

            var streamReader = new StreamReader(new MemoryStream(input.File));
            var saleList = new List<Sale>();

            while (!streamReader.EndOfStream)
            {
                var saleString = await streamReader.ReadLineAsync();
                if (string.IsNullOrWhiteSpace(saleString))
                {
                    _notificationContext.Create(HttpStatusCode.BadRequest, "File with blank line");
                    return CreateSaleOutput.Empty;
                }

                if (!TryParseToSale(saleString, out var sale))
                {
                    _notificationContext.Create(HttpStatusCode.BadRequest, "File with invalid data");
                    return CreateSaleOutput.Empty;
                }

                saleList.Add(sale);
            }

            var operationResult = await _saleRepository.SaveAsync(saleList);

            if (!operationResult.HasRowsAffected)
            {
                _notificationContext.Create(HttpStatusCode.InternalServerError, "Falha durante a inclusão do sale no banco de dados.");
                return CreateSaleOutput.Empty;
            }
            return CreateSaleOutput.Create(true);
        }

        private static bool TryParseToSale(string? saleString, out Sale sale)
        {
            sale = new Sale();

            if (!int.TryParse(saleString.Substring(0, 1), out var type))
                return false;
            if (!DateTime.TryParse(saleString.Substring(1, 25), out var date))
                return false;
            var description = saleString.Substring(26, 30).Trim();
            if (!double.TryParse(saleString.Substring(56, 10), out var value))
                return false;
            value = value / 100.0;
            var sellerName = saleString.Substring(66).Trim();


            sale = new Sale
            {
                Id = 0,
                Date = date,
                SaleType = (SaleType)type,
                Description = description,
                Value = value,
                SellerName = sellerName
            };

            return true;
        }
    }
}
