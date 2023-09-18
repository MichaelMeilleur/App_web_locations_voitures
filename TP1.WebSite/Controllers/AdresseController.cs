using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TP1.Core;
using TP1.WebSite.Models.Locations.Adresse;

namespace TP1.WebSite.Controllers
{
    [Authorize]
    public class AdresseController : Controller
    {
        private readonly SystemeReservationDbContext _context;

        public AdresseController(SystemeReservationDbContext context)
        {
            this._context = context;
        }

        // GET: AdresseController/Manage/5
        public ActionResult Manage(int succursaleId)
        {
            var succursale = _context.Succursales.Find(succursaleId);

            if (succursale is null )
                throw new ArgumentOutOfRangeException(nameof(succursaleId));

            var ads = _context.Adresses
                .Select(adresse => new AdresseDetailM()
                {
                    //Id = adresse.Id,
                    NumeroCivique = adresse.NumeroCivique,
                    Rue = adresse.Rue,
                    Ville = adresse.Ville,
                    CodePostal = adresse.CodePostal,
                });

            ViewBag.SuccursalesId = succursaleId;
            return View(ads);
        }

        // GET: AdresseController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AdresseController/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AdresseController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AdresseController/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
