namespace Services;

using System.Text.Json;
using Data.Context;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
public class NewService : INewsService
{
    private readonly AppDbContext _context;
    private readonly ILogService _logService;
    private readonly IServiceImage _serviceImage;

    public NewService(AppDbContext context, ILogService log, IServiceImage serviceImage)
    {
        _context = context;
        _logService = log;
        _serviceImage = serviceImage;

    }
    public async Task<News> FindNewsByIdAsync(int id)
    {
        try
        {
            var res = await _context.News.FirstOrDefaultAsync(x => x.Id == id);
            if (res != null)
                return res;
            else
                return new News();
        }
        catch (System.Exception ex)
        {
            var log = new Log()
            {
                Text = ex.Message,
                Service = "Newservice.FindNewsByIdAsync",
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now
            };
            _logService.AddLog(log, "log_News_Service.json");
            throw;
        }
    }
    public async Task<IEnumerable<News>> ShowNewssAsync()
    {
        try
        {
            var result = await _context.News.Include(x => x.Category).Select(x => new News()
            {
                  Name=x.Name,
                  Image=x.Image,
                  Id=x.Id,
                  Category=new Category()
                  {
                    Name=x.Category.Name
                  }
            }).ToListAsync();
            return result;
        }
        catch (System.Exception ex)
        {
            var log = new Log()
            {
                Text = ex.Message,
                Service = "Newservice.ShowNews",
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now
            };
            _logService.AddLog(log, "log_News_Service.json");
            throw;
        }

    }
    public async Task<News> AddNewsAsync(News News, IFormFile file)
    {
        try
        {
            News.CreateDate = DateTime.Now;
            News.UpdateDate = News.CreateDate;
            //SaveImage
            var pathImage = _serviceImage.AddImage(file);
            News.Image = pathImage;

            await _context.News.AddAsync(News);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return News;
            }
            else
            {
                return new News();
            }

        }
        catch (System.Exception ex)
        {
            var log = new Log()
            {
                Text = ex.Message,
                Service = "Newservice.AddNewsAsync",
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now
            };
            _logService.AddLog(log, "log_News_Service.json");
            throw;
        }
    }
    public async Task<News> UpdateNewsAsync(News News, IFormFile file)
    {
        try
        {
            News.CreateDate = News.CreateDate;
            News.UpdateDate = DateTime.Now;
            //SaveImage
            if (file != null)
            {
                var pathImage = _serviceImage.AddImage(file);
                News.Image = pathImage;
            }



            _context.News.Update(News);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return News;
            }
            else
            {
                return new News();
            }

        }
        catch (System.Exception ex)
        {
            var log = new Log()
            {
                Text = ex.Message,
                Service = "Newservice.UpdateNewsAsync",
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now
            };
            _logService.AddLog(log, "log_News_Service.json");
            throw;
        }
    }

    public async Task<bool> DeleteNewsByIdAsync(int id)
    {
        try
        {
            var News = await FindNewsByIdAsync(id);
            _context.News.Remove(News);
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
                Service = "Newservice.DeleteNewsByIdAsync",
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now
            };
            _logService.AddLog(log, "log_News_Service.json");
            return false;
        }
    }

  
}