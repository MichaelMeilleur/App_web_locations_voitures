using System.ComponentModel.DataAnnotations;

namespace TP1.WebSite.Models.Identité.Utilisateur
{
    public class UtilisateurManageM
    {
        [Display(Name = "Id")]
        public Guid ID { get; set; }
        [Display(Name = "Nom d'utilisateur")]
        public string NomUtilisateur { get; set; }
        [Display(Name = "Role id")]
        public int role_id { get; set; }
    }
}
