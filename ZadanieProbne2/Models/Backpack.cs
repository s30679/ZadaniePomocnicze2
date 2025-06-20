using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZadanieProbne2.Models;

public class Backpack
{
    [Key, Column(Order = 0)]
    public int CharacterId { get; set; }
    [Key, Column(Order = 1)]
    public int ItemId { get; set; }
    [Required]
    public int Amount { get; set; }
    
    [ForeignKey(nameof(CharacterId))]
    public Character Character { get; set; }
    
    [ForeignKey(nameof(ItemId))]
    public Item Item { get; set; }
}