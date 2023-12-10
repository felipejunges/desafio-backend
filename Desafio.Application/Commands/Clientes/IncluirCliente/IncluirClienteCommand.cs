using MediatR;

namespace Desafio.Application.Commands.Clientes.IncluirCliente
{
    public record IncluirClienteCommand(string Nome, string Email, IncluirClienteEnderecoCommand[] Enderecos) : IRequest<Result<long>>;

    public record IncluirClienteEnderecoCommand(string Cep, string Logradouro);
}