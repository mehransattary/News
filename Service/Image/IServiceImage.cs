namespace Services;
using Entities.Models;
using Microsoft.AspNetCore.Http;
public interface IServiceImage
{
    string AddImage(IFormFile fromFile);

}