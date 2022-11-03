using Microsoft.AspNetCore.Mvc;
using OnePunchApi.Data.Repository;
using Shared.Data.Model;

namespace OnePunchApi.Controllers;

[ApiController]
[Route("fights")]
public class FightsController : ControllerBase
{
    private readonly FightsRepository _repo;

    public FightsController(FightsRepository repo)
    {
        _repo = repo;
    }

    [HttpGet(Name = "GetAllFights")]
    [HttpGet("{id:int?}", Name = "GetFight")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<IEnumerable<Fight>> Get(int? id = null)
    {
        if (id <= 0)
            return BadRequest();

        var fights = id.HasValue ? _repo.Get(id.Value) : _repo.GetAll();
        return fights.Any() ? Ok(fights) : NotFound();
    }
}