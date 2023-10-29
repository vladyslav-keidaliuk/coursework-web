using System.ComponentModel.DataAnnotations;

namespace CourseWorkWeb.Models;

public class Category
{
    [Key] // Primary key
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    public int DisplayOrder { get; set; }
}