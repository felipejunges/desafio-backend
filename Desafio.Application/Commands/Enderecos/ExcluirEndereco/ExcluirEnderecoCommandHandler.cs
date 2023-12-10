using Desafio.Domain.Repositories;
using MediatR;

namespace Desafio.Application.Commands.Enderecos.ExcluirEndereco
{
    public class ExcluirEnderecoCommandHandler : IRequestHandler<ExcluirEnderecoCommand, Result<bool>>
    {
        private readonly IEnderecoRepository _enderecoRepository;

        public ExcluirEnderecoCommandHandler(IEnderecoRepository enderecoRepository)
        {
            _enderecoRepository = enderecoRepository;
        }

        public async Task<Result<bool>> Handle(ExcluirEnderecoCommand command, CancellationToken cancellationToken)
        {
            var endereco = await _enderecoRepository.ObterPeloClienteEId(command.ClienteId, command.Id);

            if (endereco == null)
            {
                return Result<bool>.Falha("O endereço informado não foi localizado ou não pertence ao cliente informado.");
            }

            await _enderecoRepository.Excluir(endereco);

            return true;
        }
    }
}