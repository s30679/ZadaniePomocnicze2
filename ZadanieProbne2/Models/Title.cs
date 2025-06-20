using System.ComponentModel.DataAnnotations;

namespace ZadanieProbne2.Models;

public class Title
{
    [Key]
    public int TitleId { get; set; }
    
    [Required, MaxLength(100)]
    public string Name { get; set; }
    
    public ICollection<Character_Title> CharacterTitles { get; set; }
}