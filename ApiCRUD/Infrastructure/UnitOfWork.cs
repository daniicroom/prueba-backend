using Core.Interface.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        public AppDbContext _appDbContext { get; private set; }
        public UnitOfWork(AppDbContext AppDbContext)
        {
            _appDbContext = AppDbContext;
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _appDbContext.Database.BeginTransactionAsync();
        }

        public IDbContextTransaction BeginTransaction()
        {
            return _appDbContext.Database.BeginTransaction();
        }

        public async Task SaveChangesAsync()
        {
            await _appDbContext.SaveChangesAsync();
        }
        public void SaveChanges()
        {
            _appDbContext.SaveChanges();
        }
        public void Rollback()
        {
            _appDbContext.Database.RollbackTransaction();
        }
    }
}
