namespace Services;

using Data.Context;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

public class CategoryService : ICategoryService
{
    private readonly ILogService _logService;
    private readonly AppDbContext _context;
    public CategoryService(AppDbContext context, ILogService logService)
    {
        _context = context;
        _logService = logService;
    }
    public async Task<Category> FindCategoryByIdAsync(int id)
    {
        try
        {
            var res = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (res != null)
                return res;
            else
                return new Category();
        }
        catch (System.Exception ex)
        {
            var log = new Log()
            {
                Text = ex.Message,
                Service = "CategoryService.FindCategoryByIdAsync",
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now
            };
            _logService.AddLog(log, "log_Category_Service.json");

            throw;
        }
    }
    public async Task<IEnumerable<Category>> ShowCategoriesAsync()
    {
        try
        {
            var result = await _context.Categories.ToListAsync();
            return result;
        }
        catch (System.Exception ex)
        {
            var log = new Log()
            {
                Text = ex.Message,
                Service = "CategoryService.ShowCategories",
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now
            };
            _logService.AddLog(log, "log_Category_Service.json");

            throw;
        }

    }
    public async Task<Category> AddCategoryAsync(Category category)
    {
        try
        {
            category.CreateDate = DateTime.Now;
            category.UpdateDate = category.CreateDate;
            await _context.Categories.AddAsync(category);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return category;
            }
            else
            {
                return new Category();
            }

        }
        catch (System.Exception ex)
        {
            var log = new Log()
            {
                Text = ex.Message,
                Service = "CategoryService.AddCategoryAsync",
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now
            };
            _logService.AddLog(log, "log_Category_Service.json");

            throw;
        }
    }
    public async Task<Category> UpdateCategoryAsync(Category category)
    {
        try
        {
            category.CreateDate = category.CreateDate;
            category.UpdateDate = DateTime.Now;
            _context.Categories.Update(category);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return category;
            }
            else
            {
                return new Category();
            }

        }
        catch (System.Exception ex)
        {
            var log = new Log()
            {
                Text = ex.Message,
                Service = "CategoryService.UpdateCategoryAsync",
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now
            };
            _logService.AddLog(log, "log_Category_Service.json");

            throw;
        }
    }

    public async Task<bool> DeleteCategoryByIdAsync(int id)
    {
        try
        {
            var category = await FindCategoryByIdAsync(id);
            _context.Categories.Remove(category);
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
                Service = "CategoryService.DeleteCategoryByIdAsync",
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now
            };
            _logService.AddLog(log, "log_Category_Service.json");

            throw;
        }
    }



}