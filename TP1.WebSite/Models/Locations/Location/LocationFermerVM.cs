using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace TP1.WebSite.Models.Locations.Location
{
    public class FermerLocationVM
    {
        [Required]
        [Display(Name = "Heure réelle de fermeture ")]
        public DateTime DateFermeture { get; set; }
        [Display(Name = "Note")]
        public string? NoteAjouter { get; set; }
    }
}
