using System.ComponentModel.DataAnnotations;

namespace TP1.WebSite.Models.Identité.Utilisateur
{
    public class UtilisateurEditM
    {
        public Guid ID { get; set; }
        [Display(Name = "Nom d'utilisateur")]
        public string NomUtilisateur { get; set; }
        [Display(Name = "Mot de passe")]
        public string MotDePasse { get; set; }
    }
}
