using BelagricolaQ4.Models;

namespace BelagricolaQ4.Repositorio.Interface
{
    public interface IContatoRepositorio
    {
        public Task<Contato> Add(Contato contato);

        public Task<Contato> GetByCodigo(int codigo);

        public Task<List<Contato>> GetAll();

        public Task<bool> Delete(int codigo);

        public Task<Contato> Update(Contato contato, int codigo);
    }
}
