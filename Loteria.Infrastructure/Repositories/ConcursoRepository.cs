using Loteria.Application.Interfaces;
using Loteria.Domain.Entities;
using Loteria.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Loteria.Infrastructure.Repositories;

public class ConcursoRepository : IConcursoRepository
{
    private readonly AppDbContext _context;

    public ConcursoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Concurso>> GetAllAsync()
    {
        return await _context.Concursos.ToListAsync();
    }

    public async Task<Concurso?> GetByIdAsync(int id)
    {
        return await _context.Concursos.FindAsync(id);
    }

    public async Task<Concurso> CreateAsync(Concurso concurso)
    {
        _context.Concursos.Add(concurso);
        await _context.SaveChangesAsync();
        return concurso;
    }

    public async Task UpdateAsync(Concurso concurso)
    {
        _context.Entry(concurso).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _context.Concursos.FindAsync(id);
        if (entity != null)
        {
            _context.Concursos.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Aposta>> GetApostasByConcursoIdAsync(int concursoId)
    {
        return await _context.Apostas
            .Where(a => a.ConcursoId == concursoId)
            .ToListAsync();
    }
}
