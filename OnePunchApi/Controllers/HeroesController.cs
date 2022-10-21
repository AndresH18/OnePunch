using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnePunchApi.Data;
using OnePunchApi.Data.Model;

namespace OnePunchApi.Controllers;

[ApiController]
[Route("[Controller]")]
public class HeroesController : ControllerBase
{
    private readonly AssociationDb _db;

    public HeroesController(AssociationDb db)
    {
        _db = db;
    }

    [HttpGet]
    public ActionResult<List<Hero>> GetAll()
    {
        return Ok(_db.Heroes.AsNoTracking().ToList());
    }

    [HttpGet("{id:int}")]
    public ActionResult<Hero?> Get(int id)
    {
        var hero = _db.Heroes.FirstOrDefault(h => h.Id == id);
        if (hero is null)
            return NotFound();

        return Ok(hero);
    }

    // TODO: authorize actions.
    [HttpPost]
    public IActionResult CreateHero([FromBody] Hero hero)
    {
        if (hero.Id != 0)
            return BadRequest();
        
        _db.Heroes.Add(hero);
        _db.SaveChanges();
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
        var hero = _db.Heroes.FirstOrDefault(h => h.Id == id);
        if (hero is null)
            return NotFound();

        _db.Heroes.Remove(hero);
        _db.SaveChanges();
        return NoContent();
    }
}