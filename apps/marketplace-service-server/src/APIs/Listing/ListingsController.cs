using Microsoft.AspNetCore.Mvc;

namespace MarketplaceService.APIs;

[ApiController()]
public class ListingsController : ListingsControllerBase
{
    public ListingsController(IListingsService service)
        : base(service) { }
}
