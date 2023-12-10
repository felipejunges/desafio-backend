using MediatR;

namespace Desafio.Application.Commands.Enderecos.ExcluirEndereco
{
    public record ExcluirEnderecoCommand(long ClienteId, long Id) : IRequest<Result<bool>>;
}