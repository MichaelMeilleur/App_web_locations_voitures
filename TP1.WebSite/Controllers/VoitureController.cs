using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using TP1.Core;
using TP1.Core.Domain.Entities.Identité;
using TP1.Core.Domain.Entities.Locations;
using TP1.WebSite.Models.Identité.Utilisateur;
using TP1.WebSite.Models.Locations.Voiture;
using System.Linq;
using TP1.WebSite.Models.Locations.Succursale;

namespace TP1.WebSite.Controllers
{
    [Authorize]
    public class VoitureController : Controller
    {
        private readonly SystemeReservationDbContext context;

        public VoitureController(SystemeReservationDbContext context)
        {
            this.context = context;
        }

        // GET: VoitureController
        public IActionResult Index()
        {
            return RedirectToAction(nameof(Manage));
        }

        // GET: VoitureController/Manage/5
        public IActionResult Manage(Guid succursaleId)
        {
            try
            {
                var succursales = context.Succursales.Select(s => new SuccursaleDetailM()
                {
                    Id = s.Id,
                    Nom = s.Nom
                }).ToList();

                var voitures = context.Voitures
                    .Where(voiture => voiture.SuccursaleId == succursaleId)
                    .Select(voiture => new VoitureDetailM()
                    {
                        Id = voiture.Id,
                        Annee = voiture.Annee,
                        Couleur = voiture.Couleur,
                        Marque = voiture.Marque,
                        Modele = voiture.Modele,
                        NumeroImmatriculation = voiture.NumeroImmatriculation,
                        Surnom = voiture.Surnom,
                        Statutv = voiture.statut
                    });

                var viewModel = new VoitureSuccursaleVM()
                {
                    SuccursaleId = succursaleId,
                    Succursales = succursales,
                    Voitures = voitures.ToList()
                };

                return View(viewModel);
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "SVP veuillez essayer de nouveau!");
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult ConsulterProfil(Guid id)
        {
            try
            {
                var voiture = context.Voitures.Where(x => x.Id == id).FirstOrDefault();
                var voitureDetail = new List<VoitureDetailM> { new VoitureDetailM
                {
                    Id = voiture.Id,
                    Annee = voiture.Annee,
                    Couleur = voiture.Couleur,
                    Marque = voiture.Marque,
                    Modele = voiture.Modele,
                    NumeroImmatriculation = voiture.NumeroImmatriculation,
                    Surnom = voiture.Surnom,
                    SuccursaleId = voiture.SuccursaleId,
                    Etat = voiture.etat,
                    Statutv = voiture.statut,
                    ValeurEstimee = voiture.ValeurEstimee != null ? (decimal)voiture.ValeurEstimee : default(decimal),
                    Kilometrage = voiture.Kilometrage != null ? (int)voiture.Kilometrage : default(int),
                    Disponible = voiture.Disponible,
                    NumeroSerie = voiture.NumeroSerie,
                    liste = context.Note.Where(n => n.VoitureId == id).ToList() ?? new List<Note>()
                }};


                return View(voitureDetail);
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "SVP veuillez essayer de nouveau!");
                return RedirectToAction("Manage", "Voiture");
            }
        }

        // GET: VoitureController/Create
        [Authorize(Roles = "Administrateur,Gérant")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: VoitureController/Create
        [HttpPost]
        [Authorize(Roles = "Administrateur,Gérant")]
        public IActionResult Create(VoitureCreateM voitureCreateVM)
        {
            if (!ModelState.IsValid)
            {
                return View(voitureCreateVM);
            }

            if (context.Voitures.Any(v => v.NumeroSerie == voitureCreateVM.NumeroSerie))
            {
                ModelState.AddModelError(string.Empty, "Le numéro de série existe déjà, veuillez choisir un autre numéro de série.");
                return View(voitureCreateVM);
            }

            try
            {
                var newVoiture = new Voiture()
                {
                    Id = Guid.NewGuid(),
                    Annee = voitureCreateVM.Annee,
                    Couleur = voitureCreateVM.Couleur,
                    Disponible = voitureCreateVM.Disponible,
                    etat = voitureCreateVM.Etat,
                    Marque = voitureCreateVM.Marque,
                    Modele = voitureCreateVM.Modele,
                    NumeroImmatriculation = voitureCreateVM.NumeroImmatriculation,
                    NumeroSerie = voitureCreateVM.NumeroSerie,
                    statut = voitureCreateVM.Statutv,
                    Surnom = voitureCreateVM.Surnom,
                    SuccursaleId = voitureCreateVM.SuccursaleID,
                    Note = new List<Note>(),
                };

                if (!string.IsNullOrEmpty(voitureCreateVM.Note))
                {
                    newVoiture.Note.Add(new Note()
                    {
                        Id = Guid.NewGuid(),
                        Texte = voitureCreateVM.Note,
                        VoitureId = newVoiture.Id
                    });
                }


                if (voitureCreateVM.Kilometrage.HasValue)
                {
                    newVoiture.Kilometrage = (int)voitureCreateVM.Kilometrage;
                }

                if (voitureCreateVM.ValeurEstimee.HasValue)
                {
                    newVoiture.ValeurEstimee = (decimal)voitureCreateVM.ValeurEstimee;
                }

                context.Add(newVoiture);
                context.SaveChanges();
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "SVP veuillez essayer de nouveau!");
                return View(voitureCreateVM);
            }
            return RedirectToAction("Manage", "Voiture");
        }

        public IActionResult CreateNote()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateNote(VoitureCreateNoteVM vm, Guid id)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            try
            {
                var voiture = context.Voitures.Find(id);

                if (voiture != null)
                {

                    Note toAdd = new Note()
                    {
                        Id = Guid.NewGuid(),
                        Texte = vm.NoteAjouter,
                        VoitureId = voiture.Id,
                    };
                    context.Note.Add(toAdd);
                    voiture.Note.Add(toAdd);
                    context.SaveChanges();
                }
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "SVP veuillez essayer de nouveau!");
                return View(vm);
            }

            return RedirectToAction("ConsulterProfil", "Voiture", new { id });
        }

        [Authorize(Roles = "Administrateur,Gérant")]
        public IActionResult ActiverVoiture(Guid id, bool newStatus)
        {
            try
            {
                var voiture = context.Voitures.Find(id);

                if (voiture is null)
                    throw new ArgumentOutOfRangeException(nameof(id));

                voiture.statut = Voiture.StatutV.Activé;

                context.SaveChanges();
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "SVP veuillez essayer de nouveau!");
                return RedirectToAction("Manage", "Voiture");
            }
            return RedirectToAction("Manage", "Voiture");
        }

        [Authorize]
        public IActionResult DesactiverVoiture(Guid id, bool newStatus)
        {
            try
            {
                var voiture = context.Voitures.Find(id);

                if (voiture is null)
                    throw new ArgumentOutOfRangeException(nameof(id));

                voiture.statut = Voiture.StatutV.Désactivé;

                context.SaveChanges();
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "SVP veuillez essayer de nouveau!");
                return RedirectToAction("Manage", "Voiture");
            }

            return RedirectToAction("Manage", "Voiture");
        }

        [Authorize(Roles = "Administrateur,Gérant")]
        public IActionResult ArchiverVoiture(Guid id, bool newStatus)
        {
            try
            {
                var voiture = context.Voitures.Find(id);

                if (voiture is null)
                    throw new ArgumentOutOfRangeException(nameof(id));

                voiture.statut = Voiture.StatutV.Archivé;

                context.SaveChanges();
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "SVP veuillez essayer de nouveau!");
                return RedirectToAction("Manage", "Voiture");
            }

            return RedirectToAction("Manage", "Voiture");
        }

    }
}
