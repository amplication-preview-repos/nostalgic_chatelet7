using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketplaceService.Infrastructure.Models;

[Table("Searches")]
public class SearchDbModel
{
    [Required()]
    public DateTime CreatedAt { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    [StringLength(1000)]
    public string? Query { get; set; }

    public string? Results { get; set; }

    public DateTime? Timestamp { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
