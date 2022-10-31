using Microsoft.AspNetCore.Mvc;
using OnePunchApi.Data.Model;
using OnePunchApi.Data.Repository;

namespace OnePunchApi.Controllers;

[ApiController]
[Route("/sponsors")]
public class SponsorsController : ControllerBase
{
    private readonly SponsorRepository _repo;

    public SponsorsController(SponsorRepository repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Sponsor>> GetAll()
    {
        return Ok(_repo.GetAll());
    }

    [HttpGet("{id:int}")]
    public ActionResult<IEnumerable<Sponsor>> Get([FromRoute] int id)
    {
        var sponsor = _repo.Get(id);
        if (sponsor is null)
            return NotFound();

        return Ok(sponsor);
    }

    // TODO: Authorize Actions
    [HttpPost]
    public IActionResult Create([FromBody] Sponsor sponsor)
    {
        if (sponsor.Id != 0)
            return BadRequest("Monster must no have an Id");

        _repo.Create(sponsor);
        return Ok(sponsor);
    }

    [HttpDelete("{id:int}/status")]
    public IActionResult ChangeStatus([FromRoute] int id, [FromBody] Status status)
    {
        var sponsor = _repo.Get(id);
        if (sponsor is null)
            return NotFound();

        _repo.ChangeStatus(sponsor, status);

        return NoContent();
    }

    [HttpPut("{id:int}/hero/{heroId:int}")]
    public IActionResult AddHero([FromRoute] int id, [FromRoute] int heroId)
    {
        var sponsor = _repo.Get(id);
        if (sponsor is null)
            return NotFound();

        var hero = _repo.AddHero(sponsor, heroId);
        if (hero is null)
            return NotFound();

        return Ok(hero.Id);
    }

    [HttpDelete("{id:int}/hero/{heroId:int}")]
    public IActionResult RemoveHero([FromRoute] int id, [FromRoute] int heroId)
    {
        var sponsor = _repo.Get(id);
        if (sponsor is null)
            return NotFound();

        _repo.RemoveHero(sponsor, heroId);
        return NoContent();
    }

    [HttpPut("{id:int}/monster/{monsterId:int}")]
    public IActionResult AddMonster([FromRoute] int id, [FromRoute] int monsterId)
    {
        var sponsor = _repo.Get(id);
        if (sponsor is null)
            return NotFound();

        var monster = _repo.AddMonster(sponsor, monsterId);
        if (monster is null)
            return NotFound();

        return Ok(monster.Id);
    }

    [HttpDelete("{id:int}/monster/{monsterId:int}")]
    public IActionResult RemoveMonster([FromRoute] int id, [FromRoute] int monsterId)
    {
        var sponsor = _repo.Get(id);
        if (sponsor is null)
            return NotFound();

        _repo.RemoveMonster(sponsor, monsterId);
        return NoContent();
    }
}