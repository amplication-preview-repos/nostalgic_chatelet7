using MarketplaceService.APIs;
using MarketplaceService.APIs.Common;
using MarketplaceService.APIs.Dtos;
using MarketplaceService.APIs.Errors;
using MarketplaceService.APIs.Extensions;
using MarketplaceService.Infrastructure;
using MarketplaceService.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace MarketplaceService.APIs;

public abstract class CartsServiceBase : ICartsService
{
    protected readonly MarketplaceServiceDbContext _context;

    public CartsServiceBase(MarketplaceServiceDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Cart
    /// </summary>
    public async Task<Cart> CreateCart(CartCreateInput createDto)
    {
        var cart = new CartDbModel
        {
            CreatedAt = createDto.CreatedAt,
            Items = createDto.Items,
            TotalAmount = createDto.TotalAmount,
            UpdatedAt = createDto.UpdatedAt,
            UserId = createDto.UserId
        };

        if (createDto.Id != null)
        {
            cart.Id = createDto.Id;
        }

        _context.Carts.Add(cart);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<CartDbModel>(cart.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Cart
    /// </summary>
    public async Task DeleteCart(CartWhereUniqueInput uniqueId)
    {
        var cart = await _context.Carts.FindAsync(uniqueId.Id);
        if (cart == null)
        {
            throw new NotFoundException();
        }

        _context.Carts.Remove(cart);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Carts
    /// </summary>
    public async Task<List<Cart>> Carts(CartFindManyArgs findManyArgs)
    {
        var carts = await _context
            .Carts.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return carts.ConvertAll(cart => cart.ToDto());
    }

    /// <summary>
    /// Meta data about Cart records
    /// </summary>
    public async Task<MetadataDto> CartsMeta(CartFindManyArgs findManyArgs)
    {
        var count = await _context.Carts.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Cart
    /// </summary>
    public async Task<Cart> Cart(CartWhereUniqueInput uniqueId)
    {
        var carts = await this.Carts(
            new CartFindManyArgs { Where = new CartWhereInput { Id = uniqueId.Id } }
        );
        var cart = carts.FirstOrDefault();
        if (cart == null)
        {
            throw new NotFoundException();
        }

        return cart;
    }

    /// <summary>
    /// Update one Cart
    /// </summary>
    public async Task UpdateCart(CartWhereUniqueInput uniqueId, CartUpdateInput updateDto)
    {
        var cart = updateDto.ToModel(uniqueId);

        _context.Entry(cart).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Carts.Any(e => e.Id == cart.Id))
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
