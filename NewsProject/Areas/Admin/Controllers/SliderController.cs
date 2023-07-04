using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace NewsProject.Areas.Admin;
[Area("Admin")]
[Authorize("Admin")]
public class SliderController : Controller
{
    #region Constructor
    private readonly ISliderService _sliderService;

    public SliderController(ISliderService SliderService)
    {
        _sliderService = SliderService;
    }
    #endregion
    #region Index
    public async Task<IActionResult> Index()
    {
        var res = await _sliderService.ShowSlidersAsync();
        return View(res);
    }
    #endregion
    #region Create
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(Slider Slider,IFormFile file)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError("EnglishName", "لطفا ورودی را درست وارد نمائید.");
            return View(Slider);
        }
        var res = await _sliderService.AddSliderAsync(Slider,file);
        if (res.Id!=0)
        {
            return RedirectToAction(nameof(Index));
        }
        return View(Slider);
    }
    #endregion
    #region Edit
    public async Task<IActionResult> Edit(int id)
    {
        var res = await _sliderService.FindSliderByIdAsync(id);
        return View(res);
    }
    [HttpPost]
    public async Task<IActionResult> Edit(Slider Slider,IFormFile? file)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError("EnglishName", "لطفا ورودی را درست وارد نمائید.");
            return View(Slider);
        }
        var res = await _sliderService.UpdateSliderAsync(Slider,file);
        if (res.Id != 0)
        {
            return RedirectToAction(nameof(Index));
        }
       return View(Slider);
    }
    #endregion
    #region Delete
    public async Task<IActionResult> Delete(int id)
    {

         var res = await _sliderService.FindSliderByIdAsync(id);
        return View(res);
    }
    [HttpPost]
    public async Task<IActionResult> Delete(Slider Slider)
    {
        if (Slider.Id==0)      
            return View(Slider);
        
        var res = await _sliderService.DeleteSliderByIdAsync(Slider.Id);
        if (res)       
            return RedirectToAction(nameof(Index));
        
       return View(Slider);
    }
    #endregion

}