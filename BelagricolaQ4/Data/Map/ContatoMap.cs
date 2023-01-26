using BelagricolaQ4.Models;
using Microsoft.EntityFrameworkCore;

namespace BelagricolaQ4.Data.Map
{
    public class ContatoMap : IEntityTypeConfiguration<Contato>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Contato> builder)
        {
            builder.HasKey(x => x.Codigo);
            builder.Property(x => x.Nome).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Telefone).HasMaxLength(15);
            builder.Property(x => x.Celular).HasMaxLength(15);

        }
    }
}
