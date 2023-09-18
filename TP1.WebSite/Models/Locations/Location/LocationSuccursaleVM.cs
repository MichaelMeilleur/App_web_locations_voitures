using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using TP1.WebSite.Models.Locations.Conducteurs;
using TP1.WebSite.Models.Locations.Succursale;
using TP1.WebSite.Models.Locations.Voiture;

namespace TP1.WebSite.Models.Locations.Location
{
    public class LocationSuccursaleVM
    {
        [Display(Name = "Succursale")]
        public Guid SuccursaleId { get; set; }

        public IEnumerable<SuccursaleDetailM> Succursales { get; set; }

        public IEnumerable<LocationDetailM> Locations { get; set; }
        public IEnumerable<ConducteurViewM> Conducteurs { get; set; }
        public IEnumerable<VoitureDetailM> Voitures { get; set; }
    }
}
