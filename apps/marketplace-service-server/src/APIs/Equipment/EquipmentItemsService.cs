using MarketplaceService.Infrastructure;

namespace MarketplaceService.APIs;

public class EquipmentItemsService : EquipmentItemsServiceBase
{
    public EquipmentItemsService(MarketplaceServiceDbContext context)
        : base(context) { }
}
