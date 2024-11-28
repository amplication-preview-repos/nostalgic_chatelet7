using Microsoft.AspNetCore.Mvc;

namespace MarketplaceService.APIs;

[ApiController()]
public class WorkersController : WorkersControllerBase
{
    public WorkersController(IWorkersService service)
        : base(service) { }
}
