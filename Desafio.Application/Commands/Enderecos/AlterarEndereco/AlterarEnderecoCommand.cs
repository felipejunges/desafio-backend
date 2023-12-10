using MediatR;

namespace Desafio.Application.Commands.Enderecos.AlterarEndereco
{
    public record AlterarEnderecoCommand(string Cep, string Logradouro) : IRequest<Result<bool>>
    {
        public long Id { get; private set; }
        public long ClienteId { get; private set; }

        public void AgregarPropriedades(long id, long clienteId)
        {
            Id = id;
            ClienteId = clienteId;
        }
    }
}