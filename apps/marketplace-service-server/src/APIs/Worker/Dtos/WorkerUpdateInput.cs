namespace MarketplaceService.APIs.Dtos;

public class WorkerUpdateInput
{
    public bool? Availability { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? Id { get; set; }

    public string? Name { get; set; }

    public double? Rating { get; set; }

    public string? Skill { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
