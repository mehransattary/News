using System.ComponentModel.DataAnnotations;

namespace Entities.Models;
public class User : Common
{
    //*********************************//
    [MaxLength(250)]
    [Display(Name = "نام کاربر")]
    public string? Fullname { get; set; }
    //*********************************//
    [Required]
    [MaxLength(11)]
    [Display(Name = "موبایل ")]
    public string Mobile { get; set; }
    //*********************************//
    [Required]
    [MaxLength(15)]
    [Display(Name = "رمز عبور ")]
    public string Password { get; set; }
    //*********************************//
    [Required]
    [Display(Name = "نقش کاربر  ")]
    public int RoleId { get; set; }
    //*********************************//
    public Role? Role { get; set; }
}