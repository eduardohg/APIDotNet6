using BelagricolaQ4.Models;

namespace BelagricolaQ4.Repositorio.Interface
{
    public interface IClienteContatoRepositorio
    {
        public Task<ClienteContato> Add(ClienteContato clienteContato);

        public Task<ClienteContato> GetByCodigo(int cliente, int contato);

        public Task<List<ClienteContato>> GetAll();

        public Task<bool> Delete(int cliente, int contato);

        public Task<ClienteContato> Update(ClienteContato clienteContato, int cliente, int contato);
    }
}
