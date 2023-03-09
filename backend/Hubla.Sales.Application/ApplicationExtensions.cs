using Hubla.Sales.Application.Features.CreateSale.DependencyInjection;
using Hubla.Sales.Application.Features.GetSales.DependencyInjection;
using Hubla.Sales.Application.Features.GetSellers.DependencyInjection;
using Hubla.Sales.Application.Shared.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Hubla.Sales.Tests.Unit")]
namespace Hubla.Sales.Application
{
    [ExcludeFromCodeCoverage]
    public static class ApplicationExtensions
    {
        public const string ApplicationName = "Hubla.Sales";

        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services
               .AddShared(configuration)
               .AddCreateSale()
               .AddGetSales()
               .AddGetSellers();

            return services;
        }
    }
}
