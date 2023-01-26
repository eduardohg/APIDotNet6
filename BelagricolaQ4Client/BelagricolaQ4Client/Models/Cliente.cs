using System.ComponentModel.DataAnnotations;

namespace BelagricolaQ4Client.Models
{
    public class Cliente
    {
        public int Codigo { get; set; }
        public string? Cpf { get; set; }
        public string? Email { get; set; }
        public string? Nome { get; set; }
        public string? Telefone { get; set; }
        public string? Celular { get; set; }

    }
}
