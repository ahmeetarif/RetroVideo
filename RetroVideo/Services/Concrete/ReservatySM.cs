using AutoMapper;
using RetroVideo.Data.Abstract;
using RetroVideo.Entities;
using RetroVideo.Services.Abstract;

namespace RetroVideo.Services.Concrete
{
    public class ReservatySM : BaseServiceManager<Reservaty, Reservaty>, IReservatySM
    {
        public ReservatySM(IBaseDataManager<RetroVideoContext> EFDbContext, IMapper mapper) : base(EFDbContext, mapper)
        {
        }
    }
}