using Microsoft.AspNetCore.Mvc;
using OnePunchApi.Data.Model;
using OnePunchApi.Data.Repository;

namespace OnePunchApi.Controllers;

[ApiController]
[Route("[controller]")]
// [MyAuthentication]
public class MonsterCellController : ControllerBase
{
    private readonly MonsterCellRepository _repo;

    public MonsterCellController(MonsterCellRepository repo)
    {
        _repo = repo;
    }

    [HttpGet(Name = "GetMonsterCells")]
    public ActionResult<IEnumerable<MonsterCell>> GetAll()
    {
        return Ok(_repo.GetAll());
    }

    [HttpGet("{id:int}")]
    public ActionResult<MonsterCell?> Get([FromRoute] int id)
    {
        var mc = _repo.Get(id);
        if (mc is null)
            return NotFound();

        return Ok(mc);
    }

    [HttpPost]
    [HttpPost("monster/{monsterId:int}")]
    public IActionResult Register([Microsoft.AspNetCore.Mvc.FromBody] MonsterCell monsterCell,
        [FromRoute] int monsterId = 0)
    {
        if (monsterCell.Id != 0)
            return BadRequest();

        if (monsterId >= 0)
        {
            // TODO : fix nullable

            var r = _repo.Register(monsterCell, monsterId);
            if (r is null)
            {
                return BadRequest();
            }
        }
        else
        {
            return BadRequest();
        }

        return CreatedAtAction(nameof(Register), new {monsterCell.Id}, monsterCell);
    }

    [HttpPut("{id:int}/monster/{monsterId:int}")]
    public IActionResult ChangeStatus([FromRoute] int id, [FromRoute] int monsterId)
    {
        if (id <= 0)
            return BadRequest();

        var mc = _repo.Get(id);
        if (mc is null)
            return NotFound();

        mc = _repo.ChangeStatus(mc, monsterId);
        if (mc is null)
            return BadRequest();

        return NoContent();
    }
}