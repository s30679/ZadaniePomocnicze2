using Microsoft.AspNetCore.Mvc;
using ZadanieProbne2.Models;
using ZadanieProbne2.Services;

namespace ZadanieProbne2.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CharacterController : ControllerBase
{
    private readonly ICharacterService _characterService;

    public CharacterController(ICharacterService characterService)
    {
        _characterService = characterService;
    }

    [HttpGet("{Id}")]
    public async Task<IActionResult> GetCharacterByIdAsync(int Id, CancellationToken cancellationToken)
    {
        var character = await _characterService.GetCharacterAsync(Id, cancellationToken);
        if (character == null)
        {
            return NotFound();
        }
        return Ok(character);
    }

    [HttpPost("{Id}/backpacks")]
    public async Task<IActionResult> AddItemToBackpackAsync(int Id, [FromBody] List<int> items, CancellationToken cancellationToken)
    {
        var wynik = await _characterService.AddItemsToBackpackAsync(Id, items, cancellationToken);
        if (!wynik.Success)
        {
            return BadRequest(wynik.odp);
        }
        return NoContent();
    }
}