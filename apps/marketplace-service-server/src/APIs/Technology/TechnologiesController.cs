using Microsoft.AspNetCore.Mvc;

namespace MarketplaceService.APIs;

[ApiController()]
public class TechnologiesController : TechnologiesControllerBase
{
    public TechnologiesController(ITechnologiesService service)
        : base(service) { }
}
