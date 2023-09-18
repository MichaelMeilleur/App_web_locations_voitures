using System.ComponentModel.DataAnnotations;

namespace TP1.WebSite.Models.Locations.Adresse
{
    public class AdresseDetailM
    {
        public int Id { get; set; }
        [Display(Name = "Numero Civique")]
        public int NumeroCivique { get; set; }
        public string Rue { get; set; }
        public string Ville { get; set; }
        [Display(Name = "Code Postal")]
        public string CodePostal { get; set; }
    }
}
