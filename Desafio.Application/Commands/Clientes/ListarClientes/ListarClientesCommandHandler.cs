using Desafio.Domain.Entities;
using Desafio.Domain.Repositories;
using MediatR;

namespace Desafio.Application.Commands.Clientes.ListarClientes
{
    public class ListarClientesCommandHandler : IRequestHandler<ListarClientesCommand, IEnumerable<Cliente>>
    {
        private readonly IClienteRepository _clienteRepository;

        public ListarClientesCommandHandler(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public Task<IEnumerable<Cliente>> Handle(ListarClientesCommand command, CancellationToken cancellationToken)
        {
            return _clienteRepository.ListarTodos();
        }
    }
}