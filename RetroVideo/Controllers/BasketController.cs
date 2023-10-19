using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RetroVideo.Entities;
using RetroVideo.Services.Abstract;
using System.Text;

namespace RetroVideo.Controllers
{
    public class BasketController : Controller
    {
        private readonly IFilmSM _filmSM;
        public BasketController(IFilmSM filmSM)
        {
            _filmSM = filmSM;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddToBasket(long id)
        {
            var films = _filmSM.GetList(x => x.Id == id);

            if (films == null || !films.Any())
            {
                return NotFound();
            }

            var currentBasketItemAsByteArray = HttpContext.Session.Get("BasketItems");

            if (currentBasketItemAsByteArray != null && currentBasketItemAsByteArray.Any())
            {
                var deserializedCurrentFilmsInBasket = JsonConvert.DeserializeObject<List<Film>>(Encoding.UTF8.GetString(currentBasketItemAsByteArray));

                if (deserializedCurrentFilmsInBasket != null && deserializedCurrentFilmsInBasket.Any())
                {
                    if (deserializedCurrentFilmsInBasket?.Any(x => x.Id == id) ?? false)
                    {
                        return RedirectToAction("Index");
                    }

                    films.AddRange(deserializedCurrentFilmsInBasket);
                }
            }

            var seriazliedObject = JsonConvert.SerializeObject(films);

            HttpContext.Session.Set("BasketItems", Encoding.UTF8.GetBytes(seriazliedObject));

            return RedirectToAction("Index");
        }


        [HttpPost]
        public IActionResult RemoveFromBasket(List<long> ids)
        {
            var currentBasketItemAsByteArray = HttpContext.Session.Get("BasketItems");

            if (currentBasketItemAsByteArray != null && currentBasketItemAsByteArray.Any())
            {
                var deserializedCurrentFilmsInBasket = JsonConvert.DeserializeObject<List<Film>>(Encoding.UTF8.GetString(currentBasketItemAsByteArray));

                if (deserializedCurrentFilmsInBasket != null && deserializedCurrentFilmsInBasket.Any())
                {
                    foreach (var item in ids)

                        deserializedCurrentFilmsInBasket = deserializedCurrentFilmsInBasket.Where(x => x.Id != item).ToList();
                }

                var seriazliedObject = JsonConvert.SerializeObject(deserializedCurrentFilmsInBasket);

                HttpContext.Session.Set("BasketItems", Encoding.UTF8.GetBytes(seriazliedObject));
            }

            return RedirectToAction("Index");
        }

    }
}