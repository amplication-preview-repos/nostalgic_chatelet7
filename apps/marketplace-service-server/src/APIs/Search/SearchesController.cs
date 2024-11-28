using Microsoft.AspNetCore.Mvc;

namespace MarketplaceService.APIs;

[ApiController()]
public class SearchesController : SearchesControllerBase
{
    public SearchesController(ISearchesService service)
        : base(service) { }
}
