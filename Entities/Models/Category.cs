using System.ComponentModel.DataAnnotations;

namespace Entities.Models;
public class Category:Common
{
//*********************************//
    [Required]
    [MaxLength(250)]
    public string Name { get; set; }
    //*********************************//
     public ICollection<News>? News { get; set; }
       
}