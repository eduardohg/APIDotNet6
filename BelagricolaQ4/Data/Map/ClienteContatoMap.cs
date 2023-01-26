using BelagricolaQ4.Models;
using Microsoft.EntityFrameworkCore;

namespace BelagricolaQ4.Data.Map
{
    public class ClienteContatoMap : IEntityTypeConfiguration<ClienteContato>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ClienteContato> builder)
        {
            builder.HasKey(x => new { x.ClienteCodigo, x.ContatoCodigo});
            builder.Property(x => x.TipoRelacionamento).IsRequired().HasMaxLength(255);
        }
    }
}
