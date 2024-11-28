using Microsoft.AspNetCore.Mvc;

namespace MarketplaceService.APIs;

[ApiController()]
public class CartsController : CartsControllerBase
{
    public CartsController(ICartsService service)
        : base(service) { }
}
