using Hubla.Sales.Application.Shared.Configurations;
using Hubla.Sales.Application.Shared.Data.Sql;
using Hubla.Sales.Application.Shared.Notifications;
using Hubla.Sales.Application.Shared.Sales.Repositories;
using Hubla.Sales.Application.Shared.Validator;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Diagnostics.CodeAnalysis;

namespace Hubla.Sales.Application.Shared.DependencyInjection
{
    [ExcludeFromCodeCoverage]
    internal static class SharedExtensions
    {
        public static IServiceCollection AddShared(this IServiceCollection services, IConfiguration configuration)
        {
            return services
               .AddConnectionStrings()
               .AddSqlServer()
               .AddValidatorService()
               .AddSaleDependencyInjections()
               .AddNotificationDependencyInjections();
        }

        private static IServiceCollection AddConnectionStrings(this IServiceCollection services)
        {
            services.AddOptions<ConnectionStrings>()
               .Configure<IConfiguration>((settings, configuration) => { configuration.GetSection("ConnectionStrings").Bind(settings); });

            return services;
        }

        private static IServiceCollection AddSqlServer(this IServiceCollection services)
        {
            services.TryAddSingleton<IDataContext, DataContext>();
            services.TryAddScoped<ISqlService, SqlService>();

            return services;
        }

        private static IServiceCollection AddValidatorService(this IServiceCollection services)
        {
            services.AddScoped(typeof(IValidatorService<>), typeof(ValidatorService<>));

            return services;
        }

        private static IServiceCollection AddSaleDependencyInjections(this IServiceCollection services)
        {
            services.TryAddScoped<ISaleRepository, SaleRepository>();

            return services;
        }

        private static IServiceCollection AddNotificationDependencyInjections(this IServiceCollection services)
        {
            services.TryAddScoped<INotificationContext, NotificationContext>();

            return services;
        }
    }
}