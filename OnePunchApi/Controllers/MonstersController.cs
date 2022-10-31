using System.Collections.Specialized;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using OnePunchApi.Data.Model;
using OnePunchApi.Data.Repository;

namespace OnePunchApi.Controllers;

[ApiController]
[Route("monsters")]
public class MonstersController : ControllerBase
{
    private readonly MonsterRepository _repo;

    public MonstersController(MonsterRepository repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Monster>> GetAll()
    {
        return Ok(_repo.GetAll());
    }

    [HttpGet("{id:int}")]
    public ActionResult<Monster?> Get([FromRoute] int id)
    {
        var monster = _repo.Get(id);
        if (monster is null)
            return NotFound();

        return Ok(monster);
    }

    // TODO: Authorize Actions
    [HttpPost]
    public IActionResult Create([FromBody] Monster monster)
    {
        if (monster.Id != 0)
            return BadRequest("Monster must not have an Id");

        _repo.Create(monster);

        return CreatedAtAction(nameof(Create), new {monster.Id}, monster);
    }

    [HttpPut("{id:int}/disaster")]
    public IActionResult UpdateDisaster([FromRoute] int id, [FromBody] DisasterLevel disasterLevel)
    {
        var monster = _repo.Get(id);
        if (monster is null)
            return BadRequest();

        _repo.ChangeDisaster(monster, disasterLevel);

        return Ok(disasterLevel);
    }

    [HttpDelete("{id:int}/status")]
    public IActionResult ChangeStatus([FromRoute] int id, [FromBody] Status status)
    {
        var monster = _repo.Get(id);
        if (monster is null)
            return NotFound();

        _repo.ChangeStatus(monster, status);

        return NoContent();
    }
}