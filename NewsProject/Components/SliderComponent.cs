using Microsoft.AspNetCore.Mvc;
using Services;

namespace NewsProject.Components;
public class SliderComponent : ViewComponent
{

    private readonly ISliderService _sliderService;
    public SliderComponent(ISliderService sliderService)
    {
        _sliderService = sliderService;
    }
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var sliders=await _sliderService.ShowSlidersAsync();
        return View("/Views/Components/SliderComponent.cshtml",sliders);
    }
}