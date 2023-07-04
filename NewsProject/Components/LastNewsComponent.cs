using Microsoft.AspNetCore.Mvc;
using Services;

namespace NewsProject.Components;
public class LastNewsComponent : ViewComponent
{

    private readonly INewsService _newsService;
    public LastNewsComponent(INewsService newsService)
    {
        _newsService = newsService;
    }
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var news=await _newsService.ShowNewssAsync();
        return View("/Views/Components/LastNewsComponent.cshtml",news);
    }
}