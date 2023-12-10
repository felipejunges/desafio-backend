using Desafio.Domain.Entities;
using Desafio.Domain.Repositories;
using Desafio.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace Desafio.Infra.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly CadastroDb _db;

        public ClienteRepository(CadastroDb db)
        {
            _db = db;
        }

        public Task<Cliente?> ObterPeloId(long id)
        {
            return _db.Clientes
                .Include(c => c.Enderecos)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Cliente>> ListarTodos()
        {
            return await _db.Clientes.OrderBy(c => c.Nome).ToListAsync();
        }

        public Task<bool> ValidarJaExisteEmail(string email, long? clienteIdExceto = null)
        {
            return _db.Clientes.AnyAsync(c => c.Email == email && c.Id != clienteIdExceto);
        }

        public async Task Incluir(Cliente cliente)
        {
            await _db.Clientes.AddAsync(cliente);
            await _db.SaveChangesAsync();
        }

        public async Task Alterar(Cliente cliente)
        {
            _db.Clientes.Update(cliente);
            await _db.SaveChangesAsync();
        }

        public async Task Excluir(Cliente cliente)
        {
            _db.Clientes.Remove(cliente);
            await _db.SaveChangesAsync();
        }
    }
}