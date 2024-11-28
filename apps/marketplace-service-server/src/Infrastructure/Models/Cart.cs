using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketplaceService.Infrastructure.Models;

[Table("Carts")]
public class CartDbModel
{
    [Required()]
    public DateTime CreatedAt { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    public string? Items { get; set; }

    [Range(-999999999, 999999999)]
    public double? TotalAmount { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }

    [StringLength(1000)]
    public string? UserId { get; set; }
}
