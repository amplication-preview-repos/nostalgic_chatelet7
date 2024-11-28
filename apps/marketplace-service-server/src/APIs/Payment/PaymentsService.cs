using MarketplaceService.Infrastructure;

namespace MarketplaceService.APIs;

public class PaymentsService : PaymentsServiceBase
{
    public PaymentsService(MarketplaceServiceDbContext context)
        : base(context) { }
}
