using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace TP1.WebSite.Models.Locations.Voiture
{
    public class VoitureCreateNoteVM
    {
        [Display(Name = "Note")]
        public string NoteAjouter { get; set; }
    }
}
