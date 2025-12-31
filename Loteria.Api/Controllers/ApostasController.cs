using Loteria.Application.Interfaces;
using Loteria.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Loteria.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ApostasController : ControllerBase
{
    private readonly IApostaRepository _repository;

    public ApostasController(IApostaRepository repository)
    {
        _repository = repository;
    }

    [HttpPost]
    public async Task<ActionResult<Aposta>> Post(Aposta aposta)
    {
        aposta.DataRegistro = aposta.DataRegistro.Kind switch
        {
            DateTimeKind.Utc => aposta.DataRegistro,
            DateTimeKind.Local => aposta.DataRegistro.ToUniversalTime(),
            _ => DateTime.SpecifyKind(aposta.DataRegistro, DateTimeKind.Utc)
        };

        var created = await _repository.CreateAsync(aposta);
        return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Aposta>> Get(int id)
    {
        var aposta = await _repository.GetByIdAsync(id);
        if (aposta == null) return NotFound();
        return Ok(aposta);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, Aposta aposta)
    {
        if (id != aposta.Id) return BadRequest();

         await _repository.UpdateAsync(aposta);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _repository.DeleteAsync(id);
        return NoContent();
    }
}
