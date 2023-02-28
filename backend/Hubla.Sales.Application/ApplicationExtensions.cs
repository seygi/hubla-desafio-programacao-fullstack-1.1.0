using Hubla.Sales.Application.Features.CreateSale.DependencyInjection;
using Hubla.Sales.Application.Shared.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

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
               .AddCreateSale();

            return services;
        }
    }
}
