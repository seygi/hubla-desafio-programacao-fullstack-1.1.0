using Hubla.Sales.Application.Shared.Notifications;
using Hubla.Sales.Application.Shared.Sales.Repositories;
using Hubla.Sales.Application.Shared.UseCase;
using System.Net;

namespace Hubla.Sales.Application.Features.GetSellers.UseCase
{
    internal sealed class GetSellersUseCase : IUseCase<DefaultInput, GetSellersListOutput>
    {
        private readonly INotificationContext _notificationContext;
        private readonly ISellerRepository _sellerRepository;

        public GetSellersUseCase(INotificationContext notificationContext, ISellerRepository sellerRepository)
        {
            _notificationContext = notificationContext;
            _sellerRepository = sellerRepository;
        }

        public async Task<GetSellersListOutput> ExecuteAsync(DefaultInput input, CancellationToken cancellationToken)
        {
            var sellers = await _sellerRepository.ListAsync();

            if (sellers.Any())
                return GetSellersListOutput.Success(sellers);

            _notificationContext.Create(HttpStatusCode.NotFound);
            return GetSellersListOutput.Empty;
        }
    }
}
