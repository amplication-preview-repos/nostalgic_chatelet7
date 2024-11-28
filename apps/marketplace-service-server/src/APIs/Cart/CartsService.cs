using MarketplaceService.Infrastructure;

namespace MarketplaceService.APIs;

public class CartsService : CartsServiceBase
{
    public CartsService(MarketplaceServiceDbContext context)
        : base(context) { }
}
