using Desafio.Domain.Entities;
using Desafio.Domain.Repositories;
using MediatR;

namespace Desafio.Application.Commands.Clientes.IncluirCliente
{
    public class IncluirClienteCommandHandler : IRequestHandler<IncluirClienteCommand, Result<long>>
    {
        private readonly IClienteRepository _clienteRepository;

        public IncluirClienteCommandHandler(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<Result<long>> Handle(IncluirClienteCommand command, CancellationToken cancellationToken)
        {
            if (await _clienteRepository.ValidarJaExisteEmail(command.Email))
            {
                return Result<long>.Falha("O e-mail informado já está em uso por outro cliente");
            }

            var cliente = new Cliente(
                command.Nome,
                command.Email);

            var enderecos = command.Enderecos.Select(MapEndereco).ToList();
            cliente.AtualizarEnderecos(enderecos);
                
            await _clienteRepository.Incluir(cliente);

            return cliente.Id;
        }

        private static Endereco MapEndereco(IncluirClienteEnderecoCommand command)
        {
            return new Endereco(
                command.Cep,
                command.Logradouro,
                clienteId: 0);
        }
    }
}