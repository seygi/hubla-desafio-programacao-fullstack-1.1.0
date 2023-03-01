using Hubla.Sales.Application.Shared.Models;
using Hubla.Sales.Application.Shared.Sellers.Entities;

namespace Hubla.Sales.Application.Shared.Sales.Repositories
{
    public interface ISellerRepository
    {
        Task<IList<Seller>> ListAsync();
        Task<Seller?> GetByIdAsync(int id);
        Task<Seller?> GetByNameAsync(string name);
        Task<Seller> SaveAsync(Seller seller);
    }
}