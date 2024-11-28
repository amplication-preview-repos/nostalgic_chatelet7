using MarketplaceService.APIs;
using MarketplaceService.APIs.Common;
using MarketplaceService.APIs.Dtos;
using MarketplaceService.APIs.Errors;
using MarketplaceService.APIs.Extensions;
using MarketplaceService.Infrastructure;
using MarketplaceService.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace MarketplaceService.APIs;

public abstract class SearchesServiceBase : ISearchesService
{
    protected readonly MarketplaceServiceDbContext _context;

    public SearchesServiceBase(MarketplaceServiceDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Search
    /// </summary>
    public async Task<Search> CreateSearch(SearchCreateInput createDto)
    {
        var search = new SearchDbModel
        {
            CreatedAt = createDto.CreatedAt,
            Query = createDto.Query,
            Results = createDto.Results,
            Timestamp = createDto.Timestamp,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            search.Id = createDto.Id;
        }

        _context.Searches.Add(search);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<SearchDbModel>(search.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Search
    /// </summary>
    public async Task DeleteSearch(SearchWhereUniqueInput uniqueId)
    {
        var search = await _context.Searches.FindAsync(uniqueId.Id);
        if (search == null)
        {
            throw new NotFoundException();
        }

        _context.Searches.Remove(search);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Searches
    /// </summary>
    public async Task<List<Search>> Searches(SearchFindManyArgs findManyArgs)
    {
        var searches = await _context
            .Searches.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return searches.ConvertAll(search => search.ToDto());
    }

    /// <summary>
    /// Meta data about Search records
    /// </summary>
    public async Task<MetadataDto> SearchesMeta(SearchFindManyArgs findManyArgs)
    {
        var count = await _context.Searches.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Search
    /// </summary>
    public async Task<Search> Search(SearchWhereUniqueInput uniqueId)
    {
        var searches = await this.Searches(
            new SearchFindManyArgs { Where = new SearchWhereInput { Id = uniqueId.Id } }
        );
        var search = searches.FirstOrDefault();
        if (search == null)
        {
            throw new NotFoundException();
        }

        return search;
    }

    /// <summary>
    /// Update one Search
    /// </summary>
    public async Task UpdateSearch(SearchWhereUniqueInput uniqueId, SearchUpdateInput updateDto)
    {
        var search = updateDto.ToModel(uniqueId);

        _context.Entry(search).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Searches.Any(e => e.Id == search.Id))
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
