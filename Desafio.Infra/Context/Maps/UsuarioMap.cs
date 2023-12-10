using Desafio.Domain.Entities;
using Desafio.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Desafio.Infra.Context.Maps
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();

            builder.Property(c => c.Senha)
                .HasColumnName(nameof(Usuario.Senha))
                .HasMaxLength(100)
                .HasConversion(
                    x => x.SenhaCifrada,
                    x => Senha.FromHashed(x)
                )
                .IsRequired();
        }
    }
}