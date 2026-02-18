using InventorySales.Application.Abstractions;
using InventorySales.Application.Abstractions.Services;
using InventorySales.Domain.DomainEvents;
using InventorySales.Domain.DomainEvents.Events;
using InventorySales.Domain.Entities;
using InventorySales.Domain.Entities.Auth;
using InventorySales.Domain.Entities.Common;
using InventorySales.Infrastructure.ModelBuilders;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Text.Json;


namespace InventorySales.Infrastructure.Persistence
{
    public class InventoryDbContext : DbContext
    {
        public InventoryDbContext(DbContextOptions options)
         : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<StockMovement> StockMovements { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleItem> SaleItems { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductModelBuilder).Assembly);
        }
    }
}
