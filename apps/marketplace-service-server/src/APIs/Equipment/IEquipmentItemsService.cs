using MarketplaceService.APIs.Common;
using MarketplaceService.APIs.Dtos;

namespace MarketplaceService.APIs;

public interface IEquipmentItemsService
{
    /// <summary>
    /// Create one Equipment
    /// </summary>
    public Task<Equipment> CreateEquipment(EquipmentCreateInput equipment);

    /// <summary>
    /// Delete one Equipment
    /// </summary>
    public Task DeleteEquipment(EquipmentWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many EquipmentItems
    /// </summary>
    public Task<List<Equipment>> EquipmentItems(EquipmentFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Equipment records
    /// </summary>
    public Task<MetadataDto> EquipmentItemsMeta(EquipmentFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Equipment
    /// </summary>
    public Task<Equipment> Equipment(EquipmentWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Equipment
    /// </summary>
    public Task UpdateEquipment(EquipmentWhereUniqueInput uniqueId, EquipmentUpdateInput updateDto);
}
