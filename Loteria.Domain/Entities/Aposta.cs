using System.ComponentModel.DataAnnotations;

namespace Loteria.Domain.Entities;

public class Aposta
{
    public int Id { get; set; }
    
    public int ConcursoId { get; set; }
    public Concurso? Concurso { get; set; }
    
    [Required]
    public string NomeCliente { get; set; } = string.Empty;
    
    public string Numeros { get; set; } = "[]"; // JSON: "[10, 15, 20...]"
    
    public DateTime DataRegistro { get; set; } = DateTime.Now;
}
