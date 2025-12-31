using Loteria.Domain.Entities;

namespace Loteria.Application.Interfaces;

public interface IApostaRepository
{
    Task<Aposta> CreateAsync(Aposta aposta);
    Task UpdateAsync(Aposta aposta);
    Task DeleteAsync(int id);
    Task<Aposta?> GetByIdAsync(int id);
}
