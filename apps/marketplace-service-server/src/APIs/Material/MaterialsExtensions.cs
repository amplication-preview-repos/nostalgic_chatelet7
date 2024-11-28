using MarketplaceService.APIs.Dtos;
using MarketplaceService.Infrastructure.Models;

namespace MarketplaceService.APIs.Extensions;

public static class MaterialsExtensions
{
    public static Material ToDto(this MaterialDbModel model)
    {
        return new Material
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            Name = model.Name,
            Price = model.Price,
            Rating = model.Rating,
            TypeField = model.TypeField,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static MaterialDbModel ToModel(
        this MaterialUpdateInput updateDto,
        MaterialWhereUniqueInput uniqueId
    )
    {
        var material = new MaterialDbModel
        {
            Id = uniqueId.Id,
            Name = updateDto.Name,
            Price = updateDto.Price,
            Rating = updateDto.Rating,
            TypeField = updateDto.TypeField
        };

        if (updateDto.CreatedAt != null)
        {
            material.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            material.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return material;
    }
}
