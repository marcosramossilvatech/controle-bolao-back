using Loteria.Domain.Entities;

namespace Loteria.Application.Interfaces;

public interface IConcursoRepository
{
    Task<IEnumerable<Concurso>> GetAllAsync();
    Task<Concurso?> GetByIdAsync(int id);
    Task<Concurso> CreateAsync(Concurso concurso);
    Task UpdateAsync(Concurso concurso);
    Task DeleteAsync(int id);
    Task<IEnumerable<Aposta>> GetApostasByConcursoIdAsync(int concursoId);
}
