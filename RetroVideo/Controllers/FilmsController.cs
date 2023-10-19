using Microsoft.AspNetCore.Mvc;
using RetroVideo.Services.Abstract;

namespace RetroVideo.Controllers
{
    public class FilmsController : Controller
    {
        private readonly IFilmSM _filmSM;
        public FilmsController(IFilmSM filmSM)
        {
            _filmSM = filmSM;
        }
        public IActionResult Index(long id)
        {
            var getFilm = _filmSM.GetList(x => x.Id == id).FirstOrDefault();
            if (getFilm == null)
            {
                return NotFound();
            }

            return View(getFilm);
        }
    }
}