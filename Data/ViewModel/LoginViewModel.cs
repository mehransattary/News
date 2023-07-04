using System.ComponentModel.DataAnnotations;

namespace Data.ViewModel;
public class LoginViewModel
{
    [Required]
    [MaxLength(11)]
    [Display(Name = "موبایل ")]
    [DataType(DataType.PhoneNumber)]
    public string Mobile { get; set; }
    //*********************************//
    [Required]
    [MaxLength(15)]
    [Display(Name = "رمز عبور ")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    //*********************************//
}