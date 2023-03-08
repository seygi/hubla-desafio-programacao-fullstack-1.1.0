using Hubla.Sales.Application.Shared.Sales.Entities;
using Hubla.Sales.Application.Shared.Sellers.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hubla.Sales.Application.Shared.Data.Postgres
{
    public class DataContext : DbContext
    {
        public virtual DbSet<Sale> Sales => Set<Sale>();
        public virtual DbSet<Seller> Sellers => Set<Seller>();

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Seller>()
                    .HasMany(c => c.Sales)
                    .WithOne(e => e.Seller);
        }
    }
}