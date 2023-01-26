using BelagricolaQ4.Data.Map;
using BelagricolaQ4.Models;
using Microsoft.EntityFrameworkCore;

namespace BelagricolaQ4.Data{
    
    public class BelagricolaQ4DBContex : DbContext{

        public BelagricolaQ4DBContex(DbContextOptions<BelagricolaQ4DBContex> options)
        :base(options)
        {

        }

        public DbSet<Cliente> Cliente {get; set;}
        public DbSet<Contato> Contato { get; set;}

        public DbSet<ClienteContato> ClienteContatos { get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder){
            modelBuilder.ApplyConfiguration(new ContatoMap());
            modelBuilder.ApplyConfiguration(new ClienteMap());
            modelBuilder.ApplyConfiguration(new ClienteContatoMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}