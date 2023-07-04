namespace Services;
using Entities.Models;
using Microsoft.AspNetCore.Http;

public interface INewsService
{
    Task<IEnumerable<News>> ShowNewssAsync();
    Task<News> FindNewsByIdAsync(int id);

    Task<News> AddNewsAsync(News News, IFormFile file);
    Task<News> UpdateNewsAsync(News News, IFormFile file);
    Task<bool> DeleteNewsByIdAsync(int id);
}