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
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<Fight>> GetAll()
    {
        return Ok(_repo.GetAll());
    }

    [HttpGet("{id:int:min(1)}", Name = "GetFight")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<IEnumerable<Fight>> Get(int id)
    {
        if (id <= 0)
            return BadRequest("Invalid Id");


        var fights = _repo.Get(id);

        return fights.Any() ? Ok(fights) : NotFound();
    }
}