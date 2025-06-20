using ZadanieProbne2.DTOs;
using ZadanieProbne2.Models;

namespace ZadanieProbne2.Services;

public interface ICharacterService
{
    Task<GetCharacterDTO?> GetCharacterAsync(int Id, CancellationToken cancellationToken);
    Task<(bool Success, string? odp)> AddItemsToBackpackAsync(int Id, List<int>items,CancellationToken cancellationToken);
}