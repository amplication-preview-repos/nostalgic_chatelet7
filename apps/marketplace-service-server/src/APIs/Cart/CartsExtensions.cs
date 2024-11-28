using MarketplaceService.APIs.Dtos;
using MarketplaceService.Infrastructure.Models;

namespace MarketplaceService.APIs.Extensions;

public static class CartsExtensions
{
    public static Cart ToDto(this CartDbModel model)
    {
        return new Cart
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            Items = model.Items,
            TotalAmount = model.TotalAmount,
            UpdatedAt = model.UpdatedAt,
            UserId = model.UserId,
        };
    }

    public static CartDbModel ToModel(this CartUpdateInput updateDto, CartWhereUniqueInput uniqueId)
    {
        var cart = new CartDbModel
        {
            Id = uniqueId.Id,
            Items = updateDto.Items,
            TotalAmount = updateDto.TotalAmount,
            UserId = updateDto.UserId
        };

        if (updateDto.CreatedAt != null)
        {
            cart.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            cart.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return cart;
    }
}
