using Microsoft.AspNetCore.Mvc;
using OnePunchApi.Data.Model;
using OnePunchApi.Data.Repository;

namespace OnePunchApi.Controllers;

[ApiController]
[Route("[controller]")]
public class HeroesController : ControllerBase
{
    private readonly HeroesRepository _repo;

    public HeroesController(HeroesRepository repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Hero>> GetAll()
    {
        return Ok(_repo.GetAll());
    }

    [HttpGet("{id:int}")]
    public ActionResult<Hero?> Get([FromRoute] int id)
    {
        var hero = _repo.Get(id);
        if (hero is null)
            return NotFound();

        return Ok(hero);
    }

    // TODO: authorize actions.
    [HttpPost]
    public IActionResult Create([FromBody] Hero hero)
    {
        if (hero.Id != 0)
            return BadRequest("Hero must not have an Id");

        _repo.Create(hero);

        return CreatedAtAction(nameof(Create), new { hero.Id }, hero);
    }

    [HttpPut("{id:int}/rank")]
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
    public IActionResult ChangeStatus([FromRoute] int id, [FromBody] Status status)
    {
        var hero = _repo.Get(id);
        if (hero is null)
            return NotFound();

        _repo.ChangeStatus(hero, status);

        return NoContent();
    }
}