namespace MarketplaceService.APIs.Dtos;

public class SearchWhereInput
{
    public DateTime? CreatedAt { get; set; }

    public string? Id { get; set; }

    public string? Query { get; set; }

    public string? Results { get; set; }

    public DateTime? Timestamp { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
