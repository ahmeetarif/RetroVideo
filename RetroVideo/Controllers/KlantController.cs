using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow.ValueContentAnalysis;
using Newtonsoft.Json;
using RetroVideo.Entities;
using RetroVideo.Services.Abstract;
using System.Text;

namespace RetroVideo.Controllers
{
    public class KlantController : Controller
    {
        private readonly IKlantenSM _klantenSM;
        private readonly IFilmSM _filmSM;
        public KlantController(IKlantenSM klantenSM, IFilmSM filmSM)
        {
            _klantenSM = klantenSM;
            _filmSM = filmSM;
        }

        public IActionResult Index(string searchQuery)
        {
            var customers = new List<Klanten>();
            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                customers = _klantenSM.GetList(x => x.Familienaam.Contains(searchQuery));
            }

            return View(customers);
        }

        [HttpGet]
        public IActionResult ConfirmUserReservation(long id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            return View(id);

        }

        [HttpGet]
        public IActionResult CheckConfirm(string message)
        {
            return View("CheckConfirm", message);
        }

        [HttpPost]
        public IActionResult ConfirmUserReservationPOST(long userId)
        {
            if (userId <= 0)
            {
                return NotFound();
            }

            bool isFalseFilmAvailable = false;


            // MARK : Session'da bulunan filmler kontrol edilir.
            var currentBasketItemAsByteArray = HttpContext.Session.Get("BasketItems");

            if (currentBasketItemAsByteArray != null && currentBasketItemAsByteArray.Any())
            {
                var deserializedCurrentFilmsInBasket = JsonConvert.DeserializeObject<List<Film>>(Encoding.UTF8.GetString(currentBasketItemAsByteArray));

                if (deserializedCurrentFilmsInBasket != null && deserializedCurrentFilmsInBasket.Any())
                {
                    var getFilmFromDatabase = _filmSM.GetList(x => deserializedCurrentFilmsInBasket.Select(z => z.Id).Contains(x.Id)).ToList();
                    var listOfReservaty = new List<Reservaty>();

                    foreach (var item in getFilmFromDatabase)
                    {
                        var checkAvailability = item.Voorraad - item.Gereserveerd;

                        if (checkAvailability <= 0)
                        {
                            isFalseFilmAvailable = true;
                            break;
                        }

                        item.Gereserveerd++;
                        item.Voorraad -= 1;
                        listOfReservaty.Add(new Reservaty
                        {
                            FilmId = item.Id,
                            KlantId = userId,
                            Reservatie = DateTime.Now
                        });
                    }

                    // MARK : Herhangi bir sorun olmadığı zaman, Reservasyon oluşturup, film'de stock güncellemesi yapılır.
                    if (!isFalseFilmAvailable)
                    {
                        _filmSM.FinishReservation(getFilmFromDatabase, listOfReservaty);
                    }

                }
            }

            if (isFalseFilmAvailable)
            {
                return RedirectToAction("CheckConfirm", "Klant", new { message = "De reservatie is Un Successful" });
            }
            else
            {
                return RedirectToAction("CheckConfirm", "Klant", new { message = "De reservatie is OK" });
            }
        }
    }
}