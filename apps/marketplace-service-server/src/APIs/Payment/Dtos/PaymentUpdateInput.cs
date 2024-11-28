using MarketplaceService.Core.Enums;

namespace MarketplaceService.APIs.Dtos;

public class PaymentUpdateInput
{
    public double? Amount { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? Id { get; set; }

    public DateTime? PaymentDate { get; set; }

    public PaymentMethodEnum? PaymentMethod { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
