using ZadanieProbne2.Models;

namespace ZadanieProbne2.DTOs;

public class GetCharacterDTO
{
    public string firstName { get; set; }
    public string lastName { get; set; }
    public int currentWeight { get; set; }
    public int maxWeight { get; set; }
    public List<BackpackDTO> Backpack { get; set; }
    public List<TitleDTO> Titles { get; set; }
}