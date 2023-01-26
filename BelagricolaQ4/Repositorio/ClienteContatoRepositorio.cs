using BelagricolaQ4.Data;
using BelagricolaQ4.Models;
using BelagricolaQ4.Repositorio.Interface;
using Microsoft.EntityFrameworkCore;

namespace BelagricolaQ4.Repositorio
{
    public class ClienteContatoRepositorio : IClienteContatoRepositorio
    {
        private readonly BelagricolaQ4DBContex _dbContext;
        public ClienteContatoRepositorio(BelagricolaQ4DBContex belagricolaQ4DBContex)
        {
            _dbContext = belagricolaQ4DBContex;
        }

        public async Task<ClienteContato> Add(ClienteContato clienteContato)
        {
            ClienteContato cliCon = await GetByCodigo(clienteContato.ClienteCodigo, clienteContato.ContatoCodigo);
            if (cliCon == null)
            {
                await _dbContext.ClienteContatos.AddAsync(clienteContato);
                await _dbContext.SaveChangesAsync();
                return clienteContato;
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> Delete(int cliente, int contato)
        {
            ClienteContato cliCon = await GetByCodigo(cliente, contato);
            if (cliCon == null)
            {
                throw new Exception($"Contato não foi encontrado no banco de dados");
            }

            _dbContext.ClienteContatos.Remove(cliCon);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<List<ClienteContato>> GetAll()
        {
            return await _dbContext.ClienteContatos.ToListAsync();
        }

        public async Task<ClienteContato> GetByCodigo(int cliente, int contato)
        {
            return await _dbContext.ClienteContatos.FirstOrDefaultAsync(x => x.ClienteCodigo == cliente && x.ContatoCodigo == contato);
        }

        public async Task<ClienteContato> Update(ClienteContato clienteContato, int cliente, int contato)
        {
            ClienteContato cliCon = await GetByCodigo(cliente, contato);
            if (cliCon == null)
            {
                throw new Exception($"Contato não foi encontrado no banco de dados");
            }
            cliCon.ClienteCodigo = clienteContato.ClienteCodigo;
            cliCon.ContatoCodigo = clienteContato.ContatoCodigo;
            cliCon.TipoRelacionamento = clienteContato.TipoRelacionamento;

            _dbContext.ClienteContatos.Update(cliCon);
            await _dbContext.SaveChangesAsync();

            return cliCon;
        }
    }
}
