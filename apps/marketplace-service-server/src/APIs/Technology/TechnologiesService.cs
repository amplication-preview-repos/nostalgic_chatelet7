using MarketplaceService.Infrastructure;

namespace MarketplaceService.APIs;

public class TechnologiesService : TechnologiesServiceBase
{
    public TechnologiesService(MarketplaceServiceDbContext context)
        : base(context) { }
}
