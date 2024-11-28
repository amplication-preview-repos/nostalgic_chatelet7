using MarketplaceService.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MarketplaceService.Infrastructure;

public class MarketplaceServiceDbContext : IdentityDbContext<IdentityUser>
{
    public MarketplaceServiceDbContext(DbContextOptions<MarketplaceServiceDbContext> options)
        : base(options) { }

    public DbSet<WorkerDbModel> Workers { get; set; }

    public DbSet<MaterialDbModel> Materials { get; set; }

    public DbSet<PaymentDbModel> Payments { get; set; }

    public DbSet<TechnologyDbModel> Technologies { get; set; }

    public DbSet<EquipmentDbModel> EquipmentItems { get; set; }

    public DbSet<ListingDbModel> Listings { get; set; }

    public DbSet<SearchDbModel> Searches { get; set; }

    public DbSet<CartDbModel> Carts { get; set; }
}
