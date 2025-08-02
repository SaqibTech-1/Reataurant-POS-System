using Microsoft.EntityFrameworkCore;
using RestaurantPOS.API.Entities;
using RestaurantPOS.API.Services.GetCurrentUser;
using System.Linq.Expressions;

namespace RestaurantPOS.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IUserContextService _userContextService;
        public ApplicationDbContext(DbContextOptions options, IUserContextService userContextService ) : base(options) {

            _userContextService = userContextService;


        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker
                .Entries<BaseEntity>();

            foreach (var entry in entries)
            {
                var now = DateTime.UtcNow;
                var userId = _userContextService.UserId;

                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedOn = now;
                    entry.Entity.CreatedBy = _userContextService.UserId ?? 0;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.ModifiedOn = now;
                    entry.Entity.ModifiedBy = _userContextService.UserId ?? 0;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }



    }
}
