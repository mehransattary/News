namespace Services;
using Entities.Models;
using Microsoft.AspNetCore.Http;

public interface ISliderService
{
    Task<IEnumerable<Slider>> ShowSlidersAsync();
    Task<Slider> FindSliderByIdAsync(int id);

    Task<Slider> AddSliderAsync(Slider Slider, IFormFile file);
    Task<Slider> UpdateSliderAsync(Slider Slider, IFormFile file);
    Task<bool> DeleteSliderByIdAsync(int id);
}