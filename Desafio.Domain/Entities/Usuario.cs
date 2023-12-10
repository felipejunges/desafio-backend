using Desafio.Domain.ValueObjects;

namespace Desafio.Domain.Entities
{
    public class Usuario
    {
        public long Id { get; private set; }

        public string Nome { get; private set; }

        public string Email { get; private set; }

        public Senha Senha { get; private set; }

        public string Role { get; private set; }

        public bool Ativo { get; private set; }

        private Usuario()
        {
            Nome = string.Empty;
            Email = string.Empty;
            Senha = string.Empty;
            Role = string.Empty;
        }

        public Usuario(long id, string nome, string email, Senha senha, string role, bool ativo)
        {
            Id = id;
            Nome = nome;
            Email = email;
            Senha = senha;
            Role = role;
            Ativo = ativo;
        }
    }
}