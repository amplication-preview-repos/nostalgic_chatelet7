namespace MarketplaceService.APIs.Dtos;

public class Equipment
{
    public DateTime CreatedAt { get; set; }

    public string Id { get; set; }

    public string? Name { get; set; }

    public double? Rating { get; set; }

    public double? RentalCost { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? Usage { get; set; }
}
