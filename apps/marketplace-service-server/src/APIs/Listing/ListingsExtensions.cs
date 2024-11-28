using MarketplaceService.APIs.Dtos;
using MarketplaceService.Infrastructure.Models;

namespace MarketplaceService.APIs.Extensions;

public static class ListingsExtensions
{
    public static Listing ToDto(this ListingDbModel model)
    {
        return new Listing
        {
            ContactEmail = model.ContactEmail,
            CreatedAt = model.CreatedAt,
            Description = model.Description,
            Id = model.Id,
            Rating = model.Rating,
            Title = model.Title,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static ListingDbModel ToModel(
        this ListingUpdateInput updateDto,
        ListingWhereUniqueInput uniqueId
    )
    {
        var listing = new ListingDbModel
        {
            Id = uniqueId.Id,
            ContactEmail = updateDto.ContactEmail,
            Description = updateDto.Description,
            Rating = updateDto.Rating,
            Title = updateDto.Title
        };

        if (updateDto.CreatedAt != null)
        {
            listing.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            listing.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return listing;
    }
}
