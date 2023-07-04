using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services;

namespace NewsProject.Areas.Admin;
[Area("Admin")]
[Authorize("Admin")]
public class NewsController : Controller
{
    #region Constructor
    private readonly INewsService _newsService;
    private readonly ICategoryService _categoryService;

    public NewsController(INewsService NewsService, ICategoryService categoryService)
    {
        _newsService = NewsService;
        _categoryService = categoryService;
    }
    #endregion
    #region Index
    public async Task<IActionResult> Index()
    {
        var res = await _newsService.ShowNewssAsync();
        return View(res);
    }
    #endregion
    #region Create
    public async Task<IActionResult> Create()
    {
        ViewBag.Categories = new SelectList(await _categoryService.ShowCategoriesAsync(), "Id", "Name");
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(News News, IFormFile? file)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Categories = new SelectList(await _categoryService.ShowCategoriesAsync(), "Id", "Name");

            ModelState.AddModelError("Name", "لطفا ورودی را درست وارد نمائید.");
            return View(News);
        }
        var res = await _newsService.AddNewsAsync(News, file);
        if (res.Id != 0)
        {
            return RedirectToAction(nameof(Index));
        }
        ViewBag.Categories = new SelectList(await _categoryService.ShowCategoriesAsync(), "Id", "Name");

        return View(News);
    }
    #endregion
    #region Edit
    public async Task<IActionResult> Edit(int id)
    {
        var res = await _newsService.FindNewsByIdAsync(id);
        ViewBag.Categories = new SelectList(await _categoryService.ShowCategoriesAsync(), "Id", "Name", res.CategoryId);

        return View(res);
    }
    [HttpPost]
    public async Task<IActionResult> Edit(News News, IFormFile? file)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Categories = new SelectList(await _categoryService.ShowCategoriesAsync(), "Id", "Name", News.CategoryId);

            ModelState.AddModelError("Name", "لطفا ورودی را درست وارد نمائید.");
            return View(News);
        }
        var res = await _newsService.UpdateNewsAsync(News, file);
        if (res.Id != 0)
        {
            return RedirectToAction(nameof(Index));
        }
        ViewBag.Categories = new SelectList(await _categoryService.ShowCategoriesAsync(), "Id", "Name", res.CategoryId);

        return View(News);
    }
    #endregion
    #region Delete
    public async Task<IActionResult> Delete(int id)
    {

        var res = await _newsService.FindNewsByIdAsync(id);
        return View(res);
    }
    [HttpPost]
    public async Task<IActionResult> Delete(News News)
    {
        if (News.Id == 0)
            return View(News);

        var res = await _newsService.DeleteNewsByIdAsync(News.Id);
        if (res)
            return RedirectToAction(nameof(Index));

        return View(News);
    }
    #endregion

}