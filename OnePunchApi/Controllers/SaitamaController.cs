using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnePunchApi.Data.Model;
using OnePunchApi.Data.Repository;

namespace OnePunchApi.Controllers;

[ApiController]
[Route("[controller]")]
// [Authorize(Policy = "Saitama")]
public class SaitamaController : ControllerBase
{
    private readonly SaitamaRepository _repo;

    public SaitamaController(SaitamaRepository repo)
    {
        _repo = repo;
    }

    [HttpGet("games/favorites")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<Game>> GetAllGames()
    {
        return Ok(_repo.GetFavoriteGames());
    }

    [HttpGet("games/favorites/{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<Game?> GetGame(int id)
    {
        if (id <= 0)
            return BadRequest();

        var game = _repo.GetFavoriteGame(id);
        if (game is null)
            return NotFound();

        return Ok(game);
    }
}