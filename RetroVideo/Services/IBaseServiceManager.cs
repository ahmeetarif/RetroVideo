using RetroVideo.Data.Abstract;
using RetroVideo.Entities;
using RetroVideo.Models;
using System.Linq.Expressions;

namespace RetroVideo.Services
{
    public interface IBaseServiceManager<TEntity, TResponse>
    where TEntity : class
    where TResponse : class, new()
    {
        #region Properties

        public IBaseDataManager<RetroVideoContext> EFDbContext { get; set; }

        #endregion

        public Task<TResponse> Create(TEntity entity);

        public List<TResponse> GetList();
        List<TResponse> GetList(Expression<Func<TEntity, bool>> predicate);
    }
}