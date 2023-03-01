using FluentValidation;
using Hubla.Sales.Application.Features.CreateSale.UseCase;
using Hubla.Sales.Application.Features.CreateSale.Validators;
using Hubla.Sales.Application.Features.GetSales.UseCase;
using Hubla.Sales.Application.Shared.UseCase;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Diagnostics.CodeAnalysis;

namespace Hubla.Sales.Application.Features.CreateSale.DependencyInjection
{
    [ExcludeFromCodeCoverage]
    internal static class CreateSaleExtensions
    {
        public static IServiceCollection AddCreateSale(this IServiceCollection services)
        {
            services.TryAddSingleton<IValidator<CreateSaleInput>, CreateSaleValidator>();
            services.TryAddScoped<IUseCase<CreateSaleInput, CreateSaleOutput>, CreateSaleUseCase>();

            return services;
        }
    }
}
