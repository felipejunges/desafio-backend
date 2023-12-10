using Desafio.Domain.Entities;
using Desafio.Domain.Repositories;
using Desafio.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace Desafio.Infra.Repositories
{
    public class EnderecoRepository : IEnderecoRepository
    {
        private readonly CadastroDb _db;

        public EnderecoRepository(CadastroDb db)
        {
            _db = db;
        }

        public Task<Endereco?> ObterPeloClienteEId(long clienteId, long id)
        {
            return _db.Enderecos.FirstOrDefaultAsync(e => e.ClienteId == clienteId && e.Id == id);
        }

        public async Task<IEnumerable<Endereco>> ListarPeloCliente(long clienteId)
        {
            return await _db.Enderecos
                .Where(e => e.ClienteId == clienteId)
                .ToListAsync();
        }

        public async Task Incluir(Endereco endereco)
        {
            await _db.Enderecos.AddAsync(endereco);
            await _db.SaveChangesAsync();
        }

        public Task Alterar(Endereco endereco)
        {
            _db.Enderecos.Update(endereco);
            return _db.SaveChangesAsync();
        }

        public Task Excluir(Endereco endereco)
        {
            _db.Enderecos.Remove(endereco);
            return _db.SaveChangesAsync();
        }
    }
}