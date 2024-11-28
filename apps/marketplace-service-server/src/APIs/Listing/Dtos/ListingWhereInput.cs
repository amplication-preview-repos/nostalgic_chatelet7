namespace MarketplaceService.APIs.Dtos;

public class ListingWhereInput
{
    public string? ContactEmail { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? Description { get; set; }

    public string? Id { get; set; }

    public double? Rating { get; set; }

    public string? Title { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
