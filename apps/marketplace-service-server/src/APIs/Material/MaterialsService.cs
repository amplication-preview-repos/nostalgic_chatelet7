using MarketplaceService.Infrastructure;

namespace MarketplaceService.APIs;

public class MaterialsService : MaterialsServiceBase
{
    public MaterialsService(MarketplaceServiceDbContext context)
        : base(context) { }
}
