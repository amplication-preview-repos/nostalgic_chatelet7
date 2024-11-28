using MarketplaceService.APIs;
using MarketplaceService.APIs.Common;
using MarketplaceService.APIs.Dtos;
using MarketplaceService.APIs.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MarketplaceService.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class ListingsControllerBase : ControllerBase
{
    protected readonly IListingsService _service;

    public ListingsControllerBase(IListingsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Listing
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Listing>> CreateListing(ListingCreateInput input)
    {
        var listing = await _service.CreateListing(input);

        return CreatedAtAction(nameof(Listing), new { id = listing.Id }, listing);
    }

    /// <summary>
    /// Delete one Listing
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DeleteListing([FromRoute()] ListingWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteListing(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Listings
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<Listing>>> Listings(
        [FromQuery()] ListingFindManyArgs filter
    )
    {
        return Ok(await _service.Listings(filter));
    }

    /// <summary>
    /// Meta data about Listing records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> ListingsMeta(
        [FromQuery()] ListingFindManyArgs filter
    )
    {
        return Ok(await _service.ListingsMeta(filter));
    }

    /// <summary>
    /// Get one Listing
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Listing>> Listing([FromRoute()] ListingWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Listing(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Listing
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateListing(
        [FromRoute()] ListingWhereUniqueInput uniqueId,
        [FromQuery()] ListingUpdateInput listingUpdateDto
    )
    {
        try
        {
            await _service.UpdateListing(uniqueId, listingUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
