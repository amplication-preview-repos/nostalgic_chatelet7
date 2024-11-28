using MarketplaceService.APIs.Dtos;
using MarketplaceService.Infrastructure.Models;

namespace MarketplaceService.APIs.Extensions;

public static class TechnologiesExtensions
{
    public static Technology ToDto(this TechnologyDbModel model)
    {
        return new Technology
        {
            CreatedAt = model.CreatedAt,
            Description = model.Description,
            Id = model.Id,
            Name = model.Name,
            Rating = model.Rating,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static TechnologyDbModel ToModel(
        this TechnologyUpdateInput updateDto,
        TechnologyWhereUniqueInput uniqueId
    )
    {
        var technology = new TechnologyDbModel
        {
            Id = uniqueId.Id,
            Description = updateDto.Description,
            Name = updateDto.Name,
            Rating = updateDto.Rating
        };

        if (updateDto.CreatedAt != null)
        {
            technology.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            technology.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return technology;
    }
}
