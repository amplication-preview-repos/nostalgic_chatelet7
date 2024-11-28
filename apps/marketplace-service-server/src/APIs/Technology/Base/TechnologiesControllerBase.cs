using MarketplaceService.APIs;
using MarketplaceService.APIs.Common;
using MarketplaceService.APIs.Dtos;
using MarketplaceService.APIs.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MarketplaceService.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class TechnologiesControllerBase : ControllerBase
{
    protected readonly ITechnologiesService _service;

    public TechnologiesControllerBase(ITechnologiesService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Technology
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Technology>> CreateTechnology(TechnologyCreateInput input)
    {
        var technology = await _service.CreateTechnology(input);

        return CreatedAtAction(nameof(Technology), new { id = technology.Id }, technology);
    }

    /// <summary>
    /// Delete one Technology
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DeleteTechnology(
        [FromRoute()] TechnologyWhereUniqueInput uniqueId
    )
    {
        try
        {
            await _service.DeleteTechnology(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Technologies
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<Technology>>> Technologies(
        [FromQuery()] TechnologyFindManyArgs filter
    )
    {
        return Ok(await _service.Technologies(filter));
    }

    /// <summary>
    /// Meta data about Technology records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> TechnologiesMeta(
        [FromQuery()] TechnologyFindManyArgs filter
    )
    {
        return Ok(await _service.TechnologiesMeta(filter));
    }

    /// <summary>
    /// Get one Technology
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Technology>> Technology(
        [FromRoute()] TechnologyWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.Technology(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Technology
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateTechnology(
        [FromRoute()] TechnologyWhereUniqueInput uniqueId,
        [FromQuery()] TechnologyUpdateInput technologyUpdateDto
    )
    {
        try
        {
            await _service.UpdateTechnology(uniqueId, technologyUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
