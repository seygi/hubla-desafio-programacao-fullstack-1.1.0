using Hubla.Sales.Application.Shared.Models;
using Hubla.Sales.Application.Shared.Sales.Entities;

namespace Hubla.Sales.Application.Shared.Sales.Repositories
{
    public interface ISaleRepository
    {
        Task<IList<Sale>> ListAsync();

        Task<OperationResult> SaveAsync(IList<Sale> sales);
    }
}