using AutoMapper;
using RetroVideo.Data.Abstract;
using RetroVideo.Entities;
using RetroVideo.Services.Abstract;

namespace RetroVideo.Services.Concrete
{
    public class KlantenSM : BaseServiceManager<Klanten, Klanten>, IKlantenSM
    {
        public KlantenSM(IBaseDataManager<RetroVideoContext> EFDbContext, IMapper mapper) : base(EFDbContext, mapper)
        {
        }
    }
}