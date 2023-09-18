using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TP1.Core.Domain.Entities.Locations.Succursale;
using static TP1.Core.Domain.Entities.Locations.Voiture;

namespace TP1.WebSite.Models.Locations.Succursale
{
    public class SuccursaleCreateM
    {
        [Required]
        public string NomSuccursale { get; set; }

        [Display(Name = "Numero Civique")]
        public int? NumeroCivique_Succcursale { get; set; }
        [Display(Name = "Rue")]
        public string? Rue_Succcursale { get; set; }
        [Display(Name = "Ville")]
        public string? Ville_Succcursale { get; set; }
        [Display(Name = "Code Postal")]
        public string? CodePostal_Succcursale { get; set; }
    }
    public class SuccursaleCreateMValidator : AbstractValidator<SuccursaleCreateM>
    {
        public SuccursaleCreateMValidator()
        {
            RuleFor(x => x.NomSuccursale)
                .NotEmpty().WithMessage("Le nom de la succursale est requis.")
                .Length(5, 20).WithMessage("Le nom de la succursale doit contenir entre 5 et 20 caractères.")
                .Matches(@"^[a-zA-Z0-9#-]+$").WithMessage("Le nom de la succursale doit être alphanumérique et ne pas contenir d'espaces.");

            RuleFor(x => x.NumeroCivique_Succcursale).GreaterThanOrEqualTo(0).WithMessage("Le numéro civique doit être un entier positif.");
            RuleFor(x => x.Rue_Succcursale).Length(5, 30).Matches("^[a-zA-Z]+$").WithMessage("Le nom de la rue doit contenir entre 5 et 30 caractères alphabétiques, sans espaces.");
            RuleFor(x => x.Ville_Succcursale).Length(5, 30).Matches("^[a-zA-Z]+$").WithMessage("Le nom de la ville doit contenir entre 5 et 30 caractères alphabétiques, sans espaces.");
            RuleFor(x => x.CodePostal_Succcursale).Matches("^[a-zA-Z]\\d[a-zA-Z]\\s?\\d[a-zA-Z]\\d$|^\\d[a-zA-Z]\\d[a-zA-Z]\\d$").WithMessage("Le code postal doit respecter le format « A0A 0A0 » ou « A0A0A0 ».");
        }
    }
}
