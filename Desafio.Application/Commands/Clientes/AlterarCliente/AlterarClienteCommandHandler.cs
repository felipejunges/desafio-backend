using Desafio.Domain.Entities;
using Desafio.Domain.Repositories;
using Desafio.Domain.UnitOfWork;
using MediatR;

namespace Desafio.Application.Commands.Clientes.AlterarCliente
{
    public class AlterarClienteCommandHandler : IRequestHandler<AlterarClienteCommand, Result<bool>>
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IEnderecoRepository _enderecoRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AlterarClienteCommandHandler(IClienteRepository clienteRepository, IEnderecoRepository enderecoRepository, IUnitOfWork unitOfWork)
        {
            _clienteRepository = clienteRepository;
            _enderecoRepository = enderecoRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(AlterarClienteCommand command, CancellationToken cancellationToken)
        {
            var cliente = await _clienteRepository.ObterPeloId(command.Id);

            if (cliente is null)
            {
                return Result<bool>.Falha("O cliente informado não foi localizado");
            }

            if (await _clienteRepository.ValidarJaExisteEmail(command.Email, command.Id))
            {
                return Result<bool>.Falha("O e-mail informado já está em uso por outro cliente");
            }

            await AtualizarClienteComEnderecos(command, cliente);

            return true;
        }

        private async Task AtualizarClienteComEnderecos(AlterarClienteCommand command, Cliente cliente)
        {
            cliente.Atualizar(
                command.Nome,
                command.Email);

            await _unitOfWork.BeginTransactionAsync();

            await _clienteRepository.Alterar(cliente);

            var enderecos = command.Enderecos.Select(e => MapEndereco(e, cliente.Id));
            await AtualizarTodosEnderecos(cliente.Id, enderecos);

            await _unitOfWork.SaveChangesAsync();
        }

        private static Endereco MapEndereco(AlterarClienteEnderecoCommand command, long clienteId)
        {
            return new Endereco(
                command.Id,
                command.Cep,
                command.Logradouro,
                clienteId);
        }

        private async Task AtualizarTodosEnderecos(long clienteId, IEnumerable<Endereco> novos)
        {
            var atuais = await _enderecoRepository.ListarPeloCliente(clienteId);

            await RemoverEnderecos(atuais, novos);

            await AtualizarEnderecos(atuais, novos);

            await AdicionarEnderecos(atuais, novos);
        }

        private async Task RemoverEnderecos(IEnumerable<Endereco> atuais, IEnumerable<Endereco> novos)
        {
            var listaRemover = atuais.Where(e => !novos.Any(n => n.Id == e.Id));

            for (int i = 0; i < listaRemover.Count(); i++)
            {
                await _enderecoRepository.Excluir(listaRemover.ElementAt(i));
            }
        }

        private async Task AdicionarEnderecos(IEnumerable<Endereco> atuais, IEnumerable<Endereco> novos)
        {
            var listaAdicionar = novos.Where(e => !atuais.Any(a => a.Id == e.Id));

            for (int i = 0; i < listaAdicionar.Count(); i++)
            {
                await _enderecoRepository.Incluir(listaAdicionar.ElementAt(i));
            }
        }

        private async Task AtualizarEnderecos(IEnumerable<Endereco> atuais, IEnumerable<Endereco> novos)
        {
            var listaAtualizar = atuais.Where(e => novos.Any(n => n.Id == e.Id));

            for (int i = 0; i < listaAtualizar.Count(); i++)
            {
                var enderecoAtualizar = listaAtualizar.ElementAt(i);
                var enderecoNovo = novos.Single(e => e.Id == enderecoAtualizar.Id);

                enderecoAtualizar.Atualizar(
                    enderecoNovo.Cep,
                    enderecoNovo.Logradouro);

                await _enderecoRepository.Alterar(enderecoAtualizar);
            }
        }
    }
}