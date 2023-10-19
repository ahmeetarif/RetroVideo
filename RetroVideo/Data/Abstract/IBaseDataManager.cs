using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace RetroVideo.Data.Abstract
{
    public interface IBaseDataManager<DBContext>
        where DBContext : DbContext
    {
        public DBContext Context { get; set; }

        public Task<T?> GetAsync<T>(long id) where T : class;
        public Task<T?> GetAsync<T>(Expression<Func<T, bool>> predicate) where T : class;

        public IEnumerable<T> List<T>(Expression<Func<T, bool>> predicate) where T : class;

        public Task<T> AddAsync<T>(T entity) where T : class;

        public Task<T> UpdateAsync<T>(T entity) where T : class;

        public Task<bool> RemoveSoftAsync(long id);
    }
}