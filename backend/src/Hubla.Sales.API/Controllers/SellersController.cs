using Hubla.Sales.API.Transport.V1.GetSellers;
using Hubla.Sales.Application.Features.GetSellers.UseCase;
using Hubla.Sales.Application.Shared.UseCase;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace Hubla.Sales.API.Controllers
{
    [ExcludeFromCodeCoverage]
    [Route("api/[controller]")]
    [ApiController]
    public class SellersController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll([FromServices]
            IUseCase<DefaultInput, GetSellersListOutput> useCase,
            CancellationToken cancellationToken)
        {
            var output = await useCase.ExecuteAsync(DefaultInput.Default, cancellationToken);
            var result = GetSellerResponse.Create(output);

            return Ok(result);
        }
    }
}
