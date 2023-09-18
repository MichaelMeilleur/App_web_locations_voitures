using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static TP1.Core.Domain.Entities.Locations.Conducteur;
using System.Linq;
using TP1.Core;
using static TP1.Core.Domain.Entities.Locations.Location;
using TP1.WebSite.Models.Locations.Voiture;
using TP1.WebSite.Models.Locations.Location;

namespace TP1.WebSite.Models.Locations.Conducteurs
{
    public class ConducteurViewM
    {
        [Display(Name = "Identifiant")]
        public Guid Id { get; set; }
        [Display(Name = "Prenom")]
        public string Prenom { get; set; }
        [Display(Name = "Nom")]
        public string Nom { get; set; }
        [Display(Name = "Courriel")]
        public string Courriel { get; set; }
        [Display(Name = "Adresse civique")]
        public string Adresse { get; set; }
        public string Rue { get; set; }
        public string CodeCiviv { get; set; }
        [Display(Name = "Telephone")]
        public string Telephone { get; set; }
        [Display(Name = "Num. Permis de conduire")]
        public string PermisDeConduire { get; set; }
        public int NbLocations { get; set; }
        public DateTime DerniereLocation { get; set; }
        public Statut statut { get; set; }
        public DateTime OuverturePlanifie { get; set; }
        public DateTime FermeturePlanifie { get; set; }
        public string NomSuccursale { get; set; }
        public string NomVoiture { get; set; }
    }
}
