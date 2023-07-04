using System.ComponentModel.DataAnnotations;

namespace Entities.Models;
public class Role : Common
{
    //*********************************//
    [Required]
    [MaxLength(150)]
    [Display(Name = "نام انگلیسی")]
    public string EnglishName { get; set; }
    //*********************************//
    [Required]
    [MaxLength(150)]
    [Display(Name = "نام فارسی")]
    public string PersianName { get; set; }
    //*********************************//
    public ICollection<User>? Users { get; set; }
    //*********************************//
}