using Loteria.Application.Interfaces;
using Loteria.Domain.Entities;
using Loteria.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Loteria.Infrastructure.Repositories;

public class ApostaRepository : IApostaRepository
{
    private readonly AppDbContext _context;

    public ApostaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Aposta> CreateAsync(Aposta aposta)
    {
        _context.Apostas.Add(aposta);
        await _context.SaveChangesAsync();
        return aposta;
    }

    public async Task UpdateAsync(Aposta aposta)
    {
        _context.Entry(aposta).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _context.Apostas.FindAsync(id);
        if (entity != null)
        {
            _context.Apostas.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<Aposta?> GetByIdAsync(int id)
    {
        return await _context.Apostas.FindAsync(id);
    }
}
