using MarketplaceService.APIs.Common;
using MarketplaceService.APIs.Dtos;

namespace MarketplaceService.APIs;

public interface IListingsService
{
    /// <summary>
    /// Create one Listing
    /// </summary>
    public Task<Listing> CreateListing(ListingCreateInput listing);

    /// <summary>
    /// Delete one Listing
    /// </summary>
    public Task DeleteListing(ListingWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Listings
    /// </summary>
    public Task<List<Listing>> Listings(ListingFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Listing records
    /// </summary>
    public Task<MetadataDto> ListingsMeta(ListingFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Listing
    /// </summary>
    public Task<Listing> Listing(ListingWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Listing
    /// </summary>
    public Task UpdateListing(ListingWhereUniqueInput uniqueId, ListingUpdateInput updateDto);
}
