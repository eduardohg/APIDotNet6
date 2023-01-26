using System.Text.Json.Serialization;

namespace BelagricolaQ4.Models
{
    public class ClienteContato
    {
        public int ClienteCodigo { get; set; }

        [JsonIgnore]
        public Cliente? Cliente { get; set; }

        public int ContatoCodigo { get; set; }

        [JsonIgnore]
        public Contato? Contato { get; set; }
        
        public string? TipoRelacionamento { get; set; }
    }
}
