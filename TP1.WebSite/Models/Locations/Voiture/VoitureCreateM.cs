using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP1.Core.Domain.Entities.Locations;
using static TP1.Core.Domain.Entities.Locations.Voiture;

namespace TP1.WebSite.Models.Locations.Voiture
{
    // 1 à plusieurs voitures pour une succursale
    public class VoitureCreateM
    {
        [Display(Name = "Succursale")]
        public Guid Id { get; set; }
        public Guid SuccursaleID { get; set; }
        public string Surnom { get; set; }
        [Display(Name = "Status")]
        public StatutV Statutv { get; set; }
        public bool Disponible { get; set; }
        public Etat Etat { get; set; }
        [Display(Name = "Numéro de série")]
        public string NumeroSerie { get; set; }
        [Display(Name = "Numéro d'immatriculation")]
        public string NumeroImmatriculation { get; set; }
        public string Marque { get; set; }
        public string Modele { get; set; }
        public int Annee { get; set; }
        public string Couleur { get; set; }
        public int? Kilometrage { get; set; }
        [Display(Name = "Valeur estimée")]
        public decimal? ValeurEstimee { get; set; }
        public string? Note { get; set; }

    }

    public class VoitureCreateMValidator : AbstractValidator<VoitureCreateM>
    {
        public VoitureCreateMValidator()
        {
            RuleFor(v => v.Marque).NotEmpty().Length(3, 20).Matches("^[a-zA-Z]*$");
            RuleFor(v => v.Modele).NotEmpty().Length(3, 20).Matches("^[a-zA-Z]*$");
            RuleFor(v => v.Couleur).NotEmpty().Length(3, 20).Matches("^[a-zA-Z]*$");
            RuleFor(v => v.Annee).InclusiveBetween(2000, System.DateTime.Now.Year + 1);
            RuleFor(v => v.Kilometrage).GreaterThan(0);
            RuleFor(v => v.ValeurEstimee).GreaterThan(0);
            RuleFor(v => v.NumeroSerie).NotEmpty().Matches("^[A-HJ-NPR-Z0-9]*$");

            RuleFor(v => v.NumeroImmatriculation).NotEmpty().Length(6, 7).Matches("^[a-zA-Z0-9]*$");
        }
    }
}
