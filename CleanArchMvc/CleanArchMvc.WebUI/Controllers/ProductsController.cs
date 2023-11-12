using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CleanArchMvc.WebUI.Controllers;

public class ProductsController : Controller
{
    private readonly IProductService productService;
    private readonly ICategoryService categoryService;
    private readonly IWebHostEnvironment webHostEnvironment;

    public ProductsController(IProductService productService,
        ICategoryService categoryService,
        IWebHostEnvironment webHostEnvironment)
    {
        this.productService = productService;
        this.categoryService = categoryService;
        this.webHostEnvironment = webHostEnvironment;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var products = await productService.GetProductsAsync();
        return View(products);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        ViewBag.CategoryId =
            new SelectList(await categoryService.GetCategoriesAsync(), "Id", "Name");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProductDTO productDto)
    {
        if (ModelState.IsValid)
        {
            await productService.CreateAsync(productDto);
            return RedirectToAction(nameof(Index));
        }
        return View(productDto);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();
        var productDto = await productService.GetByIdAsync(id);
        if (productDto == null) return NotFound();

        var categories = await categoryService.GetCategoriesAsync();
        ViewBag.CategoryId = new SelectList(categories, "Id", "Name", productDto.CategoryId);
        return View(productDto);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(ProductDTO productDto)
    {
        if (ModelState.IsValid)
        {
            await productService.UpdateAsync(productDto);
            return RedirectToAction(nameof(Index));
        }
        return View(productDto);
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();
        var productDto = await productService.GetByIdAsync(id);
        if (productDto == null) return NotFound();
        return View(productDto);
    }

    [HttpPost(), ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirm(int id)
    {
        await productService.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();
        var productDto = await productService.GetByIdAsync(id);
        if (productDto == null) return NotFound();

        var wwwroot = webHostEnvironment.WebRootPath;
        var image = Path.Combine(wwwroot, "images\\" + productDto.Image);
        var exists = System.IO.File.Exists(image);
        ViewBag.ImageExist = exists;

        return View(productDto);
    }
}
