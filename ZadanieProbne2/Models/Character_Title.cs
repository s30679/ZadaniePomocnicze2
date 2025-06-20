using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZadanieProbne2.Models;

public class Character_Title
{
    [Key, Column(Order = 0)]
    public int CharacterId { get; set; }
    [Key, Column(Order = 1)]
    public int TitleId { get; set; }
    [Required]
    public DateTime AcquiredAt { get; set; }
    
    [ForeignKey(nameof(CharacterId))]
    public Character Character { get; set; }
    
    [ForeignKey(nameof(TitleId))]
    public Title Title { get; set; }
}