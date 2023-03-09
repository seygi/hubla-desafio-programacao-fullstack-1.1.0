using Hubla.Sales.Application.Shared.Data.Postgres;
using Hubla.Sales.Application.Shared.Models;
using Hubla.Sales.Application.Shared.Sales.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hubla.Sales.Application.Shared.Sales.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        private readonly DataContext _dataContext;

        public SaleRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IList<Sale>> ListAsync()
        {
            return await _dataContext.Sales.Include(u => u.Seller).ToListAsync();
        }

        public async Task<OperationResult> SaveAsync(IList<Sale> sales)
        {
            foreach (var sale in sales)
            {
                _dataContext.Sales.Add(sale);
            }
            await _dataContext.SaveChangesAsync();
            return OperationResult.Success(sales.Count);
        }
    }
}