using System.ComponentModel.DataAnnotations;

namespace Entities.Models;
public class Common
{
    [Key]
    public int Id { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime UpdateDate { get; set; }

}