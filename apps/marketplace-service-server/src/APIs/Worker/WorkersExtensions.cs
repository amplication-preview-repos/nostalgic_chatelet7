using MarketplaceService.APIs.Dtos;
using MarketplaceService.Infrastructure.Models;

namespace MarketplaceService.APIs.Extensions;

public static class WorkersExtensions
{
    public static Worker ToDto(this WorkerDbModel model)
    {
        return new Worker
        {
            Availability = model.Availability,
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            Name = model.Name,
            Rating = model.Rating,
            Skill = model.Skill,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static WorkerDbModel ToModel(
        this WorkerUpdateInput updateDto,
        WorkerWhereUniqueInput uniqueId
    )
    {
        var worker = new WorkerDbModel
        {
            Id = uniqueId.Id,
            Availability = updateDto.Availability,
            Name = updateDto.Name,
            Rating = updateDto.Rating,
            Skill = updateDto.Skill
        };

        if (updateDto.CreatedAt != null)
        {
            worker.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            worker.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return worker;
    }
}
