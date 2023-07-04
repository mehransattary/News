namespace Services;

using System.Text.Json;
using Data.Context;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

public class ServiceImage : IServiceImage
{
    public string AddImage(IFormFile fromFile)
    {
        if (fromFile != null)
        {
            string fileName = fromFile.FileName;
            string pathCurrent = Directory.GetCurrentDirectory();
            string customerPath = Path.Combine(pathCurrent, "wwwroot", "images", fileName);
           
            using( var fileStream = new FileStream(customerPath, FileMode.Create))
            {
               fromFile.CopyTo(fileStream);
            }
            return $"/images/{fileName}";
        }
        return "";
    }
}