using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TP1.Core.Domain.Entities.Locations.Succursale;

namespace TP1.WebSite.Models.Locations.Succursale
{
    public class SuccursaleDetailM
    {
        public Guid Id { get; set; }
        public string Nom { get; set; }
        [Display(Name = "Activé ou Désactivé")]
        public bool Statut { get; set; }

        [Display(Name = "Numero Civique")]
        public int NumeroCivique_Succcursale { get; set; }
        [Display(Name = "Rue")]
        public string Rue_Succcursale { get; set; }
        [Display(Name = "Ville")]
        public string Ville_Succcursale { get; set; }
        [Display(Name = "Code Postal")]
        public string CodePostal_Succcursale { get; set; }
    }
}
