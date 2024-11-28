using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketplaceService.Infrastructure.Models;

[Table("Equipment")]
public class EquipmentDbModel
{
    [Required()]
    public DateTime CreatedAt { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    [StringLength(1000)]
    public string? Name { get; set; }

    [Range(-999999999, 999999999)]
    public double? Rating { get; set; }

    [Range(-999999999, 999999999)]
    public double? RentalCost { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }

    [StringLength(1000)]
    public string? Usage { get; set; }
}
