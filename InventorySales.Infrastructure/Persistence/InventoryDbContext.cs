using InventorySales.Domain.DomainEvents;
using InventorySales.Domain.Entities;
using InventorySales.Domain.Entities.Auth;
using InventorySales.Domain.Entities.Common;
using InventorySales.Infrastructure.ModelBuilders;
using MediatR;


namespace InventorySales.Infrastructure.Persistence
{
    public class InventoryDbContext : DbContext
    {
        public readonly IMediator _mediator;

        public InventoryDbContext(DbContextOptions options, IMediator mediator)
         : base(options)
        {
            _mediator = mediator;
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<StockMovement> StockMovements { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductModelBuilder).Assembly);
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var domainEntities = ChangeTracker.Entries()
               .Where(e => e.Entity is IHasDomainEvents de && de.DomainEvents.Any())
               .Select(e => (IHasDomainEvents)e.Entity)
               .ToList();

            var events = domainEntities
                .SelectMany(x => x.DomainEvents)
                .ToList();

            var result = await base.SaveChangesAsync(cancellationToken);

            domainEntities.ForEach(entity => entity.ClearDomainEvents());

            foreach (var domainEvent in events)
                await _mediator.Publish(domainEvent);

            return result;
        }

    }
}
