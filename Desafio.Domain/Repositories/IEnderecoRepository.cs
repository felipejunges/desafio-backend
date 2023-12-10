using Desafio.Domain.Entities;

namespace Desafio.Domain.Repositories
{
    public interface IEnderecoRepository
    {
        Task<Endereco?> ObterPeloClienteEId(long clienteId, long id);
        Task<IEnumerable<Endereco>> ListarPeloCliente(long clienteId);
        Task Incluir(Endereco endereco);
        Task Alterar(Endereco endereco);
        Task Excluir(Endereco endereco);
    }
}