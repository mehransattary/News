using System.ComponentModel.DataAnnotations;

namespace Entities.Models;
public class Log : Common
{
    //*********************************//
    [Required]
    [MaxLength(250)]
    public string Text { get; set; }
    //*********************************//
    [MaxLength(250)]
    public string? Service { get; set; }
    //*********************************//
    [MaxLength(250)]
    public string? Action { get; set; }
    //*********************************//
}