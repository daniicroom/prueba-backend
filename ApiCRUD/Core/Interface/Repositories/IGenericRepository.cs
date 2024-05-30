using Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface.Repositories
{
    public interface IGenericRepository<T>
    {
        IQueryable<T> FindAll();
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
        Task<T> Create(T entity);
        Task<IEnumerable<T>> CreateRange(IEnumerable<T> entity);
        T Update(T entity);
        void Delete(T entity);
        void UpdateRangeAsync(IEnumerable<T> entities);
        Task UpdateRangeAsyncAux(IEnumerable<T> entities);
    }
}
