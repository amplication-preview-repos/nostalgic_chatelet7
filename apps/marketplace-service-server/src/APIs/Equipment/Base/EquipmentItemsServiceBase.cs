using MarketplaceService.APIs;
using MarketplaceService.APIs.Common;
using MarketplaceService.APIs.Dtos;
using MarketplaceService.APIs.Errors;
using MarketplaceService.APIs.Extensions;
using MarketplaceService.Infrastructure;
using MarketplaceService.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace MarketplaceService.APIs;

public abstract class EquipmentItemsServiceBase : IEquipmentItemsService
{
    protected readonly MarketplaceServiceDbContext _context;

    public EquipmentItemsServiceBase(MarketplaceServiceDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Equipment
    /// </summary>
    public async Task<Equipment> CreateEquipment(EquipmentCreateInput createDto)
    {
        var equipment = new EquipmentDbModel
        {
            CreatedAt = createDto.CreatedAt,
            Name = createDto.Name,
            Rating = createDto.Rating,
            RentalCost = createDto.RentalCost,
            UpdatedAt = createDto.UpdatedAt,
            Usage = createDto.Usage
        };

        if (createDto.Id != null)
        {
            equipment.Id = createDto.Id;
        }

        _context.EquipmentItems.Add(equipment);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<EquipmentDbModel>(equipment.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Equipment
    /// </summary>
    public async Task DeleteEquipment(EquipmentWhereUniqueInput uniqueId)
    {
        var equipment = await _context.EquipmentItems.FindAsync(uniqueId.Id);
        if (equipment == null)
        {
            throw new NotFoundException();
        }

        _context.EquipmentItems.Remove(equipment);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many EquipmentItems
    /// </summary>
    public async Task<List<Equipment>> EquipmentItems(EquipmentFindManyArgs findManyArgs)
    {
        var equipmentItems = await _context
            .EquipmentItems.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return equipmentItems.ConvertAll(equipment => equipment.ToDto());
    }

    /// <summary>
    /// Meta data about Equipment records
    /// </summary>
    public async Task<MetadataDto> EquipmentItemsMeta(EquipmentFindManyArgs findManyArgs)
    {
        var count = await _context.EquipmentItems.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Equipment
    /// </summary>
    public async Task<Equipment> Equipment(EquipmentWhereUniqueInput uniqueId)
    {
        var equipmentItems = await this.EquipmentItems(
            new EquipmentFindManyArgs { Where = new EquipmentWhereInput { Id = uniqueId.Id } }
        );
        var equipment = equipmentItems.FirstOrDefault();
        if (equipment == null)
        {
            throw new NotFoundException();
        }

        return equipment;
    }

    /// <summary>
    /// Update one Equipment
    /// </summary>
    public async Task UpdateEquipment(
        EquipmentWhereUniqueInput uniqueId,
        EquipmentUpdateInput updateDto
    )
    {
        var equipment = updateDto.ToModel(uniqueId);

        _context.Entry(equipment).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.EquipmentItems.Any(e => e.Id == equipment.Id))
            {
                throw new NotFoundException();
            }
            else
            {
                throw;
            }
        }
    }
}
