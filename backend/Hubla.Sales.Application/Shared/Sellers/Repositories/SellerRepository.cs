using Hubla.Sales.Application.Shared.Data.Postgre;
using Hubla.Sales.Application.Shared.Models;
using Hubla.Sales.Application.Shared.Sellers.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hubla.Sales.Application.Shared.Sales.Repositories
{
    public class SellerRepository : ISellerRepository
    {
        private readonly DataContext _dataContext;

        public SellerRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public Task<Seller?> GetByIdAsync(int id)
        {
            return _dataContext.Sellers.FirstOrDefaultAsync(u => u.Id == id);
        }

        public Task<Seller?> GetByNameAsync(string name)
        {
            return _dataContext.Sellers.FirstOrDefaultAsync(u => u.Name.Equals(name));
        }

        public async Task<IList<Seller>> ListAsync()
        {
            return await _dataContext.Sellers.Include(u => u.Sales).ToListAsync();
        }

        public async Task<Seller> SaveAsync(Seller seller)
        {
            _dataContext.Sellers.Add(seller);
            await _dataContext.SaveChangesAsync();
            return seller;
        }

    }
}