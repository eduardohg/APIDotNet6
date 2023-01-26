using BelagricolaQ4.Data;
using BelagricolaQ4.Models;
using BelagricolaQ4.Repositorio.Interface;
using Microsoft.EntityFrameworkCore;

namespace BelagricolaQ4.Repositorio{


    public class ClienteRepositorio : IClienteRepositorio
    {
        private readonly BelagricolaQ4DBContex _dbContext;
        public ClienteRepositorio(BelagricolaQ4DBContex belagricolaQ4DBContex){
            _dbContext = belagricolaQ4DBContex;
        }

        public async Task<Cliente> Add(Cliente cliente)
        {
            await _dbContext.Cliente.AddAsync(cliente);
            await _dbContext.SaveChangesAsync();

            return cliente;
        }

        public async Task<List<Cliente>> GetAll()
        {
            return await _dbContext.Cliente.ToListAsync();
        }

        public async Task<Cliente> GetByCodigo(int codigo)
        {
            return await _dbContext.Cliente.FirstOrDefaultAsync(x => x.Codigo == codigo);
        }

        public async Task<bool> Delete(int codigo)
        {
            Cliente cli = await GetByCodigo(codigo);
            if(cli == null){
                throw new Exception($"Cliente com o id {codigo} não foi encontrado no banco de dados");
            }

            _dbContext.Cliente.Remove(cli);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<Cliente> Update(Cliente cliente, int codigo)
        {
            Cliente cli = await GetByCodigo(codigo);
            if(cli == null){
                throw new Exception($"Cliente com o id{codigo} não foi encontrado no banco de dados");
            }
            cli.Cpf = cliente.Cpf;
            cli.Email = cliente.Email;
            cli.Nome = cliente.Nome;
            cli.Telefone = cliente.Telefone;
            cli.Celular = cliente.Celular;


            _dbContext.Cliente.Update(cli);
            await _dbContext.SaveChangesAsync();

            return cli;
        }
    }
}