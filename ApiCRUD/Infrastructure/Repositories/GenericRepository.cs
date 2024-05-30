using Core.Interface;
using Core.Interface.Repositories;
using Core.Model;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext _appDbContext;

        public GenericRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IQueryable<T> FindAll()
        {
            return _appDbContext.Set<T>().AsNoTracking();
        }
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return _appDbContext.Set<T>().Where(expression).AsNoTracking();
        }
        public async Task<T> Create(T entity)
        {
            await _appDbContext.Set<T>().AddAsync(entity);
            return entity;
        }
        public async Task<IEnumerable<T>> CreateRange(IEnumerable<T> entity)
        {
            await _appDbContext.Set<T>().AddRangeAsync(entity);
            return entity;
        }
        public T Update(T entity)
        {
            try
            {
                _appDbContext.Set<T>().Update(entity);
            }
            catch (Exception ex)
            {
                _ = ex.Message;
            }
            return entity;
        }
        public void Delete(T entity)
        {
            _appDbContext.Set<T>().Remove(entity);
        }
        public void UpdateRangeAsync(IEnumerable<T> entities)
        {
            _appDbContext.Set<T>().UpdateRange(entities);
        }
        public async Task UpdateRangeAsyncAux(IEnumerable<T> entities)
        {
            try
            {
                _appDbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
                _appDbContext.Set<T>().UpdateRange(entities);
            }
            catch (Exception ex)
            {
                _ = ex.Message;
            }
        }
    }
}
