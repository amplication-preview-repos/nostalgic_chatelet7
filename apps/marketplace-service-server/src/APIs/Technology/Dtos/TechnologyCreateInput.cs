namespace MarketplaceService.APIs.Dtos;

public class TechnologyCreateInput
{
    public DateTime CreatedAt { get; set; }

    public string? Description { get; set; }

    public string? Id { get; set; }

    public string? Name { get; set; }

    public double? Rating { get; set; }

    public DateTime UpdatedAt { get; set; }
}
