using Desafio.Domain.Entities;
using MediatR;

namespace Desafio.Application.Commands.Clientes.ListarClientes
{
    public record ListarClientesCommand : IRequest<IEnumerable<Cliente>>;
}