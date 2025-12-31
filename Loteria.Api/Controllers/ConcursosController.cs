using Loteria.Application.Interfaces;
using Loteria.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Loteria.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ConcursosController : ControllerBase
{
    private readonly IConcursoRepository _repository;

    public ConcursosController(IConcursoRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Concurso>>> Get()
    {
        return Ok(await _repository.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Concurso>> Get(int id)
    {
        var concurso = await _repository.GetByIdAsync(id);
        if (concurso == null) return NotFound();
        return Ok(concurso);
    }

    [HttpPost]
    public async Task<ActionResult<Concurso>> Post(Concurso concurso)
    {
        var created = await _repository.CreateAsync(concurso);
        return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, Concurso concurso)
    {
        if (id != concurso.Id) return BadRequest();
        await _repository.UpdateAsync(concurso);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _repository.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet("{id}/apostas")]
    public async Task<ActionResult<IEnumerable<Aposta>>> GetApostas(int id)
    {
        return Ok(await _repository.GetApostasByConcursoIdAsync(id));
    }
}
