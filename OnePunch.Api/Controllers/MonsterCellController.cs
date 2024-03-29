﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnePunch.Api.Data.Repository;
using OnePunch.Api.Security.Policies;
using Shared.Data.Model;

namespace OnePunch.Api.Controllers;

[ApiController]
[Route("/monster-cells")]
[Authorize(Policy = PolicyConstants.HeroS)]
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
    public IActionResult Register([FromBody] MonsterCell monsterCell)
    {
        if (monsterCell.Id != 0)
            return BadRequest();

        monsterCell.Monster = null;
        monsterCell.MonsterId = 0;

        _repo.Register(monsterCell);

        return CreatedAtAction(nameof(Register), new { monsterCell.Id }, monsterCell);
    }

    [HttpPost("monster/{monsterId:int:min(1)}")]
    public IActionResult Register([FromBody] MonsterCell monsterCell, [FromRoute] int monsterId)
    {
        if (monsterCell.Id != 0)
            return BadRequest();

        if (monsterId >= 0)
        {
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

        return CreatedAtAction(nameof(Register), new { monsterCell.Id }, monsterCell);
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