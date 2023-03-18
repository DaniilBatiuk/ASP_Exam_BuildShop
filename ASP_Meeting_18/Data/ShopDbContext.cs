using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ASP_Meeting_18.Data
{
    public class ShopDbContext : IdentityDbContext<User>
    {
        public DbSet<Photo> Photos { get; set; } = default!;

        public DbSet<Category> Categories { get; set; } = default!;

        public DbSet<Product> Products { get; set; } = default!;

        public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
