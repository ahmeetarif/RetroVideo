using Microsoft.EntityFrameworkCore;
using RetroVideo.Data.Abstract;
using RetroVideo.Entities;
using System.Linq.Expressions;

namespace RetroVideo.Data.Concrete
{
    public class BaseDataManager : IBaseDataManager<RetroVideoContext>
    {
        public RetroVideoContext Context { get; set; }

        public BaseDataManager(RetroVideoContext _context)
        {
            this.Context = _context;
        }

        public async Task<T> AddAsync<T>(T entity) where T : class
        {
            Context.Entry<T>(entity).State = EntityState.Added;
            await Context.Set<T>().AddAsync(entity);
            return entity;
        }

        public async Task<T?> GetAsync<T>(long id) where T : class
        {
            return await Context.Set<T>().FindAsync(id);
        }

        public async Task<T?> GetAsync<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return await Context.Set<T>().FirstOrDefaultAsync(predicate);
        }

        public IEnumerable<T> List<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return Context.Set<T>().Where(predicate).AsEnumerable();
        }

        public Task<bool> RemoveSoftAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<T> UpdateAsync<T>(T entity) where T : class
        {
            throw new NotImplementedException();
        }
    }
}
