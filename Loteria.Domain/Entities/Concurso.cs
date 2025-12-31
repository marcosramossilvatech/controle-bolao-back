using System.ComponentModel.DataAnnotations;

namespace Loteria.Domain.Entities;

public class Concurso
{
    public int Id { get; set; }
    
    [Required]
    public string Numero { get; set; } = string.Empty;
    
    public string Descricao { get; set; } = string.Empty;
    
    public DateTime DataSorteio { get; set; }
    
    public decimal PremioEstimado { get; set; }
    
    [Required]
    public string Status { get; set; } = "ABERTO"; // ABERTO, AGUARDANDO_SORTEIO, FINALIZADO
    
    public string NumerosSorteados { get; set; } = "[]"; // Armazenado como JSON string: "[1, 2, 3...]"

    // Propriedade auxiliar não mapeada se necessário, mas para SQLite simples vamos usar string
    // Em produção usaria ValueConverter do EF Core
}
