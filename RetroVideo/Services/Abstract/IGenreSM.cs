using RetroVideo.Entities;

namespace RetroVideo.Services.Abstract
{
    public interface IGenreSM : IBaseServiceManager<Genre, Genre>
    {
        public Task<Genre?> GetById(long id);
    }
}