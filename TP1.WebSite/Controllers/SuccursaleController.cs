using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using TP1.Core;
using TP1.Core.Domain.Entities.Identité;
using TP1.Core.Domain.Entities.Locations;
using TP1.WebSite.Models.Locations;
using TP1.WebSite.Models.Locations.Succursale;
using TP1.WebSite.Models.Locations.Voiture;
using static TP1.Core.Domain.Entities.Locations.Succursale;


namespace TP1.WebSite.Controllers
{
    [Authorize(Roles = "Administrateur")]
    public class SuccursaleController : Controller
    {
        private readonly SystemeReservationDbContext context;
        private readonly UserManager<Utilisateur> userManager;

        public SuccursaleController(SystemeReservationDbContext context, UserManager<Utilisateur> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }


        // GET: SuccursaleController/Manage/5
        public IActionResult Manage()
        {
            var succursales = context.Succursales
                .Select(succursale => new SuccursaleDetailM
                {
                    Nom = succursale.Nom,
                    Statut = succursale.Statut,
                    Id = succursale.Id  
                });

            var voitures = context.Voitures
                    .Select(voiture => new VoitureDetailM()
                    {
                        Id = voiture.Id,
                        Annee = voiture.Annee,
                        Couleur = voiture.Couleur,
                        Marque = voiture.Marque,
                        Modele = voiture.Modele,
                        Disponible = voiture.Disponible,
                        SuccursaleId = voiture.SuccursaleId,
                        NumeroImmatriculation = voiture.NumeroImmatriculation,
                        Surnom = voiture.Surnom,
                        Statutv = voiture.statut
                    });

            var viewModel = new VoitureSuccursaleVM()
            {
                Succursales = succursales,
                Voitures = voitures.ToList()
            };

            return View(viewModel);
        }

        // GET: SuccursaleController/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: SuccursaleController/Create
        [HttpPost]
        [Authorize]
        public IActionResult Create(SuccursaleCreateM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            try
            {
                var toAdd = new Succursale()
                {
                    Id = Guid.NewGuid(),                 
                    Nom = vm.NomSuccursale,
                    Statut = true
                };

                if (vm.NumeroCivique_Succcursale > 0 && !string.IsNullOrEmpty(vm.Rue_Succcursale) && !string.IsNullOrEmpty(vm.CodePostal_Succcursale) && !string.IsNullOrEmpty(vm.Ville_Succcursale))
                {
                    toAdd.adresse = (new Adresse()
                    {
                        NumeroCivique = (int)vm.NumeroCivique_Succcursale,
                        Rue = vm.Rue_Succcursale,
                        CodePostal = vm.CodePostal_Succcursale,
                        Ville = vm.Ville_Succcursale
                    });
                }

                context.Add(toAdd);
                context.SaveChanges();
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "SVP veuillez essayer de nouveau!");
                return View(vm);
            }

            return RedirectToAction("Manage", "Succursale");
        }


        public IActionResult UpdateStatus(Guid id, bool newStatus)
        {
            var succursale = context.Succursales.Find(id);

            if (succursale is null)
                throw new ArgumentOutOfRangeException(nameof(id));

            if (succursale.Statut == true)
            {
                succursale.Statut = false;
            }
            else
                succursale.Statut = true;

            context.SaveChanges();

            return RedirectToAction("Manage", "Succursale");
        }
    }
}
