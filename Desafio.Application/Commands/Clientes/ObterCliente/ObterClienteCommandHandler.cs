using Desafio.Domain.Entities;
using Desafio.Domain.Repositories;
using MediatR;

namespace Desafio.Application.Commands.Clientes.ObterCliente
{
    public class ObterClienteCommandHandler : IRequestHandler<ObterClienteCommand, Cliente?>
    {
        private readonly IClienteRepository _clienteRepository;

        public ObterClienteCommandHandler(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public Task<Cliente?> Handle(ObterClienteCommand command, CancellationToken cancellationToken)
        {
            return _clienteRepository.ObterPeloId(command.Id);
        }
    }
}