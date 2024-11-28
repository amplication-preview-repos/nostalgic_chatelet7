using Microsoft.AspNetCore.Mvc;

namespace MarketplaceService.APIs;

[ApiController()]
public class EquipmentItemsController : EquipmentItemsControllerBase
{
    public EquipmentItemsController(IEquipmentItemsService service)
        : base(service) { }
}
