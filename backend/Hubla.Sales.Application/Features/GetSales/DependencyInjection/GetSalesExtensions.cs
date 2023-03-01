using Hubla.Sales.Application.Features.GetSales.UseCase;
using Hubla.Sales.Application.Shared.UseCase;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Diagnostics.CodeAnalysis;

namespace Hubla.Sales.Application.Features.GetSales.DependencyInjection
{
    [ExcludeFromCodeCoverage]
    internal static class GetSalesExtensions
    {
        public static IServiceCollection AddGetSales(this IServiceCollection services)
        {
            services.TryAddScoped<IUseCase<DefaultInput, GetSalesListOutput>, GetSalesUseCase>();

            return services;
        }
    }
}
