using Microsoft.AspNetCore.Mvc;
using Services;

namespace NewsProject.Components;
public class CategoriesComponent : ViewComponent
{

    private readonly ICategoryService _categoryService;
    public CategoriesComponent(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var categories=await _categoryService.ShowCategoriesAsync();
        return View("/Views/Components/CategoriesComponent.cshtml",categories);
    }
}