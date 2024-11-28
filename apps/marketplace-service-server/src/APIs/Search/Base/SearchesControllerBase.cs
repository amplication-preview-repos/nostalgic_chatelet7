using MarketplaceService.APIs;
using MarketplaceService.APIs.Common;
using MarketplaceService.APIs.Dtos;
using MarketplaceService.APIs.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MarketplaceService.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class SearchesControllerBase : ControllerBase
{
    protected readonly ISearchesService _service;

    public SearchesControllerBase(ISearchesService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Search
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Search>> CreateSearch(SearchCreateInput input)
    {
        var search = await _service.CreateSearch(input);

        return CreatedAtAction(nameof(Search), new { id = search.Id }, search);
    }

    /// <summary>
    /// Delete one Search
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DeleteSearch([FromRoute()] SearchWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteSearch(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Searches
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<Search>>> Searches([FromQuery()] SearchFindManyArgs filter)
    {
        return Ok(await _service.Searches(filter));
    }

    /// <summary>
    /// Meta data about Search records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> SearchesMeta(
        [FromQuery()] SearchFindManyArgs filter
    )
    {
        return Ok(await _service.SearchesMeta(filter));
    }

    /// <summary>
    /// Get one Search
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Search>> Search([FromRoute()] SearchWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Search(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Search
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateSearch(
        [FromRoute()] SearchWhereUniqueInput uniqueId,
        [FromQuery()] SearchUpdateInput searchUpdateDto
    )
    {
        try
        {
            await _service.UpdateSearch(uniqueId, searchUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
