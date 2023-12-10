using Desafio.Domain.Entities;
using Desafio.Infra.Context.Maps;
using Microsoft.EntityFrameworkCore;

namespace Desafio.Infra.Context
{
    public class CadastroDb : DbContext
    {
        public CadastroDb(DbContextOptions<CadastroDb> options) : base(options) { }

        public DbSet<Cliente> Clientes => Set<Cliente>();
        public DbSet<Endereco> Enderecos => Set<Endereco>();
        public DbSet<Usuario> Usuarios => Set<Usuario>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ClienteMap());
            modelBuilder.ApplyConfiguration(new EnderecoMap());
            modelBuilder.ApplyConfiguration(new UsuarioMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}