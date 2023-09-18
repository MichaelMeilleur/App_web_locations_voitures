using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TP1.WebSite.Models.Locations;
using TP1.WebSite.Models.Locations.Succursale;

namespace TP1.WebSite.Models.Locations.Voiture
{
    public class VoitureSuccursaleVM
    {
        [Display(Name = "Succursale")]
        public Guid SuccursaleId { get; set; }

        public IEnumerable<SuccursaleDetailM> Succursales { get; set; }

        public IEnumerable<VoitureDetailM> Voitures { get; set; }
    }
}
