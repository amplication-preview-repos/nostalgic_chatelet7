using MarketplaceService.APIs;
using MarketplaceService.APIs.Common;
using MarketplaceService.APIs.Dtos;
using MarketplaceService.APIs.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MarketplaceService.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class CartsControllerBase : ControllerBase
{
    protected readonly ICartsService _service;

    public CartsControllerBase(ICartsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Cart
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Cart>> CreateCart(CartCreateInput input)
    {
        var cart = await _service.CreateCart(input);

        return CreatedAtAction(nameof(Cart), new { id = cart.Id }, cart);
    }

    /// <summary>
    /// Delete one Cart
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DeleteCart([FromRoute()] CartWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteCart(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Carts
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<Cart>>> Carts([FromQuery()] CartFindManyArgs filter)
    {
        return Ok(await _service.Carts(filter));
    }

    /// <summary>
    /// Meta data about Cart records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> CartsMeta([FromQuery()] CartFindManyArgs filter)
    {
        return Ok(await _service.CartsMeta(filter));
    }

    /// <summary>
    /// Get one Cart
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Cart>> Cart([FromRoute()] CartWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Cart(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Cart
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateCart(
        [FromRoute()] CartWhereUniqueInput uniqueId,
        [FromQuery()] CartUpdateInput cartUpdateDto
    )
    {
        try
        {
            await _service.UpdateCart(uniqueId, cartUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
