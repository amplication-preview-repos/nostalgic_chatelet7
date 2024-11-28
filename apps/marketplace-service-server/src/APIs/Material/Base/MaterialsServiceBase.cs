using MarketplaceService.APIs;
using MarketplaceService.APIs.Common;
using MarketplaceService.APIs.Dtos;
using MarketplaceService.APIs.Errors;
using MarketplaceService.APIs.Extensions;
using MarketplaceService.Infrastructure;
using MarketplaceService.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace MarketplaceService.APIs;

public abstract class MaterialsServiceBase : IMaterialsService
{
    protected readonly MarketplaceServiceDbContext _context;

    public MaterialsServiceBase(MarketplaceServiceDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Material
    /// </summary>
    public async Task<Material> CreateMaterial(MaterialCreateInput createDto)
    {
        var material = new MaterialDbModel
        {
            CreatedAt = createDto.CreatedAt,
            Name = createDto.Name,
            Price = createDto.Price,
            Rating = createDto.Rating,
            TypeField = createDto.TypeField,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            material.Id = createDto.Id;
        }

        _context.Materials.Add(material);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<MaterialDbModel>(material.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Material
    /// </summary>
    public async Task DeleteMaterial(MaterialWhereUniqueInput uniqueId)
    {
        var material = await _context.Materials.FindAsync(uniqueId.Id);
        if (material == null)
        {
            throw new NotFoundException();
        }

        _context.Materials.Remove(material);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Materials
    /// </summary>
    public async Task<List<Material>> Materials(MaterialFindManyArgs findManyArgs)
    {
        var materials = await _context
            .Materials.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return materials.ConvertAll(material => material.ToDto());
    }

    /// <summary>
    /// Meta data about Material records
    /// </summary>
    public async Task<MetadataDto> MaterialsMeta(MaterialFindManyArgs findManyArgs)
    {
        var count = await _context.Materials.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Material
    /// </summary>
    public async Task<Material> Material(MaterialWhereUniqueInput uniqueId)
    {
        var materials = await this.Materials(
            new MaterialFindManyArgs { Where = new MaterialWhereInput { Id = uniqueId.Id } }
        );
        var material = materials.FirstOrDefault();
        if (material == null)
        {
            throw new NotFoundException();
        }

        return material;
    }

    /// <summary>
    /// Update one Material
    /// </summary>
    public async Task UpdateMaterial(
        MaterialWhereUniqueInput uniqueId,
        MaterialUpdateInput updateDto
    )
    {
        var material = updateDto.ToModel(uniqueId);

        _context.Entry(material).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Materials.Any(e => e.Id == material.Id))
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
