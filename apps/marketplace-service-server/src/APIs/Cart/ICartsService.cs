using MarketplaceService.APIs.Common;
using MarketplaceService.APIs.Dtos;

namespace MarketplaceService.APIs;

public interface ICartsService
{
    /// <summary>
    /// Create one Cart
    /// </summary>
    public Task<Cart> CreateCart(CartCreateInput cart);

    /// <summary>
    /// Delete one Cart
    /// </summary>
    public Task DeleteCart(CartWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Carts
    /// </summary>
    public Task<List<Cart>> Carts(CartFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Cart records
    /// </summary>
    public Task<MetadataDto> CartsMeta(CartFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Cart
    /// </summary>
    public Task<Cart> Cart(CartWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Cart
    /// </summary>
    public Task UpdateCart(CartWhereUniqueInput uniqueId, CartUpdateInput updateDto);
}
