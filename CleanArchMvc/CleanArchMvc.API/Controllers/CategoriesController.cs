using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchMvc.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        this.categoryService = categoryService ??
            throw new ArgumentNullException(nameof(categoryService));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryDTO>>> Get()
    {
        var categories = await categoryService.GetCategoriesAsync();
        if (categories == null) return NotFound();
        return Ok(categories);
    }

    [HttpGet("{id:int}", Name = "GetCategory")]
    public async Task<ActionResult<CategoryDTO>> Get(int id)
    {
        var category = await categoryService.GetByIdAsync(id);
        if (category == null) return NotFound();
        return Ok(category);
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] CategoryDTO categoryDTO)
    {
        if (categoryDTO == null) return BadRequest();
        await categoryService.CreateAsync(categoryDTO);

        return new CreatedAtRouteResult("GetCategory", new { id = categoryDTO.Id }, categoryDTO);
    }

    [HttpPut]
    public async Task<ActionResult> Put(int id, [FromBody] CategoryDTO categoryDTO)
    {
        if(categoryDTO == null || id != categoryDTO.Id) return BadRequest();
        await categoryService.UpdateAsync(categoryDTO);
        return Ok(categoryDTO);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<CategoryDTO>> Delete(int id)
    {
        var category = await categoryService.GetByIdAsync(id);
        if (category == null) return NotFound();
        await categoryService.DeleteAsync(id);
        return Ok(category);
    }
}
