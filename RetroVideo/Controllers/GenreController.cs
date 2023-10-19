using Microsoft.AspNetCore.Mvc;
using RetroVideo.Entities;
using RetroVideo.Services.Abstract;
using System.Text;

namespace RetroVideo.Controllers
{
    public class GenreController : Controller
    {
        private readonly IGenreSM _genreSM;
        private readonly IFilmSM _filmSM;
        public GenreController(IGenreSM genreSM, IFilmSM filmSM)
        {
            _genreSM = genreSM;
            _filmSM = filmSM;
        }

        public IActionResult Index(long? id)
        {
            List<Film> films = new();
            List<Genre> genres = new();

            if (id > 0)
            {
                HttpContext.Session.SetInt32("SelectedGenreId", (int)id);
                films = _filmSM.GetList(x => x.GenreId == id);
            }

            genres = _genreSM.GetList();

            var tupleOfReturn = new Tuple<List<Genre>, List<Film>>(genres, films);

            return View(tupleOfReturn);
        }

        public IActionResult Genre(long id)
        {
            var films = _filmSM.GetList(x => x.GenreId == id);
            return View(films);
        }
    }
}