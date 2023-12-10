namespace Desafio.Domain.Entities
{
    public class Endereco
    {
        public long Id { get; private set; }

        public string Cep { get; private set; }

        public string Logradouro { get; private set; }

        public long ClienteId { get; private set; }

        public Cliente Cliente { get; private set; } = null!;

        private Endereco()
        {
            Cep = string.Empty;
            Logradouro = string.Empty;
            ClienteId = 0;
        }

        public Endereco(string cep, string logradouro, long clienteId)
        {
            Cep = cep;
            Logradouro = logradouro;
            ClienteId = clienteId;
        }

        public Endereco(long id, string cep, string logradouro, long clienteId)
            : this(cep, logradouro, clienteId)
        {
            Id = id;
        }

        public void Atualizar(string cep, string logradouro)
        {
            Cep = cep;
            Logradouro = logradouro;
        }
    }
}