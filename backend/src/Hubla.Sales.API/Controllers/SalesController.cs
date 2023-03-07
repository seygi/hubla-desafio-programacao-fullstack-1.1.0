using Hubla.Sales.API.Transport.V1.GetSales;
using Hubla.Sales.Application.Features.CreateSale.UseCase;
using Hubla.Sales.Application.Features.GetSales.UseCase;
using Hubla.Sales.Application.Shared.UseCase;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace Hubla.Sales.API.Controllers
{
    [ExcludeFromCodeCoverage]
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll([FromServices]
            IUseCase<DefaultInput, GetSalesListOutput> useCase,
            CancellationToken cancellationToken)
        {
            var output = await useCase.ExecuteAsync(DefaultInput.Default, cancellationToken);
            var result = GetSalesResponse.Create(output);

            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateSaleAsync(IFormFile file,
            [FromServices]
            IUseCase<CreateSaleInput, CreateSaleOutput> useCase,
            CancellationToken cancellationToken)
        {
            CreateSaleInput input;
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                var buffer = memoryStream.ToArray();
                input = CreateSaleInput.Create(buffer);
            }
            await useCase.ExecuteAsync(input, cancellationToken);

            return Ok();
        }
    }
}
