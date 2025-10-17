using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Opinio.API.Models.Entity;
using Opinio.Core.Entities;
using Opinio.Core.Helpers;
using Opinio.Core.Services;

namespace Opinio.API.Controllers;

[ApiController]
[Route("api/")]
public class EntitiesController(IEntityService entityService, IMapper mapper) : ControllerBase
{
    #region POST - Create
    [HttpPost("entities")]
    public async Task<IActionResult> CreateEntity([FromBody] CreateEntityRequest request, CancellationToken cancellationToken)
    {
        var entity = mapper.Map<Entity>(request);
        var createdEntity = await entityService.CreateEntityAsync(entity, cancellationToken);

        var entityResponse = mapper.Map<OperationResult<CreateEntityResponse>>(createdEntity);
        return Ok(entityResponse);
    }
    #endregion

    #region Put - Update

    [HttpPut("entities/{id}")]
    public async Task<IActionResult> UpdateEntity([FromRoute] int id, [FromBody] UpdateEntityRequest request, CancellationToken cancellationToken)
    {
        var entity = mapper.Map<Entity>(request);

        entity.Id = id;
        var updatedEntity = await entityService.UpdateEntityAsync(entity, cancellationToken);

        var entityResponse = mapper.Map<OperationResult<UpdateEntityResponse>>(updatedEntity);
        return Ok(entityResponse);
    }
    #endregion

    #region Delete - Delete

    [HttpDelete("entities/{id}")]
    public async Task<IActionResult> DeleteEntity([FromRoute] int id, CancellationToken cancellationToken)
    {
        var message = await entityService.DeleteEntityAsync(id, cancellationToken);

        return Ok(message);
    }
    #endregion

    #region Get - GetById

    [HttpGet("entities/{id}")]
    public async Task<IActionResult> GetEntity([FromRoute] int id, CancellationToken cancellationToken)
    {
        var entity = await entityService.GetEntityAsync(id, cancellationToken);

        var entityResponse = mapper.Map<OperationResult<GetEntityResponse>>(entity);

        return Ok(entityResponse);
    }
    #endregion

    #region Get - List

    [HttpGet("categories/{categoryId}/entities")]
    public async Task<IActionResult> ListEntitiesByCategory([FromRoute] int categoryId, [FromQuery] int? status, CancellationToken cancellationToken)
    {
        var entities = await entityService.ListEntitiesByCategoryAsync(categoryId, status, cancellationToken);
        var entityResponse = mapper.Map<OperationResult<List<GetEntityResponse>>>(entities);

        return Ok(entityResponse);
    }
    #endregion

    #region Get - List

    [HttpGet("entities")]
    public async Task<IActionResult> ListEntities([FromQuery] int? pageNumber, [FromQuery] int? pageSize, [FromQuery] int? status, CancellationToken cancellationToken = default)
    {
        var entities = await entityService.ListEntitiesAsync(pageNumber ?? 1, pageSize ?? 10, status, cancellationToken);
        var entityResponse = mapper.Map<OperationResult<PaginatedResult<GetEntityResponse>>>(entities);

        return Ok(entityResponse);
    }
    #endregion
}
