using MarketplaceService.Infrastructure;

namespace MarketplaceService.APIs;

public class SearchesService : SearchesServiceBase
{
    public SearchesService(MarketplaceServiceDbContext context)
        : base(context) { }
}
