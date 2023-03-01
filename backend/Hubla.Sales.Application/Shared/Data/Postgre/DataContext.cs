using Hubla.Sales.Application.Shared.Configurations;
using Hubla.Sales.Application.Shared.Sales.Entities;
using Hubla.Sales.Application.Shared.Sellers.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Reflection.Emit;

namespace Hubla.Sales.Application.Shared.Data.Postgre
{
    public class DataContext : DbContext
    {
        public virtual DbSet<Sale> Sales => Set<Sale>();
        public virtual DbSet<Seller> Sellers => Set<Seller>();

        public DataContext(DbContextOptions<DataContext> options, IOptions<ConnectionStrings> configuration) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.Entity<Sale>()
            //        .HasOne(e => e.Seller)
            //        .WithOne()
            //        .HasForeignKey("Sellers");

            builder.Entity<Seller>()
                    .HasMany(c => c.Sales)
                    .WithOne(e => e.Seller);
        }
    }
}