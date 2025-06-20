using Microsoft.EntityFrameworkCore;
using ZadanieProbne2.DAL;
using ZadanieProbne2.DTOs;
using ZadanieProbne2.Models;

namespace ZadanieProbne2.Services;

public class CharacterService : ICharacterService
{
    private readonly CharacterDbContext _dbcontext;

    public CharacterService(CharacterDbContext dbcontext)
    {
        _dbcontext = dbcontext;
    }

    public async Task<GetCharacterDTO?> GetCharacterAsync(int Id, CancellationToken cancellationToken)
    {
        var character = await _dbcontext.Characters
            .Include(b => b.Backpacks).ThenInclude(i => i.Item)
            .Include(ct => ct.CharacterTitles).ThenInclude(t => t.Title)
            .FirstOrDefaultAsync(ch=>ch.CharacterId == Id, cancellationToken);
        
        if (character == null)
        { 
            return null;
        }

        return new GetCharacterDTO
        {
            firstName = character.FirstName,
            lastName = character.LastName,
            currentWeight = character.CurrentWeight,
            maxWeight = character.MaxWeight,
            Backpack = character.Backpacks.Select(b => new BackpackDTO
            {
                itemName = b.Item.Name,
                itemWeight = b.Item.Weight,
                amount = b.Amount
            }).ToList(),
            Titles = character.CharacterTitles.Select(ct => new TitleDTO
            {
                title = ct.Title.Name,
                acquiredAt = ct.AcquiredAt
            }).ToList()
        };
    }

    public async Task<(bool Success, string? odp)> AddItemsToBackpackAsync(int Id, List<int> items, CancellationToken cancellationToken)
    {
        if (items.Count == 0 || items == null)
        {
            return (false, "Lista elementów jest pusta");
        }
        
        var character = await _dbcontext.Characters
            .Include(b => b.Backpacks)
            .FirstOrDefaultAsync(ch => ch.CharacterId == Id, cancellationToken);

        if (character == null)
        {
            return (false, "Nie ma postaci o takim id");
        }
        
        var przedmioty = await _dbcontext.Items
            .Where(it => items.Contains(it.ItemId))
            .ToListAsync(cancellationToken);

        if (przedmioty.Count != items.Count)
        {
            return (false, "Niektóre z przedmiotów z listy nie istnieją");
        }
        
        int waga = przedmioty.Sum(i => i.Weight);

        if (character.CurrentWeight + waga > character.MaxWeight)
        {
            return (false, "Przekroczono wagę dla postaci");
        }

        foreach (var przedmiot in przedmioty)
        {
            var dodawany = character.Backpacks.FirstOrDefault(b=>b.ItemId == przedmiot.ItemId);
            if (dodawany == null)
            {
                _dbcontext.Backpacks.Add( new Backpack
                {
                    CharacterId = Id,
                    ItemId = przedmiot.ItemId,
                    Amount = 1
                });
            }
            else
            {
                dodawany.Amount += 1;
            }
        }
        
        character.CurrentWeight += waga;
        
        await _dbcontext.SaveChangesAsync(cancellationToken);
        return (true, null);
    }
}