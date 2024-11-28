using MarketplaceService.Infrastructure;

namespace MarketplaceService.APIs;

public class WorkersService : WorkersServiceBase
{
    public WorkersService(MarketplaceServiceDbContext context)
        : base(context) { }
}
