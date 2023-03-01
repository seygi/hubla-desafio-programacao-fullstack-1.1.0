using Hubla.Sales.Application.Shared.Notifications;
using Hubla.Sales.Application.Shared.Sales.Repositories;
using Hubla.Sales.Application.Shared.UseCase;
using System.Net;

namespace Hubla.Sales.Application.Features.GetSales.UseCase
{
    internal sealed class GetSalesUseCase : IUseCase<DefaultInput, GetSalesListOutput>
    {
        private readonly INotificationContext _notificationContext;
        private readonly ISaleRepository _saleRepository;

        public GetSalesUseCase(INotificationContext notificationContext, ISaleRepository saleRepository)
        {
            _notificationContext = notificationContext;
            _saleRepository = saleRepository;
        }

        public async Task<GetSalesListOutput> ExecuteAsync(DefaultInput input, CancellationToken cancellationToken)
        {
            var sales = await _saleRepository.ListAsync();

            if (sales.Any())
                return GetSalesListOutput.Success(sales);

            _notificationContext.Create(HttpStatusCode.NotFound);
            return GetSalesListOutput.Empty;
        }
    }
}
