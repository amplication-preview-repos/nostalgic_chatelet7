using Microsoft.AspNetCore.Mvc;

namespace MarketplaceService.APIs;

[ApiController()]
public class PaymentsController : PaymentsControllerBase
{
    public PaymentsController(IPaymentsService service)
        : base(service) { }
}
