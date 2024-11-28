namespace MarketplaceService.APIs.Dtos;

public class Cart
{
    public DateTime CreatedAt { get; set; }

    public string Id { get; set; }

    public string? Items { get; set; }

    public double? TotalAmount { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? UserId { get; set; }
}
