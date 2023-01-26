using BelagricolaQ4.Models;

namespace BelagricolaQ4.Repositorio.Interface{

    public interface IClienteRepositorio{
        public Task<Cliente> Add(Cliente cliente);

        public Task<Cliente> GetByCodigo(int codigo);

        public Task<List<Cliente>> GetAll();

        public Task<bool> Delete(int codigo);

        public Task<Cliente> Update(Cliente cliente, int codigo);
    }

    
}