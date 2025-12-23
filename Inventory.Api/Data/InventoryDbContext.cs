using Inventory.Api.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Api.Data;
public class InventoryDbContext: IdentityDbContext<ApplicationUser>
{
    public InventoryDbContext(DbContextOptions options) : base(options) { }

    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<Products> Products { get; set; }
    public DbSet<Category> Category { get; set; }
    public DbSet<User> User { get; set; }
    public DbSet<StockTransactions> StockTransactions { get; set; }
    public DbSet<UserPrimaryInfo> UserPrimaryInfos { get; set; }
}
