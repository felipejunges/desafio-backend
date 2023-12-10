namespace Desafio.Domain.Entities
{
    public class Cliente
    {
        public long Id { get; private set; }

        public string Nome { get; private set; }

        public string Email { get; private set; }

        public ICollection<Endereco> Enderecos { get; private set; }

        private Cliente()
        {
            Nome = string.Empty;
            Email = string.Empty;
            Enderecos = new HashSet<Endereco>();
        }

        public Cliente(string nome, string email)
        {
            Nome = nome;
            Email = email;
            Enderecos = new HashSet<Endereco>();
        }

        public void Atualizar(string nome, string email)
        {
            Nome = nome;
            Email = email;
        }

        public void AtualizarEnderecos(ICollection<Endereco> enderecos)
        {
            Enderecos = enderecos;
        }
    }
}
