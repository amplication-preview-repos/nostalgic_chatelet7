using MarketplaceService.APIs.Common;
using MarketplaceService.APIs.Dtos;

namespace MarketplaceService.APIs;

public interface ISearchesService
{
    /// <summary>
    /// Create one Search
    /// </summary>
    public Task<Search> CreateSearch(SearchCreateInput search);

    /// <summary>
    /// Delete one Search
    /// </summary>
    public Task DeleteSearch(SearchWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Searches
    /// </summary>
    public Task<List<Search>> Searches(SearchFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Search records
    /// </summary>
    public Task<MetadataDto> SearchesMeta(SearchFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Search
    /// </summary>
    public Task<Search> Search(SearchWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Search
    /// </summary>
    public Task UpdateSearch(SearchWhereUniqueInput uniqueId, SearchUpdateInput updateDto);
}
