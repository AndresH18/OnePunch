using Microsoft.AspNetCore.Mvc;
using OnePunchApi.Data.Repository;

namespace OnePunchApi.Controllers;

[ApiController]
[Route("[Controller]")]
public abstract class AbstractController<T> : ControllerBase
{
    // ReSharper disable once MemberCanBePrivate.Global
    protected readonly IRepository<T> _repo;

    protected AbstractController(IRepository<T> repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public virtual ActionResult<IEnumerable<T>> GetAll()
    {
        return Ok(_repo.GetAll());
    }

    [HttpGet("{id:int}")]
    public virtual ActionResult<T?> Get(int id)
    {
        var t = _repo.Get(id);
        if (t is null)
            return NotFound();

        return Ok(t);
    }

    // TODO: authorize actions.
    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var t = _repo.Get(id);
        if (t is null)
            return NotFound();

        _repo.Delete(t);

        return NoContent();
    }

    [HttpPost]
    public abstract IActionResult Create([FromBody] T t);
}