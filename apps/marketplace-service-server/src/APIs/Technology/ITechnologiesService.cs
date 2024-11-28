using MarketplaceService.APIs.Common;
using MarketplaceService.APIs.Dtos;

namespace MarketplaceService.APIs;

public interface ITechnologiesService
{
    /// <summary>
    /// Create one Technology
    /// </summary>
    public Task<Technology> CreateTechnology(TechnologyCreateInput technology);

    /// <summary>
    /// Delete one Technology
    /// </summary>
    public Task DeleteTechnology(TechnologyWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Technologies
    /// </summary>
    public Task<List<Technology>> Technologies(TechnologyFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Technology records
    /// </summary>
    public Task<MetadataDto> TechnologiesMeta(TechnologyFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Technology
    /// </summary>
    public Task<Technology> Technology(TechnologyWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Technology
    /// </summary>
    public Task UpdateTechnology(
        TechnologyWhereUniqueInput uniqueId,
        TechnologyUpdateInput updateDto
    );
}
