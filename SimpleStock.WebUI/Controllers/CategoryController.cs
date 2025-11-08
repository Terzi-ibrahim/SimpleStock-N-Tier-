using Microsoft.AspNetCore.Mvc;
using SimpleStock.Application.DTOs;
using SimpleStock.Application.Interfaces;

public class CategoryController : Controller
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    // GET: /Category
    public async Task<IActionResult> Index()
    {
        var categories = await _categoryService.GetAllAsync();

        // Null kontrolü ekle
        if (categories == null)
            categories = new List<CategoryDto>();

        return View(categories);
    }


    // GET: /Category/Create
    public IActionResult Create()
    {
        return View(new CategoryDto());
    }

    // POST: /Category/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CategoryDto categoryDto)
    {
        if (!ModelState.IsValid)
            return View(categoryDto);

        await _categoryService.CreateAsync(categoryDto);
        return RedirectToAction(nameof(Index));
    }

    // GET: /Category/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        var category = await _categoryService.GetByIdAsync(id);
        if (category == null) return NotFound();

        return View(category);
    }

    // POST: /Category/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, CategoryDto categoryDto)
    {
        if (!ModelState.IsValid)
            return View(categoryDto);

        await _categoryService.UpdateAsync(categoryDto);
        return RedirectToAction(nameof(Index));
    }


    // GET: /Category/Delete/5
    public async Task<IActionResult> Delete(int id)
    {
        var category = await _categoryService.GetByIdAsync(id);
        if (category == null) return NotFound();

        return View(category);
    }

    // POST: /Category/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _categoryService.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
