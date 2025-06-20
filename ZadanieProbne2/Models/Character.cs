using System.ComponentModel.DataAnnotations;

namespace ZadanieProbne2.Models;

public class Character
{
    [Key]
    public int CharacterId { get; set; }
    
    [Required, MaxLength(50)]
    public string FirstName { get; set; }
    
    [Required, MaxLength(120)]
    public string LastName { get; set; }
    
    [Required]
    public int CurrentWeight { get; set; }
    
    [Required]
    public int MaxWeight { get; set; }
    
    public ICollection<Backpack> Backpacks { get; set; }
    public ICollection<Character_Title> CharacterTitles { get; set; }
}