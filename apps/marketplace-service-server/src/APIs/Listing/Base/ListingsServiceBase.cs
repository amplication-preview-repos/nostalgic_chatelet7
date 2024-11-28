using MarketplaceService.APIs;
using MarketplaceService.APIs.Common;
using MarketplaceService.APIs.Dtos;
using MarketplaceService.APIs.Errors;
using MarketplaceService.APIs.Extensions;
using MarketplaceService.Infrastructure;
using MarketplaceService.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace MarketplaceService.APIs;

public abstract class ListingsServiceBase : IListingsService
{
    protected readonly MarketplaceServiceDbContext _context;

    public ListingsServiceBase(MarketplaceServiceDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Listing
    /// </summary>
    public async Task<Listing> CreateListing(ListingCreateInput createDto)
    {
        var listing = new ListingDbModel
        {
            ContactEmail = createDto.ContactEmail,
            CreatedAt = createDto.CreatedAt,
            Description = createDto.Description,
            Rating = createDto.Rating,
            Title = createDto.Title,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            listing.Id = createDto.Id;
        }

        _context.Listings.Add(listing);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<ListingDbModel>(listing.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Listing
    /// </summary>
    public async Task DeleteListing(ListingWhereUniqueInput uniqueId)
    {
        var listing = await _context.Listings.FindAsync(uniqueId.Id);
        if (listing == null)
        {
            throw new NotFoundException();
        }

        _context.Listings.Remove(listing);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Listings
    /// </summary>
    public async Task<List<Listing>> Listings(ListingFindManyArgs findManyArgs)
    {
        var listings = await _context
            .Listings.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return listings.ConvertAll(listing => listing.ToDto());
    }

    /// <summary>
    /// Meta data about Listing records
    /// </summary>
    public async Task<MetadataDto> ListingsMeta(ListingFindManyArgs findManyArgs)
    {
        var count = await _context.Listings.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Listing
    /// </summary>
    public async Task<Listing> Listing(ListingWhereUniqueInput uniqueId)
    {
        var listings = await this.Listings(
            new ListingFindManyArgs { Where = new ListingWhereInput { Id = uniqueId.Id } }
        );
        var listing = listings.FirstOrDefault();
        if (listing == null)
        {
            throw new NotFoundException();
        }

        return listing;
    }

    /// <summary>
    /// Update one Listing
    /// </summary>
    public async Task UpdateListing(ListingWhereUniqueInput uniqueId, ListingUpdateInput updateDto)
    {
        var listing = updateDto.ToModel(uniqueId);

        _context.Entry(listing).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Listings.Any(e => e.Id == listing.Id))
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
