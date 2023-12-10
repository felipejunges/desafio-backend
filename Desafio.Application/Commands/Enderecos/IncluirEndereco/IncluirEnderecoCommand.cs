using MediatR;

namespace Desafio.Application.Commands.Enderecos.IncluirEndereco
{
    public record IncluirEnderecoCommand(string Cep, string Logradouro) : IRequest<Result<long>>
    {
        public long ClienteId { get; private set; }

        public void AgregarPropriedades(long clienteId)
        {
            ClienteId = clienteId;
        }
    }
}