using System.ComponentModel.DataAnnotations;

namespace socials.DBContext.Models;

public class PageInfoModel
{
    [Range(0, int.MaxValue, ErrorMessage = "Size must be positive")]
    public Int32 Size { get; set; }
    [Range(0, int.MaxValue, ErrorMessage = "Count must be positive")]
    public Int32 Count { get; set; }
    [Range(0, int.MaxValue, ErrorMessage = "Current must be positive")]
    public Int32 Current { get; set; }
}