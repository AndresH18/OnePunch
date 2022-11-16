using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnePunch.Api.Data.Repository;
using OnePunch.Api.Security.Policies;
using Shared.Data.Model;

namespace OnePunch.Api.Controllers;

[ApiController]
[Route("heroes")]
public class HeroesController : ControllerBase
{
    private readonly HeroesRepository _repo;

    public HeroesController(HeroesRepository repo)
    {
        _repo = repo;
    }

    [HttpGet]
    [HttpGet("/")]
    public ActionResult<IEnumerable<Hero>> GetAll()
    {
        return Ok(_repo.GetAll());
    }

    [HttpGet("top")]
    public ActionResult<List<Hero>> GetTop([FromQuery] int limit = 10)
    {
        if (limit <= 0)
            return BadRequest();

        return Ok(_repo.GetTop(limit));
    }

    [HttpGet("{id:int}")]
    public ActionResult<Hero?> Get([FromRoute] int id)
    {
        var hero = _repo.Get(id);
        if (hero is null)
            return NotFound();

        return Ok(hero);
    }

    [HttpPost]
    [Authorize(Policy = PolicyConstants.Admin)]
    public IActionResult Create([FromBody] Hero hero)
    {
        if (hero.Id != 0)
            return BadRequest("Hero must not have an Id");

        _repo.Create(hero);

        return CreatedAtAction(nameof(Create), new {hero.Id}, hero);
    }

    [HttpPut("{id:int}/rank")]
    [Authorize(Policy = PolicyConstants.Admin)]
    public IActionResult UpdateRank([FromRoute] int id, [FromBody] Rank rank)
    {
        var hero = _repo.Get(id);
        if (hero is null)
            return BadRequest();

        _repo.ChangeRank(hero, rank);

        return Ok(rank);
    }

    // [HttpPut("{id:int}")]
    // public IActionResult UpdateHero(int id, Hero hero)
    // {
    //     
    // }
    [HttpDelete("{id:int}/status")]
    [Authorize(Policy = PolicyConstants.Admin)]
    public IActionResult ChangeStatus([FromRoute] int id, [FromBody] Status status)
    {
        var hero = _repo.Get(id);
        if (hero is null)
            return NotFound();

        _repo.ChangeStatus(hero, status);

        return NoContent();
    }
}