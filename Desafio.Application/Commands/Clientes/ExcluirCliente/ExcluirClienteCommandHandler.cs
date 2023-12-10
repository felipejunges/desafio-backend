using Desafio.Domain.Repositories;
using MediatR;

namespace Desafio.Application.Commands.Clientes.ExcluirCliente
{
    public class ExcluirClienteCommandHandler : IRequestHandler<ExcluirClienteCommand, Result<bool>>
    {
        private readonly IClienteRepository _clienteRepository;

        public ExcluirClienteCommandHandler(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<Result<bool>> Handle(ExcluirClienteCommand command, CancellationToken cancellationToken)
        {
            var cliente = await _clienteRepository.ObterPeloId(command.Id);

            if (cliente is null)
            {
                return Result<bool>.Falha("O cliente informado não foi localizado");
            }

            await _clienteRepository.Excluir(cliente);

            return true;
        }
    }
}