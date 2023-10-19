using AutoMapper;
using RetroVideo.Data.Abstract;
using RetroVideo.Entities;
using RetroVideo.Services.Abstract;

namespace RetroVideo.Services.Concrete
{
    public class GenreSM : BaseServiceManager<Genre, Genre>, IGenreSM
    {
        public GenreSM(IBaseDataManager<RetroVideoContext> EFDbContext, IMapper mapper) : base(EFDbContext, mapper)
        {
        }


        public async Task<Genre?> GetById(long id)
        {
            return await EFDbContext.GetAsync<Genre>(id);
        }
    }
}