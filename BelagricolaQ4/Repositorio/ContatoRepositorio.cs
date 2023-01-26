using BelagricolaQ4.Data;
using BelagricolaQ4.Models;
using BelagricolaQ4.Repositorio.Interface;
using Microsoft.EntityFrameworkCore;

namespace BelagricolaQ4.Repositorio
{
    public class ContatoRepositorio : IContatoRepositorio
    {
        private readonly BelagricolaQ4DBContex _dbContext;
        public ContatoRepositorio(BelagricolaQ4DBContex belagricolaQ4DBContex)
        {
            _dbContext = belagricolaQ4DBContex;
        }

        public async Task<Contato> Add(Contato contato)
        {
            await _dbContext.Contato.AddAsync(contato);
            await _dbContext.SaveChangesAsync();

            return contato;
        }

        public async Task<List<Contato>> GetAll()
        {
            return await _dbContext.Contato.ToListAsync();
        }

        public async Task<Contato> GetByCodigo(int codigo)
        {
            return await _dbContext.Contato.FirstOrDefaultAsync(x => x.Codigo == codigo);
        }

        public async Task<bool> Delete(int codigo)
        {
            Contato con = await GetByCodigo(codigo);
            if (con == null)
            {
                throw new Exception($"Contato com o código {codigo} não foi encontrado no banco de dados");
            }

            _dbContext.Contato.Remove(con);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<Contato> Update(Contato contato, int codigo)
        {
            Contato con = await GetByCodigo(codigo);
            if (con == null)
            {
                throw new Exception($"Contato com o código {codigo} não foi encontrado no banco de dados");
            }
            con.Nome = contato.Nome;
            con.Telefone = contato.Telefone;
            con.Celular = contato.Celular;
            
            _dbContext.Contato.Update(con);
            await _dbContext.SaveChangesAsync();

            return con;
        }
    }
}
