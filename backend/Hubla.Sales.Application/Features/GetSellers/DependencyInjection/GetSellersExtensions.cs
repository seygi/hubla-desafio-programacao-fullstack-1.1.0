using Hubla.Sales.Application.Features.GetSellers.UseCase;
using Hubla.Sales.Application.Shared.UseCase;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Diagnostics.CodeAnalysis;

namespace Hubla.Sales.Application.Features.GetSellers.DependencyInjection
{
    [ExcludeFromCodeCoverage]
    internal static class GetSellersExtensions
    {
        public static IServiceCollection AddGetSellers(this IServiceCollection services)
        {
            services.TryAddScoped<IUseCase<DefaultInput, GetSellersListOutput>, GetSellersUseCase>();

            return services;
        }
    }
}
