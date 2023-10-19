using RetroVideo.Entities;

namespace RetroVideo.Services.Abstract
{
    public interface IFilmSM : IBaseServiceManager<Film, Film>
    {
        public bool FinishReservation(List<Film> films, List<Reservaty> reservaties);
    }
}