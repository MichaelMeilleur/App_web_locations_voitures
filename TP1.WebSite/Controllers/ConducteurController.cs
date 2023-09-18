using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TP1.Core;
using TP1.Core.Domain.Entities.Locations;
using TP1.WebSite.Models.Locations;
using TP1.WebSite.Models.Locations.Conducteurs;
using TP1.WebSite.Models.Locations.Location;
using TP1.WebSite.Models.Locations.Succursale;
using TP1.WebSite.Models.Locations.Voiture;
using static TP1.Core.Domain.Entities.Locations.Conducteur;

namespace TP1.WebSite.Controllers
{
    [Authorize]
    public class ConducteurController : Controller
    {
        private readonly SystemeReservationDbContext context;

        public ConducteurController(SystemeReservationDbContext context)
        {
            this.context = context;
        }
   
        public IActionResult Index()
        {
            var conducteurs = context.Conducteurs
                .Select(conducteur => new ConducteurViewM
                {
                    Id = conducteur.Id,
                    Prenom = conducteur.Prenom,
                    Nom = conducteur.Nom,
                    Courriel = conducteur.Courriel,
                    Telephone = conducteur.Telephone,
                    PermisDeConduire = conducteur.PermisDeConduire,
                    NbLocations = context.Locations.Count(l => l.Conducteur.Nom == conducteur.Nom),
                    DerniereLocation = context.Locations.Where(l => l.Conducteur.Nom 
                    == conducteur.Nom).OrderByDescending(l => l.OuverturePlanifie).
                    FirstOrDefault().OuverturePlanifie
                });
            return View(conducteurs);
        }
        public IActionResult ConsulterProfilConducteur(Guid id)
        {
            try
            {
                var conducteur = context.Conducteurs.Where(x => x.Id == id).FirstOrDefault();
                var location = context.Locations.Where(x => x.Conducteur.Id == id).FirstOrDefault();
                var voiture = context.Voitures.Where(x => x.LocationId == location.Id).FirstOrDefault();
                var succursale = context.Succursales.Where(x => x.Id == voiture.SuccursaleId).FirstOrDefault();

                var conducteurDetail = new List<ConducteurViewM> { new ConducteurViewM
                {
                    Nom = conducteur.Nom,
                    Prenom = conducteur.Prenom,
                    Courriel = conducteur.Courriel,
                    PermisDeConduire = conducteur.PermisDeConduire,
                    statut = location.statut,
                    OuverturePlanifie = location.OuverturePlanifie,
                    FermeturePlanifie = location.FermeturePlanifie,
                    NomVoiture = voiture.Surnom,
                    NomSuccursale = succursale.Nom,                  
                }};

                return View(conducteurDetail);
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "SVP veuillez essayer de nouveau!");
                return RedirectToAction("Index", "Conducteurs");
            }
        }
    }
}
