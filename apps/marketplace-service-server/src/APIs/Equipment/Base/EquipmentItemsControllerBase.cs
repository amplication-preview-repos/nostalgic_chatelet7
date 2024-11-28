using MarketplaceService.APIs;
using MarketplaceService.APIs.Common;
using MarketplaceService.APIs.Dtos;
using MarketplaceService.APIs.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MarketplaceService.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class EquipmentItemsControllerBase : ControllerBase
{
    protected readonly IEquipmentItemsService _service;

    public EquipmentItemsControllerBase(IEquipmentItemsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Equipment
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Equipment>> CreateEquipment(EquipmentCreateInput input)
    {
        var equipment = await _service.CreateEquipment(input);

        return CreatedAtAction(nameof(Equipment), new { id = equipment.Id }, equipment);
    }

    /// <summary>
    /// Delete one Equipment
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DeleteEquipment(
        [FromRoute()] EquipmentWhereUniqueInput uniqueId
    )
    {
        try
        {
            await _service.DeleteEquipment(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many EquipmentItems
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<Equipment>>> EquipmentItems(
        [FromQuery()] EquipmentFindManyArgs filter
    )
    {
        return Ok(await _service.EquipmentItems(filter));
    }

    /// <summary>
    /// Meta data about Equipment records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> EquipmentItemsMeta(
        [FromQuery()] EquipmentFindManyArgs filter
    )
    {
        return Ok(await _service.EquipmentItemsMeta(filter));
    }

    /// <summary>
    /// Get one Equipment
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Equipment>> Equipment(
        [FromRoute()] EquipmentWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.Equipment(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Equipment
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateEquipment(
        [FromRoute()] EquipmentWhereUniqueInput uniqueId,
        [FromQuery()] EquipmentUpdateInput equipmentUpdateDto
    )
    {
        try
        {
            await _service.UpdateEquipment(uniqueId, equipmentUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
