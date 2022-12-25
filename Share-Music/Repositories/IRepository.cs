using Share_Music.Models;
using System.Linq.Expressions;

namespace Share_Music.Repositories
{
    public interface IRepository<T> where T : class
    {
        bool Create(IEnumerable<T> entities);
        bool Create(T entity);
        Task CreateAsync(T entity);
        Task CreateAsync(IEnumerable<T> entities);
        bool Delete(T entity);
        Task DeleteAsync(T entity);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetByFilter(Expression<Func<T, bool>> filterClause);
        T GetById(string id);
        bool HasAny(Expression<Func<T, bool>> filterClause);
        Task Update(IEnumerable<T> entities);
        bool Update(T entity);
        Task UpdateAsync(T entity);
    }
}
