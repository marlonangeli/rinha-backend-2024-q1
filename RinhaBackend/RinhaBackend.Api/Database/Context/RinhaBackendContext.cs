using Microsoft.EntityFrameworkCore;
using RinhaBackend.Api.Models;

namespace RinhaBackend.Api.Database.Context;

public class RinhaBackendContext : DbContext
{
    public DbSet<Transacao> Transacoes { get; set; }
    public DbSet<Cliente> Clientes { get; set; }

    public RinhaBackendContext(DbContextOptions<RinhaBackendContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Transacao>().HasKey(t => t.Id);

        modelBuilder.Entity<Transacao>()
            .Property(c => c.Descricao)
            .IsRequired().HasMaxLength(10);

        modelBuilder.Entity<Transacao>()
            .Property(c => c.Valor)
            .IsRequired();

        modelBuilder.Entity<Transacao>()
            .Property(c => c.Tipo)
            .IsRequired()
            .HasMaxLength(1)
            .IsFixedLength();

        modelBuilder.Entity<Transacao>()
            .Property(c => c.Data)
            .IsRequired();

        modelBuilder.Entity<Transacao>()
            .HasOne(t => t.Cliente)
            .WithMany(c => c.Transacoes)
            .HasForeignKey(t => t.ClienteId)
            .IsRequired();

        modelBuilder.Entity<Cliente>().HasKey(c => c.Id);

        modelBuilder.Entity<Cliente>()
            .Property(c => c.Nome)
            .IsRequired()
            .HasMaxLength(32);

        modelBuilder.Entity<Cliente>()
            .Property(c => c.Limite)
            .IsRequired();

        modelBuilder.Entity<Cliente>()
            .Property(c => c.SaldoInicial)
            .IsRequired();

        modelBuilder.Entity<Cliente>()
            .HasMany(c => c.Transacoes)
            .WithOne(t => t.Cliente)
            .HasForeignKey(t => t.ClienteId);

        base.OnModelCreating(modelBuilder);
    }
}