using Desafio.Domain.Entities;
using MediatR;

namespace Desafio.Application.Commands.Clientes.ObterCliente
{
    public record ObterClienteCommand(long Id) : IRequest<Cliente?>;
}