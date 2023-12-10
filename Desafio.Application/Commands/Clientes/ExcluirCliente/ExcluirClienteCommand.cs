using MediatR;

namespace Desafio.Application.Commands.Clientes.ExcluirCliente
{
    public record ExcluirClienteCommand(long Id) : IRequest<Result<bool>>;
}