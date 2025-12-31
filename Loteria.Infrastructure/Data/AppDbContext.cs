using Loteria.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Loteria.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Concurso> Concursos { get; set; }
    public DbSet<Aposta> Apostas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Concurso>()
            .HasKey(c => c.Id);

        modelBuilder.Entity<Aposta>()
            .HasKey(a => a.Id);

        modelBuilder.Entity<Aposta>()
            .HasOne(a => a.Concurso)
            .WithMany()
            .HasForeignKey(a => a.ConcursoId)
            .OnDelete(DeleteBehavior.Cascade);

        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            foreach (var property in entity.GetProperties()
                         .Where(p => p.ClrType == typeof(DateTime)))
            {
                property.SetColumnType("timestamp with time zone");
            }
        }
    }
}
