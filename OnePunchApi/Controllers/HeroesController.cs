using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnePunchApi.Data;
using OnePunchApi.Data.Model;
using OnePunchApi.Data.Repository;

namespace OnePunchApi.Controllers;

[ApiController]
[Route("[Controller]")]
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
    public ActionResult<Hero?> Get(int id)
    {
        var hero = _repo.Get(id);
        if (hero is null)
            return NotFound();

        return Ok(hero);
    }

    // TODO: authorize actions.
    [HttpPost]
    public IActionResult CreateHero([FromBody] Hero hero)
    {
        if (hero.Id != 0)
            return BadRequest("Hero must not have an Id");

        _repo.Create(hero);

        return CreatedAtAction(nameof(CreateHero), new {hero.Id}, hero);
    }

    // [HttpPut("{id:int}")]
    // public IActionResult UpdateHero(int id, Hero hero)
    // {
    //     
    // }
    [HttpDelete("{id:int}")]
    public IActionResult DeleteHero(int id)
    {
        var hero = _repo.Get(id);
        if (hero is null)
            return NotFound();

        _repo.Delete(hero);
        
        return NoContent();
    }
}