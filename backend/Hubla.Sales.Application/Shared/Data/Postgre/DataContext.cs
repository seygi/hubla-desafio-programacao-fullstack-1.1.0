using Hubla.Sales.Application.Shared.Configurations;
using Hubla.Sales.Application.Shared.Sales.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Hubla.Sales.Application.Shared.Data.Postgre
{
    public class DataContext : DbContext
    {
        public virtual DbSet<Sale> Sales => Set<Sale>();
        
        public DataContext(DbContextOptions<DataContext> options, IOptions<ConnectionStrings> configuration) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}