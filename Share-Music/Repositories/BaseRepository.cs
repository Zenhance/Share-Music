using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Share_Music.Data;
using Share_Music.Models;
using System.Linq.Expressions;

namespace Share_Music.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        private readonly MusicDbContext musicDbContext;

        public BaseRepository(MusicDbContext musicDbContext)
        {
            this.musicDbContext = musicDbContext;
        }
        public bool Create(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public bool Create(T entity)
        {
            throw new NotImplementedException();
        }

        public async Task CreateAsync(T entity)
        {
            musicDbContext.Set<T>().Add(entity);
            await musicDbContext.SaveChangesAsync();
        }

        public Task CreateAsync(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public bool Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetByFilter(Expression<Func<T, bool>> filterClause)
        {
            return musicDbContext.Set<T>().Where(filterClause).ToList();
        }

        public async Task<IEnumerable<T>> GetByFilterAsync(Expression<Func<T, bool>> filterClause)
        {
            return await musicDbContext.Set<T>().Where(filterClause).ToListAsync();
        }

        public T GetById(string id)
        {
            throw new NotImplementedException();
        }

        public bool HasAny(Expression<Func<T, bool>> filterClause)
        {
           return musicDbContext.Set<T>().Any(filterClause);
        }

        public Task Update(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public bool Update(T entity)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(T entity)
        {
            musicDbContext.Set<T>().Update(entity);
            await musicDbContext.SaveChangesAsync();
        }
    }
}
