using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using TP1.Core;
using TP1.Core.Domain.Entities.Identité;
using TP1.Core.Domain.Entities.Locations;
using TP1.WebSite.Models.Identité.Utilisateur;
using TP1.WebSite.Models.Locations;
using TP1.WebSite.Models.Locations.Conducteurs;
using TP1.WebSite.Models.Locations.Location;
using TP1.WebSite.Models.Locations.Succursale;
using TP1.WebSite.Models.Locations.Voiture;
using static TP1.Core.Domain.Entities.Locations.Location;
using Location = TP1.Core.Domain.Entities.Locations.Location;

namespace TP1.WebSite.Controllers
{
    [Authorize]
    public class LocationController : Controller
    {
        private readonly SystemeReservationDbContext context;

        public LocationController(SystemeReservationDbContext context)
        {
            this.context = context;
        }

        // GET: LocationController/Manage/5
        public IActionResult Manage(Guid succursaleId)
        {
            try
            {
                var succursales = context.Succursales.Select(s => new SuccursaleDetailM()
                {
                    Id = s.Id,
                    Nom = s.Nom
                }).ToList();

                var locations = context.Locations
                    .Where(locations => locations.SuccursaleId == succursaleId)
                    .Select(locations => new LocationDetailM
                    {
                        ID = locations.Id,
                        CodePostal_Conducteur = locations.Conducteur.Adresse.CodePostal,
                        Nom = locations.Conducteur.Nom,
                        Prenom = locations.Conducteur.Prenom,
                        Rue_Conducteur = locations.Conducteur.Adresse.Rue,
                        Ville_Conducteur = locations.Conducteur.Adresse.Ville,
                        NumeroCivique_Conducteur = locations.Conducteur.Adresse.NumeroCivique,
                        NomVoiture = locations.voiture.Surnom,
                        OuverturePlanifie = locations.OuverturePlanifie,
                        FermeturePlanifie = locations.FermeturePlanifie,
                        Statut = (LocationDetailM.Etat)locations.statut
                    }).ToList();

                Guid v_id = locations.Select(x => x.IdVoiture).FirstOrDefault();

                var conducteurs = context.Conducteurs.Where(x => x.VoitureId == v_id).Select(conducteurs => new ConducteurViewM
                {
                    Prenom = conducteurs.Prenom,
                    Nom = conducteurs.Nom,
                    Adresse = conducteurs.Adresse.Ville,
                    CodeCiviv = conducteurs.Adresse.CodePostal,
                    Rue = conducteurs.Adresse.Rue
                }).ToList();

                var voitures = context.Voitures.Where(x => x.Id == locations.Select(x => x.IdVoiture).FirstOrDefault()).Select(voitures => new VoitureDetailM
                {
                    Surnom = voitures.Surnom
                }
                ).ToList();

                var viewModel = new LocationSuccursaleVM()
                {
                    SuccursaleId = succursaleId,
                    Succursales = succursales,
                    Conducteurs = conducteurs,
                    Voitures = voitures,
                    Locations = locations.ToList(),
                };

                return View(viewModel);
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "SVP veuillez essayer de nouveau!");
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: LocationController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LocationController/Create
        [HttpPost]
        public IActionResult Create(LocationCreateM vm)
        {
            try
            {
                Voiture voiture = context.Voitures.Find(vm.IdVoiture);
                voiture.Disponible = false;

                if (voiture != null)
                {


                    var newConducteur = new Conducteur()
                    {
                        Id = Guid.NewGuid(),
                        VoitureId = voiture.Id,
                        Nom = vm.Nom,
                        Prenom = vm.Prenom,
                        Telephone = vm.Telephone,
                        Courriel = vm.Courriel,
                        PermisDeConduire = vm.PermisDeConduire,
                        InformationsValides = vm.Valide,
                        Voiture = voiture
                    };
                    newConducteur.Adresse = (new Adresse()
                    {
                        ID = Guid.NewGuid(),
                        NumeroCivique = (int)vm.NumeroCivique_Conducteur,
                        Rue = vm.Rue_Conducteur,
                        CodePostal = vm.CodePostal_Conducteur,
                        Ville = vm.Ville_Conducteur,
                        ConducteurID = newConducteur.Id,

                    });

                    var newLocation = new Location()
                    {
                        Id = Guid.NewGuid(),
                        statut = Statut.ouvert,
                        OuverturePlanifie = vm.OuverturePlanifie,
                        FermeturePlanifie = vm.FermeturePlanifie,
                        Conducteur = newConducteur,
                        SuccursaleId = voiture.SuccursaleId,
                        voiture = voiture,
                        Note = new List<Note>()
                    };

                    if (!string.IsNullOrEmpty(vm.Note))
                    {
                        newLocation.Note.Add(new Note()
                        {
                            Id = Guid.NewGuid(),
                            Texte = vm.Note,
                            LocationId = newLocation.Id
                        });
                    }

                    context.Add(newConducteur);
                    context.Add(newLocation);
                    context.SaveChanges();
                }
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "SVP veuillez essayer de nouveau!");
                return View(vm);
            }

            return RedirectToAction(nameof(Manage));
        }

        [HttpGet]
        public IActionResult GetCarsBySuccursaleId(Guid succursaleId)
        {
            try
            {
                var cars = context.Voitures
                    .Where(x => x.SuccursaleId == succursaleId)
                    .Select(x => new
                    {
                        Id = x.Id,
                        Surnom = x.Surnom
                    })
                    .ToList();

                return Json(cars);
            }
            catch (Exception ex)
            {
                // Handle the exception
                return StatusCode(500, "An error occurred while retrieving the cars.");
            }
        }

        public IActionResult ConsulterLocation(Guid id)
        {
            try
            {
                var location = context.Locations.Where(x => x.Id == id).FirstOrDefault();

                var voiture = context.Voitures.Where(x => x.LocationId == id).FirstOrDefault();

                var conducteur = context.Conducteurs.Where(x => x.VoitureId == voiture.Id).FirstOrDefault();

                var adresse = context.Adresses.Where(x => x.ConducteurID == conducteur.Id).FirstOrDefault();

                var locationdetail = new List<LocationDetailM> { new LocationDetailM
                {
                    ID = location.Id,
                    Statut = (LocationDetailM.Etat)location.statut,
                    OuverturePlanifie = location.OuverturePlanifie,
                    FermeturePlanifie = location.FermeturePlanifie,
                    OuvertureReel = location.OuvertureReel == null ? location.OuverturePlanifie : location.OuvertureReel,
                    FermetureReel = location.FermetureReel == null ? location.FermeturePlanifie : location.FermetureReel,
                    SuccursaleId = voiture.SuccursaleId,
                    Prenom = conducteur.Prenom,
                    Nom =  conducteur.Nom,
                    NumeroCivique_Conducteur = conducteur.Adresse.NumeroCivique,
                    Rue_Conducteur = adresse.Rue,
                    Ville_Conducteur = adresse.Ville,
                    CodePostal_Conducteur = adresse.CodePostal,
                    Courriel = conducteur.Courriel,
                    Telephone = conducteur.Telephone,
                    PermisDeConduire = conducteur.PermisDeConduire,
                    IdVoiture = voiture.Id,
                    NomVoiture = voiture.Surnom,
                    liste = context.Note.Where(n => n.LocationId == id).ToList() ?? new List<Note>()
                }};

                return View(locationdetail);
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "SVP veuillez essayer de nouveau!");
                return RedirectToAction("Manage", "Location");
            }
        }
        public IActionResult CreateNoteLocation()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateNoteLocation(LocationCreateNoteVM vm, Guid id)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            try
            {
                var location = context.Locations.Find(id);

                if (location != null)
                {

                    Note toAdd = new Note()
                    {
                        Id = Guid.NewGuid(),
                        Texte = vm.NoteAjouter,
                        LocationId = location.Id,
                    };
                    context.Note.Add(toAdd);
                    location.Note.Add(toAdd);
                    context.SaveChanges();
                }
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "SVP veuillez essayer de nouveau!");
                return View(vm);
            }

            return RedirectToAction("ConsulterLocation", "Location", new { id });
        }

        public IActionResult FermerLocation()
        {
            return View();
        }

        [HttpPost]
        public IActionResult FermerLocation(FermerLocationVM vm, Guid id)
        {
            try
            {
                var location = context.Locations.Find(id);

                if (location != null)
                {

                    location.FermetureReel = vm.DateFermeture;
                    location.statut = Statut.fermé;

                    if (vm.NoteAjouter != null)
                    {
                        Note toAdd = new Note()
                        {
                            Id = Guid.NewGuid(),
                            Texte = vm.NoteAjouter,
                            LocationId = location.Id,
                        };
                        context.Note.Add(toAdd);
                        location.Note.Add(toAdd);
                    }

                    context.SaveChanges();
                }

            }
            catch
            {
                ModelState.AddModelError(string.Empty, "SVP veuillez essayer de nouveau!");
                return View(vm);
            }

               return RedirectToAction("Manage", "Location");
        }

    }
}
