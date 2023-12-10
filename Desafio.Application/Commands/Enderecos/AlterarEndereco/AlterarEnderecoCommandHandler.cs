using Desafio.Domain.Repositories;
using MediatR;

namespace Desafio.Application.Commands.Enderecos.AlterarEndereco
{
    public class AlterarEnderecoCommandHandler : IRequestHandler<AlterarEnderecoCommand, Result<bool>>
    {
        private readonly IEnderecoRepository _enderecoRepository;

        public AlterarEnderecoCommandHandler(IEnderecoRepository enderecoRepository)
        {
            _enderecoRepository = enderecoRepository;
        }

        public async Task<Result<bool>> Handle(AlterarEnderecoCommand command, CancellationToken cancellationToken)
        {
            var endereco = await _enderecoRepository.ObterPeloClienteEId(command.ClienteId, command.Id);

            if (endereco == null)
            {
                return Result<bool>.Falha("O endereço informado não foi localizado ou não pertence ao cliente informado.");
            }

            endereco.Atualizar(
                command.Cep,
                command.Logradouro);

            await _enderecoRepository.Alterar(endereco);

            return true;
        }
    }
}