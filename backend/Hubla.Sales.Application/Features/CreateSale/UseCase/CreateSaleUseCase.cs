using Hubla.Sales.Application.Shared.Notifications;
using Hubla.Sales.Application.Shared.Sales.Entities;
using Hubla.Sales.Application.Shared.Sales.Enums;
using Hubla.Sales.Application.Shared.Sales.Repositories;
using Hubla.Sales.Application.Shared.Sellers.Entities;
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
        private readonly ISellerRepository _sellerRepository;

        public CreateSaleUseCase(IValidatorService<CreateSaleInput> validatorService, INotificationContext notificationContext, ISaleRepository saleRepository, ISellerRepository sellerRepository)
        {
            _validatorService = validatorService;
            _notificationContext = notificationContext;
            _saleRepository = saleRepository;
            _sellerRepository = sellerRepository;
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

                if (!TryParseToSale(saleString, out var sale, out var sellerName))
                {
                    _notificationContext.Create(HttpStatusCode.BadRequest, "File with invalid data");
                    return CreateSaleOutput.Empty;
                }

                var seller = await _sellerRepository.GetByNameAsync(sellerName);
                if (seller == null)
                    seller = await _sellerRepository.SaveAsync(new Seller { Name = sellerName });

                sale.Seller = seller;

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

        private bool TryParseToSale(string? saleString, out Sale sale, out string sellerName)
        {
            sale = new Sale();
            sellerName = null;

            if (!int.TryParse(saleString.Substring(0, 1), out var type))
                return false;
            if (!DateTime.TryParse(saleString.Substring(1, 25), out var date))
                return false;
            var description = saleString.Substring(26, 30).Trim();
            if (!decimal.TryParse(saleString.Substring(56, 10), out var value))
                return false;
            value = value / 100;
            sellerName = saleString.Substring(66).Trim();

            sale = new Sale
            {
                Id = 0,
                Date = date,
                SaleType = (SaleType)type,
                Description = description,
                Value = value
            };

            return true;
        }
    }
}
