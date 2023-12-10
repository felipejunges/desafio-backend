using Desafio.Domain.Entities;
using Desafio.Domain.Repositories;
using MediatR;

namespace Desafio.Application.Commands.Enderecos.IncluirEndereco
{
    public class IncluirEnderecoCommandHandler : IRequestHandler<IncluirEnderecoCommand, Result<long>>
    {
        private readonly IEnderecoRepository _enderecoRepository;

        public IncluirEnderecoCommandHandler(IEnderecoRepository enderecoRepository)
        {
            _enderecoRepository = enderecoRepository;
        }

        public async Task<Result<long>> Handle(IncluirEnderecoCommand command, CancellationToken cancellationToken)
        {
            var endereco = new Endereco(
                command.Cep,
                command.Logradouro,
                command.ClienteId);

            await _enderecoRepository.Incluir(endereco);

            return endereco.Id;
        }
    }
}