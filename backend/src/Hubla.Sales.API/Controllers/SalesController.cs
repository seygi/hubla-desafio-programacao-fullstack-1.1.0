using Hubla.Sales.Application.Features.CreateSale.UseCase;
using Hubla.Sales.Application.Shared.UseCase;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Hubla.Sales.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
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
            var output = await useCase.ExecuteAsync(input, cancellationToken);

            return Ok();
        }
    }
}
