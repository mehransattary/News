using System.ComponentModel.DataAnnotations;

namespace Entities.Models;
public class News : Common
{
    //*********************************//
    [Required]
    [MaxLength(250)]
    [Display(Name = "نام خبر")]
    public string Name { get; set; }
    //*********************************//
    [Required]
    [Display(Name = "متن خبر")]
    [DataType(DataType.MultilineText)]
    public string Text { get; set; }
    //*********************************//
    [MaxLength(1500)]
    [Display(Name = "تصویر خبر")]
    public string? Image { get; set; }
    //*********************************//  
    [Display(Name = "دسته خبر")]
    public int CategoryId { get; set; }
    //*********************************//
    public Category? Category { get; set; }
}