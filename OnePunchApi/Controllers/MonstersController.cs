using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnePunchApi.Data.Repository;
using OnePunchApi.Security.Policies;
using Shared.Data.Model;

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

    [HttpPost]
    [Authorize(Policy = PolicyConstants.Admin)]
    public IActionResult Create([FromBody] Monster monster)
    {
        if (monster.Id != 0)
            return BadRequest("Monster must not have an Id");

        _repo.Create(monster);

        return CreatedAtAction(nameof(Create), new { monster.Id }, monster);
    }

    [HttpPut("{id:int}/disaster")]
    [Authorize(Policy = PolicyConstants.Admin)]
    public IActionResult UpdateDisaster([FromRoute] int id, [FromBody] DisasterLevel disasterLevel)
    {
        var monster = _repo.Get(id);
        if (monster is null)
            return BadRequest();

        _repo.ChangeDisaster(monster, disasterLevel);

        return Ok(disasterLevel);
    }

    [HttpDelete("{id:int}/status")]
    [Authorize(Policy = PolicyConstants.Admin)]
    public IActionResult ChangeStatus([FromRoute] int id, [FromBody] Status status)
    {
        var monster = _repo.Get(id);
        if (monster is null)
            return NotFound();

        _repo.ChangeStatus(monster, status);

        return NoContent();
    }
}