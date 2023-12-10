using Desafio.Domain.Entities;
using Desafio.Domain.Repositories;
using Desafio.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace Desafio.Infra.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly CadastroDb _db;

        public UsuarioRepository(CadastroDb db)
        {
            _db = db;
        }

        public Task<Usuario?> ObterUsuarioAtivoPeloEmail(string email)
        {
            return _db.Usuarios.FirstOrDefaultAsync(u => u.Email == email && u.Ativo);
        }
    }
}