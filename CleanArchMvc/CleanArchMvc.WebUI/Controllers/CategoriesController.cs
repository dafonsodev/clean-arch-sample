using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchMvc.WebUI.Controllers;

public class CategoriesController : Controller
{
    private readonly ICategoryService categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        this.categoryService = categoryService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var categories = await categoryService.GetCategoriesAsync();
        return View(categories);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CategoryDTO categoryDto)
    {
        if (ModelState.IsValid)
        {
            await categoryService.CreateAsync(categoryDto);
            return RedirectToAction(nameof(Index));
        }
        return View(categoryDto);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();
        var categoryDto = await categoryService.GetByIdAsync(id);
        if (categoryDto == null) return NotFound();
        return View(categoryDto);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(CategoryDTO categoryDto)
    {
        if (ModelState.IsValid)
        {
            await categoryService.UpdateAsync(categoryDto);
            return RedirectToAction(nameof(Index));
        }
        return View(categoryDto);
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();
        var categoryDto = await categoryService.GetByIdAsync(id);
        if (categoryDto == null) return NotFound();
        return View(categoryDto);
    }

    [HttpPost(), ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirm(int id)
    {
        await categoryService.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();
        var categoryDto = await categoryService.GetByIdAsync(id);
        if (categoryDto == null) return NotFound();
        return View(categoryDto);
    }
}
