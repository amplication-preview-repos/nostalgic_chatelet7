using MarketplaceService.APIs.Dtos;
using MarketplaceService.Infrastructure.Models;

namespace MarketplaceService.APIs.Extensions;

public static class SearchesExtensions
{
    public static Search ToDto(this SearchDbModel model)
    {
        return new Search
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            Query = model.Query,
            Results = model.Results,
            Timestamp = model.Timestamp,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static SearchDbModel ToModel(
        this SearchUpdateInput updateDto,
        SearchWhereUniqueInput uniqueId
    )
    {
        var search = new SearchDbModel
        {
            Id = uniqueId.Id,
            Query = updateDto.Query,
            Results = updateDto.Results,
            Timestamp = updateDto.Timestamp
        };

        if (updateDto.CreatedAt != null)
        {
            search.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            search.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return search;
    }
}
