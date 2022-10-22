using Microsoft.AspNetCore.Mvc;
using OnePunchApi.Data.Model;
using OnePunchApi.Data.Repository;

namespace OnePunchApi.Controllers;

[ApiController]
[Route("[Controller]")]
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
    public ActionResult<IEnumerable<Sponsor>> Get(int id)
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
    public IActionResult ChangeStatus(int id, [FromBody] Status status)
    {
        var sponsor = _repo.Get(id);
        if (sponsor is null)
            return NotFound();

        _repo.ChangeStatus(sponsor, status);

        return NoContent();
    }
}