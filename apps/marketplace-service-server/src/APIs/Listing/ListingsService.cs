using MarketplaceService.Infrastructure;

namespace MarketplaceService.APIs;

public class ListingsService : ListingsServiceBase
{
    public ListingsService(MarketplaceServiceDbContext context)
        : base(context) { }
}
