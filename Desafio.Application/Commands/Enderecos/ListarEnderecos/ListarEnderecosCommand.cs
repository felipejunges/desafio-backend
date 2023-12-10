using Desafio.Domain.Entities;
using MediatR;

namespace Desafio.Application.Commands.Enderecos.ListarEnderecos
{
    public record ListarEnderecosCommand(long ClienteId) : IRequest<IEnumerable<Endereco>>;
}