using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Opinio.API.Models.Category;
using Opinio.Core.Entities;
using Opinio.Core.Helpers;
using Opinio.Core.Services;

namespace Opinio.API.Controllers;

[ApiController]
[Route("api/")]
public class CategoriesController(ICategoryService categoryService, IMapper mapper) : ControllerBase
{
    #region POST - Create
    //[Authorize(Roles = "Admin")]
    [HttpPost("categories")]
    public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryRequest request, CancellationToken cancellationToken)
    {
        var category = mapper.Map<Category>(request);
        var createdCategory = await categoryService.CreateCategoryAsync(category, cancellationToken);

        var categoryResponse = mapper.Map<OperationResult<CreateCategoryResponse>>(createdCategory);
        return Ok(categoryResponse);
    }
    #endregion

    #region Put - Update

    [HttpPut("categories/{id}")]
    public async Task<IActionResult> UpdateCategory([FromRoute] int id, [FromBody] UpdateCategoryRequest request, CancellationToken cancellationToken)
    {
        var category = mapper.Map<Category>(request);

        category.Id = id;
        var updatedCategory = await categoryService.UpdateCategoryAsync(category, cancellationToken);

        var categoryResponse = mapper.Map<OperationResult<UpdateCategoryResponse>>(updatedCategory);
        return Ok(categoryResponse);
    }
    #endregion

    #region Delete - Delete

    [HttpDelete("categories/{id}")]
    public async Task<IActionResult> DeleteCategory([FromRoute] int id, CancellationToken cancellationToken)
    {
        var message = await categoryService.DeleteCategoryAsync(id, cancellationToken);

        return Ok(message);
    }
    #endregion

    #region Get - GetById

    [HttpGet("categories/{id}")]
    public async Task<IActionResult> GetCategory([FromRoute] int id, CancellationToken cancellationToken)
    {
        var category = await categoryService.GetCategoryAsync(id, cancellationToken);

        var categoryResponse = mapper.Map<OperationResult<GetCategoryResponse>>(category);

        return Ok(categoryResponse);
    }
    #endregion

    #region Get - List
    [HttpGet("categories")]
    public async Task<IActionResult> ListCategories(CancellationToken cancellationToken)
    {
        var categories = await categoryService.ListCategoriesAsync(cancellationToken);

        return Ok(categories);
    }
    #endregion


}
