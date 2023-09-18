using System.ComponentModel.DataAnnotations;
using TP1.Core.Domain.Entities.Locations;
using TP1.WebSite.Models.Locations.Conducteurs;
using TP1.WebSite.Models.Locations.Succursale;
using TP1.WebSite.Models.Locations.Voiture;

namespace TP1.WebSite.Models.Locations.Location
{
    public class LocationDetailM
    {
        [Display(Name = "ID Location")]
        public Guid ID { get; set; }

        public Etat Statut { get; set; }

        [Display(Name = "Date et heure d'ouverture planifié")]
        public DateTime OuverturePlanifie { get; set; }

        [Display(Name = "Date et heure de fermeture  planifié")]
        public DateTime FermeturePlanifie { get; set; }

        [Display(Name = "Date et heure d'ouverture réel")]
        public DateTime? OuvertureReel { get; set; }

        [Display(Name = "Date et heure de fermeture réel")]
        public DateTime? FermetureReel { get; set; }

        public Guid SuccursaleId { get; set; }

        public enum Etat { Ouvert, Ferme }
        [Display(Name = "Prenom")]

        public string Prenom { get; set; }
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

        [Display(Name = "Voiture")]
        public Guid IdVoiture { get; set; }

        [Display(Name = "Voiture")]
        public string NomVoiture { get; set; }
        public List<Note> liste { get; set; } = new List<Note>();
    }
}
