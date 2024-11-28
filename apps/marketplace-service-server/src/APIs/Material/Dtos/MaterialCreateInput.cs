namespace MarketplaceService.APIs.Dtos;

public class MaterialCreateInput
{
    public DateTime CreatedAt { get; set; }

    public string? Id { get; set; }

    public string? Name { get; set; }

    public double? Price { get; set; }

    public double? Rating { get; set; }

    public string? TypeField { get; set; }

    public DateTime UpdatedAt { get; set; }
}
