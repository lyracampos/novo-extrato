using GL.NovoExtrato.Lancamento.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace GL.NovoExtrato.Lancamento.Infrastructure.Repositorios
{
    public class LancamentoContexto : DbContext
    {
        private IConfiguration _configuration;
        public LancamentoContexto(IConfiguration configuration)
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
            var cliente = modelBuilder.Entity<Cliente>();
            cliente.ToTable("Cliente");
            cliente.HasKey(p => p.Id);
            cliente.Property(p => p.Id).IsRequired();
            cliente.Property(p => p.Nome).IsRequired().HasColumnType("nvarchar(255)");

            cliente
                .HasOne<ContaCorrente>(c => c.ContaCorrente)
                .WithOne(cc => cc.Cliente)
                .HasForeignKey<ContaCorrente>(cc => cc.ClienteId);

            var contaCorrente = modelBuilder.Entity<ContaCorrente>();
            contaCorrente.ToTable("ContaCorrente");
            contaCorrente.HasKey(p => p.Id);
            contaCorrente.Property(p => p.ClienteId).IsRequired();
            contaCorrente.Property(p => p.Numero).IsRequired().HasColumnType("nvarchar(100)");
            contaCorrente.Property(p => p.Saldo).IsRequired();
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<ContaCorrente> ContasCorrentes { get; set; }
    }
}
