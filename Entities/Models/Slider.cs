using System.ComponentModel.DataAnnotations;

namespace Entities.Models;
public class Slider:Common
{
    //*********************************//   
    [MaxLength(250)]
    [Display(Name ="نام اسلایدر")]
    public string? Name { get; set; }
    //*********************************//    
    [MaxLength(1500)]    
    public string? Image { get; set; }
    //*********************************//
}