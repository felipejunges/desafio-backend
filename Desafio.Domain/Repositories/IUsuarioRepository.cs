using Desafio.Domain.Entities;

namespace Desafio.Domain.Repositories
{
    public interface IUsuarioRepository
    {
        Task<Usuario?> ObterUsuarioAtivoPeloEmail(string email);
    }
}