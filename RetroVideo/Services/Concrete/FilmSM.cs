using AutoMapper;
using RetroVideo.Data.Abstract;
using RetroVideo.Entities;
using RetroVideo.Services.Abstract;

namespace RetroVideo.Services.Concrete
{
    public class FilmSM : BaseServiceManager<Film, Film>, IFilmSM
    {
        public FilmSM(IBaseDataManager<RetroVideoContext> EFDbContext, IMapper mapper) : base(EFDbContext, mapper)
        {
        }

        public bool FinishReservation(List<Film> films, List<Reservaty> reservaties)
        {
            try
            {
                using (var transaction = EFDbContext.Context.Database.BeginTransaction())
                {

                    EFDbContext.Context.Films.UpdateRange(films);
                    EFDbContext.Context.Reservaties.AddRange(reservaties);

                    EFDbContext.Context.SaveChanges();
                    transaction.Commit();
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}