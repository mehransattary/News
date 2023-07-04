namespace Services;
using Entities.Models;
public interface ICategoryService
{
    Task<IEnumerable<Category>> ShowCategoriesAsync();
    Task<Category> FindCategoryByIdAsync(int id);

    Task<Category> AddCategoryAsync(Category category);
    Task<Category> UpdateCategoryAsync(Category category);
    Task<bool> DeleteCategoryByIdAsync(int id);

}

