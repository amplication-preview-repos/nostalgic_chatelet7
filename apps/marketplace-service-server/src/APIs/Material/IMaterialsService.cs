using MarketplaceService.APIs.Common;
using MarketplaceService.APIs.Dtos;

namespace MarketplaceService.APIs;

public interface IMaterialsService
{
    /// <summary>
    /// Create one Material
    /// </summary>
    public Task<Material> CreateMaterial(MaterialCreateInput material);

    /// <summary>
    /// Delete one Material
    /// </summary>
    public Task DeleteMaterial(MaterialWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Materials
    /// </summary>
    public Task<List<Material>> Materials(MaterialFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Material records
    /// </summary>
    public Task<MetadataDto> MaterialsMeta(MaterialFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Material
    /// </summary>
    public Task<Material> Material(MaterialWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Material
    /// </summary>
    public Task UpdateMaterial(MaterialWhereUniqueInput uniqueId, MaterialUpdateInput updateDto);
}
