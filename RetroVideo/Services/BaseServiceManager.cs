using AutoMapper;
using RetroVideo.Data.Abstract;
using RetroVideo.Entities;
using RetroVideo.Models;
using System.Linq.Expressions;

namespace RetroVideo.Services
{
    public class BaseServiceManager<TEntity, TResponse> : IBaseServiceManager<TEntity, TResponse>
        where TEntity : class
        where TResponse : class, new()
    {
        public IBaseDataManager<RetroVideoContext> EFDbContext { get; set; }
        protected IMapper Mapper { get; set; }
        public BaseServiceManager(IBaseDataManager<RetroVideoContext> EFDbContext,
            IMapper mapper)
        {
            this.EFDbContext = EFDbContext;
            this.Mapper = mapper;
        }

        public async Task<TResponse> Create(TEntity entity)
        {
            try
            {
                await EFDbContext.AddAsync<TEntity>(entity);
                EFDbContext.Context.SaveChanges();

                var mappedResponse = Mapper.Map<TEntity, TResponse>(entity);

                return mappedResponse;
            }
            catch (Exception ex)
            {
                return new TResponse();
            }
        }

        public List<TResponse> GetList()
        {
            try
            {
                var getDataAsQueryable = EFDbContext.List<TEntity>(x => true).ToList();

                var mappedData = Mapper.Map<List<TEntity>, List<TResponse>>(getDataAsQueryable);


                return mappedData;
            }
            catch (Exception ex)
            {
                return new List<TResponse>();
            }
        }

        public List<TResponse> GetList(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                var getDataAsQueryable = EFDbContext.List<TEntity>(predicate).ToList();

                var mappedData = Mapper.Map<List<TEntity>, List<TResponse>>(getDataAsQueryable);

                return mappedData;
            }
            catch (Exception ex)
            {
                return new List<TResponse>();
            }
        }

    }
}