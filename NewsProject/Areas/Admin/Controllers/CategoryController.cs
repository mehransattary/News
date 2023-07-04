using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace NewsProject.Areas.Admin;
[Area("Admin")]
[Authorize("Admin")]
public class CategoryController : Controller
{
    #region Constructor
    private readonly ICategoryService _categoryService;
    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }
    #endregion
    #region Index
    public async Task<IActionResult> Index()
    {
        var res = await _categoryService.ShowCategoriesAsync();
        return View(res);
    }
    #endregion
    #region Create
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(Category category)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError("Name", "لطفا ورودی را درست وارد نمائید.");
            return View(category);
        }
        var res = await _categoryService.AddCategoryAsync(category);
        if (!string.IsNullOrEmpty(res.Name))
        {
            return RedirectToAction(nameof(Index));
        }
        return View(category);
    }
    #endregion
    #region Edit
    public async Task<IActionResult> Edit(int id)
    {
        var res = await _categoryService.FindCategoryByIdAsync(id);
        return View(res);
    }
    [HttpPost]
    public async Task<IActionResult> Edit(Category category)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError("Name", "لطفا ورودی را درست وارد نمائید.");
            return View(category);
        }
        var res = await _categoryService.UpdateCategoryAsync(category);
        if (res.Id != 0)
        {
            return RedirectToAction(nameof(Index));
        }
       return View(category);
    }
    #endregion
    #region Delete
    public async Task<IActionResult> Delete(int id)
    {
         var res = await _categoryService.FindCategoryByIdAsync(id);
        return View(res);
    }
    [HttpPost]
    public async Task<IActionResult> Delete(Category category)
    {
        if (category.Id==0)      
            return View(category);
        
        var res = await _categoryService.DeleteCategoryByIdAsync(category.Id);
        if (res)       
            return RedirectToAction(nameof(Index));
        
       return View(category);
    }
    #endregion

}