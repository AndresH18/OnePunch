using Microsoft.AspNetCore.Mvc;
using OnePunchApi.Data.Model;
using OnePunchApi.Data.Repository;

namespace OnePunchApi.Controllers;

[ApiController]
[Route("[Controller]")]
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

    [HttpGet]
    public ActionResult<Monster?> Get(int id)
    {
        var monster = _repo.Get(id);
        if (monster is null)
            return NotFound();

        return Ok(monster);
    }

}