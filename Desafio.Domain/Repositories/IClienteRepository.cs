using Desafio.Domain.Entities;

namespace Desafio.Domain.Repositories
{
    public interface IClienteRepository
    {
        Task<Cliente?> ObterPeloId(long id);
        Task<IEnumerable<Cliente>> ListarTodos();
        Task<bool> ValidarJaExisteEmail(string email, long? clienteIdExceto = null);
        Task Incluir(Cliente cliente);
        Task Alterar(Cliente cliente);
        Task Excluir(Cliente cliente);
    }
}