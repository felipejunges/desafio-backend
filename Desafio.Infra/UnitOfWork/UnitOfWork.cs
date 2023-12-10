using Desafio.Domain.UnitOfWork;
using Desafio.Infra.Context;

namespace Desafio.Infra.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CadastroDb _db;

        public UnitOfWork(CadastroDb db)
        {
            _db = db;
        }

        public Task BeginTransactionAsync()
        {
            return _db.Database.BeginTransactionAsync();
        }

        public Task CommitTransactionAsync()
        {
            return _db.Database.CommitTransactionAsync();
        }

        public Task RollbackTransactionAsync()
        {
            return _db.Database.RollbackTransactionAsync();
        }

        public Task SaveChangesAsync()
        {
            return _db.SaveChangesAsync();
        }
    }
}
