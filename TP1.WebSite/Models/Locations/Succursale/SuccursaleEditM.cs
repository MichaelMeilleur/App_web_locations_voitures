using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TP1.WebSite.Models.Locations.Succursale
{
    public class VoitureEditM
    {
        public string Nom { get; set; }
        //public _Statut Statut { get; set; }

        [Display(Name = "Activé ou Désactivé")]
        public string Statut { get; set; }
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
