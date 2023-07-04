namespace Services;

using System.Text.Json;
using Data.Context;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
public class SliderService : ISliderService
{
    private readonly AppDbContext _context;
    private readonly ILogService _logService;
    private readonly IServiceImage _serviceImage;

    public SliderService(AppDbContext context, ILogService log, IServiceImage serviceImage)
    {
        _context = context;
        _logService = log;
        _serviceImage = serviceImage;

    }
    public async Task<Slider> FindSliderByIdAsync(int id)
    {
        try
        {
            var res = await _context.Sliders.FirstOrDefaultAsync(x => x.Id == id);
            if (res != null)
                return res;
            else
                return new Slider();
        }
        catch (System.Exception ex)
        {
            var log = new Log()
            {
                Text = ex.Message,
                Service = "SliderService.FindSliderByIdAsync",
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now
            };
            _logService.AddLog(log, "log_Slider_Service.json");
            throw;
        }
    }
    public async Task<IEnumerable<Slider>> ShowSlidersAsync()
    {
        try
        {
            var result = await _context.Sliders.ToListAsync();
            return result;
        }
        catch (System.Exception ex)
        {
            var log = new Log()
            {
                Text = ex.Message,
                Service = "SliderService.ShowSliders",
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now
            };
            _logService.AddLog(log, "log_Slider_Service.json");
            throw;
        }

    }
    public async Task<Slider> AddSliderAsync(Slider Slider, IFormFile file)
    {
        try
        {
            Slider.CreateDate = DateTime.Now;
            Slider.UpdateDate = Slider.CreateDate;
            //SaveImage
            var pathImage = _serviceImage.AddImage(file);
            Slider.Image = pathImage;

            await _context.Sliders.AddAsync(Slider);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return Slider;
            }
            else
            {
                return new Slider();
            }

        }
        catch (System.Exception ex)
        {
            var log = new Log()
            {
                Text = ex.Message,
                Service = "SliderService.AddSliderAsync",
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now
            };
            _logService.AddLog(log, "log_Slider_Service.json");
            throw;
        }
    }
    public async Task<Slider> UpdateSliderAsync(Slider Slider, IFormFile file)
    {
        try
        {
            Slider.CreateDate = Slider.CreateDate;
            Slider.UpdateDate = DateTime.Now;
            //SaveImage
            if (file != null)
            {
                var pathImage = _serviceImage.AddImage(file);
                Slider.Image = pathImage;
            }
            


            _context.Sliders.Update(Slider);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return Slider;
            }
            else
            {
                return new Slider();
            }

        }
        catch (System.Exception ex)
        {
            var log = new Log()
            {
                Text = ex.Message,
                Service = "SliderService.UpdateSliderAsync",
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now
            };
            _logService.AddLog(log, "log_Slider_Service.json");
            throw;
        }
    }

    public async Task<bool> DeleteSliderByIdAsync(int id)
    {
        try
        {
            var Slider = await FindSliderByIdAsync(id);
            _context.Sliders.Remove(Slider);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
                return true;
            else
                return false;


        }
        catch (System.Exception ex)
        {
            var log = new Log()
            {
                Text = ex.Message,
                Service = "SliderService.DeleteSliderByIdAsync",
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now
            };
            _logService.AddLog(log, "log_Slider_Service.json");
            return false;
        }
    }


}