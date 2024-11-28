using MarketplaceService.APIs;
using MarketplaceService.APIs.Common;
using MarketplaceService.APIs.Dtos;
using MarketplaceService.APIs.Errors;
using MarketplaceService.APIs.Extensions;
using MarketplaceService.Infrastructure;
using MarketplaceService.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace MarketplaceService.APIs;

public abstract class TechnologiesServiceBase : ITechnologiesService
{
    protected readonly MarketplaceServiceDbContext _context;

    public TechnologiesServiceBase(MarketplaceServiceDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Technology
    /// </summary>
    public async Task<Technology> CreateTechnology(TechnologyCreateInput createDto)
    {
        var technology = new TechnologyDbModel
        {
            CreatedAt = createDto.CreatedAt,
            Description = createDto.Description,
            Name = createDto.Name,
            Rating = createDto.Rating,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            technology.Id = createDto.Id;
        }

        _context.Technologies.Add(technology);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<TechnologyDbModel>(technology.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Technology
    /// </summary>
    public async Task DeleteTechnology(TechnologyWhereUniqueInput uniqueId)
    {
        var technology = await _context.Technologies.FindAsync(uniqueId.Id);
        if (technology == null)
        {
            throw new NotFoundException();
        }

        _context.Technologies.Remove(technology);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Technologies
    /// </summary>
    public async Task<List<Technology>> Technologies(TechnologyFindManyArgs findManyArgs)
    {
        var technologies = await _context
            .Technologies.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return technologies.ConvertAll(technology => technology.ToDto());
    }

    /// <summary>
    /// Meta data about Technology records
    /// </summary>
    public async Task<MetadataDto> TechnologiesMeta(TechnologyFindManyArgs findManyArgs)
    {
        var count = await _context.Technologies.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Technology
    /// </summary>
    public async Task<Technology> Technology(TechnologyWhereUniqueInput uniqueId)
    {
        var technologies = await this.Technologies(
            new TechnologyFindManyArgs { Where = new TechnologyWhereInput { Id = uniqueId.Id } }
        );
        var technology = technologies.FirstOrDefault();
        if (technology == null)
        {
            throw new NotFoundException();
        }

        return technology;
    }

    /// <summary>
    /// Update one Technology
    /// </summary>
    public async Task UpdateTechnology(
        TechnologyWhereUniqueInput uniqueId,
        TechnologyUpdateInput updateDto
    )
    {
        var technology = updateDto.ToModel(uniqueId);

        _context.Entry(technology).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Technologies.Any(e => e.Id == technology.Id))
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
