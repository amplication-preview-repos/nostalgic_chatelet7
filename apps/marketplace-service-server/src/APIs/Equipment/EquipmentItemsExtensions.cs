using MarketplaceService.APIs.Dtos;
using MarketplaceService.Infrastructure.Models;

namespace MarketplaceService.APIs.Extensions;

public static class EquipmentItemsExtensions
{
    public static Equipment ToDto(this EquipmentDbModel model)
    {
        return new Equipment
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            Name = model.Name,
            Rating = model.Rating,
            RentalCost = model.RentalCost,
            UpdatedAt = model.UpdatedAt,
            Usage = model.Usage,
        };
    }

    public static EquipmentDbModel ToModel(
        this EquipmentUpdateInput updateDto,
        EquipmentWhereUniqueInput uniqueId
    )
    {
        var equipment = new EquipmentDbModel
        {
            Id = uniqueId.Id,
            Name = updateDto.Name,
            Rating = updateDto.Rating,
            RentalCost = updateDto.RentalCost,
            Usage = updateDto.Usage
        };

        if (updateDto.CreatedAt != null)
        {
            equipment.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            equipment.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return equipment;
    }
}
