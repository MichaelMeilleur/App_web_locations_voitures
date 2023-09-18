using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static TP1.Core.Domain.Entities.Locations.Conducteur;
using static TP1.Core.Domain.Entities.Locations.Voiture;
using System.Linq;
using TP1.Core;
using Microsoft.AspNetCore.Mvc.Rendering;
using FluentValidation;

namespace TP1.WebSite.Models.Locations.Location
{
    public class LocationCreateM
    {
        [Display(Name = "Prenom")]
        public string Prenom { get; set; }
        [Required]
        [Display(Name = "Succursale")]
        public Guid SuccursaleId { get; set; }
        [Display(Name = "Nom")]
        public string Nom { get; set; }
        [Display(Name = "Numero Civique")]
        public int NumeroCivique_Conducteur { get; set; }
        [Display(Name = "Rue")]
        public string Rue_Conducteur { get; set; }
        [Display(Name = "Ville")]
        public string Ville_Conducteur { get; set; }
        [Display(Name = "Code Postal")]
        public string CodePostal_Conducteur { get; set; }
        [Display(Name = "Courriel")]
        public string Courriel { get; set; }
        [Display(Name = "Telephone")]
        public string Telephone { get; set; }
        [Display(Name = "Num. Permis de conduire")]
        public string PermisDeConduire { get; set; }
        [Display(Name = "Informations (Client et preuve assurance) valides?")]
        public bool Valide { get; set; } = false;
        public string? Note { get; set; }

        //Locations
        [Display(Name = "Date et heure d'ouverture planifié")]
        public DateTime OuverturePlanifie { get; set; }
        [Display(Name = "Date et heure de fermeture  planifié")]
        public DateTime FermeturePlanifie { get; set; }

        //Voiture
        [Required]
        [Display(Name = "Voiture")]
        public Guid IdVoiture { get; set; }
        public IEnumerable<SelectListItem> Voitures { get; set; }

        public class LocationCreateMValidator : AbstractValidator<LocationCreateM>
        {
            public LocationCreateMValidator()
            {
                RuleFor(x => x.Prenom)
                    .NotEmpty().WithMessage("Le prénom est requis.")
                    .Length(3, 30).WithMessage("Le prénom doit être entre 3 et 30 caractères.")
                    .Matches("^[a-zA-Z]+$").WithMessage("Le prénom ne peut contenir que des lettres alphabétiques.");

                RuleFor(x => x.Nom)
                    .NotEmpty().WithMessage("Le nom est requis.")
                    .Length(3, 50).WithMessage("Le nom doit être entre 3 et 50 caractères.")
                    .Matches("^[a-zA-Z]+$").WithMessage("Le nom ne peut contenir que des lettres alphabétiques.");

                RuleFor(x => x.Telephone)
                    .NotEmpty().WithMessage("Le numéro de téléphone est requis.")
                    .Matches(@"^(\(\d{3}\)\s*|\d{3}-)\d{3}-\d{4}$|\d{10}$")
                    .WithMessage("Le numéro de téléphone doit être au format (555) 555-5555, 555-555-5555 ou 5555555555.");

                RuleFor(x => x.Courriel)
                    .NotEmpty().WithMessage("L'adresse courriel est requise.")
                    .EmailAddress().WithMessage("L'adresse courriel n'est pas valide.");

                RuleFor(x => x.PermisDeConduire)
                    .NotEmpty().WithMessage("Le numéro de permis de conduire est requis.")
                    .Matches(@"^[A-Za-z]\d{4}(0[1-9]|[12]\d|3[01])(0[1-9]|1[0-2])\d{2}$")
                    .WithMessage("Le numéro de permis de conduire doit être au format A####DDMMYY##, où A est la première lettre du nom de famille, # est un chiffre, DD représente le jour de naissance, MM représente le mois de naissance, et YY représente les deux derniers chiffres de l’année de naissance. Exemple : J1203170399");

                RuleFor(x => x.NumeroCivique_Conducteur)
                    .NotEmpty().WithMessage("Le numéro civique est requis.")
                    .GreaterThan(0).WithMessage("Le numéro civique doit être un entier positif.");

                RuleFor(x => x.Rue_Conducteur)
                    .NotEmpty().WithMessage("Le nom de la rue est requis.")
                    .Length(5, 30).WithMessage("Le nom de la rue doit être entre 5 et 30 caractères.")
                    .Matches("^[a-zA-Z]+$").WithMessage("Le nom de la rue ne peut contenir que des lettres alphabétiques.");

                RuleFor(x => x.Ville_Conducteur)
                    .NotEmpty().WithMessage("Le nom de la ville est requis.")
                    .Length(5, 30).WithMessage("Le nom de la ville doit être entre 5 et 30 caractères.")
                    .Matches("^[a-zA-Z]+$").WithMessage("Le nom de la ville ne peut contenir que des lettres alphabétiques.");

                RuleFor(x => x.CodePostal_Conducteur).Matches("^[a-zA-Z]\\d[a-zA-Z]\\s?\\d[a-zA-Z]\\d$|^\\d[a-zA-Z]\\d[a-zA-Z]\\d$").WithMessage("Le code postal doit respecter le format « A0A 0A0 » ou « A0A0A0 ».");


            }
        }
    }
}

