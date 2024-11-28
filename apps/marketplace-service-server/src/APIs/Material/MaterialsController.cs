using Microsoft.AspNetCore.Mvc;

namespace MarketplaceService.APIs;

[ApiController()]
public class MaterialsController : MaterialsControllerBase
{
    public MaterialsController(IMaterialsService service)
        : base(service) { }
}
