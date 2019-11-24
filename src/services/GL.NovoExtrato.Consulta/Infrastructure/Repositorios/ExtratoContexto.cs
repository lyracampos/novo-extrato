using GL.NovoExtrato.Consulta.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace GL.NovoExtrato.Consulta.Infrastructure.Repositorios
{
    public class ExtratoContexto : DbContext
    {
        private IConfiguration _configuration;

        public ExtratoContexto(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = _configuration["ConnectionString"];
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var extrato = modelBuilder.Entity<Extrato>();
            extrato.ToTable("Extrato");
            extrato.HasKey(p => p.Id);
            extrato.Property(p => p.Id).IsRequired();
            extrato.Property(p => p.Descricao).IsRequired().HasColumnType("varchar(255)");
            extrato.Property(p => p.NomeClienteDestino).IsRequired().HasColumnType("varchar(255)");
            extrato.Property(p => p.NomeClienteOrigem).IsRequired().HasColumnType("varchar(255)");
            extrato.Property(p => p.Tipo).IsRequired();
            extrato.Property(p => p.ValorTransacao).IsRequired();
            extrato.Property(p => p.Saldo).IsRequired();
            extrato.Property(p => p.ClienteId).IsRequired();
            extrato.Property(p => p.TransacaoTipo).IsRequired();
        }

        public DbSet<Extrato> Extratos { get; set; }
    }
}