using System.ComponentModel.DataAnnotations;

namespace ZadanieProbne2.Models;

public class Item
{
    [Key]
    public int ItemId { get; set; }
    
    [Required, MaxLength(100)]
    public string Name { get; set; }
    
    [Required]
    public int Weight { get; set; }
    
    public ICollection<Backpack> Backpacks { get; set; }
}