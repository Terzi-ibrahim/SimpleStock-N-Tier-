using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SimpleStock.Application.DTOs;
using SimpleStock.Application.Interfaces;
using SimpleStock.Application.Services;
using SimpleStock.Domain.Interfaces;

public class ProductController : Controller
{

    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;

    public ProductController(IProductService productService, ICategoryService categoryService)
    {
        _productService = productService;
        _categoryService = categoryService;
    }

    // GET: /Product
    public async Task<IActionResult> Index()
    {
        var products = await _productService.GetAllAsync();
        return View(products);
    }

    // GET: /Product/Create
    public async Task<IActionResult> Create()
    {
        var categories = await _categoryService.GetAllAsync();
        ViewBag.Categories = new SelectList(categories, "Id", "Name");
        return View(new ProductDto());
    }

    // POST: /Product/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ProductDto productDto)
    {
        if (!ModelState.IsValid)
        {
            var categories = await _categoryService.GetAllAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name", productDto.CategoryId);
            return View(productDto);
        }

        await _productService.CreateAsync(productDto);
        return RedirectToAction(nameof(Index));
    }

    // GET: /Product/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        var product = await _productService.GetByIdAsync(id);
        if (product == null) return NotFound();

        var categories = await _categoryService.GetAllAsync();
        ViewBag.Categories = new SelectList(categories, "Id", "Name", product.CategoryId);

        return View(product);
    }

    // POST: /Product/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, ProductDto productDto)
    {
        if (!ModelState.IsValid)
        {
            var categories = await _categoryService.GetAllAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name", productDto.CategoryId);
            return View(productDto);
        }

        await _productService.UpdateAsync(productDto);
        return RedirectToAction(nameof(Index));
    }

    // GET: /Product/Delete/5
    public async Task<IActionResult> Delete(int id)
    {
        var product = await _productService.GetByIdAsync(id);
        if (product == null) return NotFound();
        return View(product);
    }

    // POST: /Product/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _productService.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }

}
