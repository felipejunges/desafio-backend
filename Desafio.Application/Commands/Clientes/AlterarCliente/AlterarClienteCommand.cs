using MediatR;

namespace Desafio.Application.Commands.Clientes.AlterarCliente
{
    public record AlterarClienteCommand(string Nome, string Email, AlterarClienteEnderecoCommand[] Enderecos) : IRequest<Result<bool>>
    {
        public long Id { get; private set; }

        public void AgregarPropriedades(long id)
        {
            Id = id;
        }
    }

    public record AlterarClienteEnderecoCommand(long Id, string Cep, string Logradouro);
}