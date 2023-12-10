using Desafio.Domain.Entities;
using Desafio.Domain.Repositories;
using MediatR;

namespace Desafio.Application.Commands.Enderecos.ListarEnderecos
{
    public class ListarEnderecosCommandHandler : IRequestHandler<ListarEnderecosCommand, IEnumerable<Endereco>>
    {
        private readonly IEnderecoRepository _enderecoRepository;

        public ListarEnderecosCommandHandler(IEnderecoRepository enderecoRepository)
        {
            _enderecoRepository = enderecoRepository;
        }

        public Task<IEnumerable<Endereco>> Handle(ListarEnderecosCommand command, CancellationToken cancellationToken)
        {
            return _enderecoRepository.ListarPeloCliente(command.ClienteId);
        }
    }
}