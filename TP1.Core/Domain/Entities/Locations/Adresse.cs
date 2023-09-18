using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP1.Core.Domain.Entities.Identité;

namespace TP1.Core.Domain.Entities.Locations
{
    public class Adresse
    {
        public Guid ID { get; set; }
        public Guid? SuccursaleID { get; set; }
        public Guid? ConducteurID { get; set; }
        public int NumeroCivique { get; set; }
        public string Rue { get; set; }
        public string Ville { get; set; }
        public string CodePostal { get; set; }

        // Navigation Properties
        [ForeignKey(nameof(SuccursaleID))]
        public virtual Succursale? Succursale { get; set; }
        // Navigation Properties
        [ForeignKey(nameof(ConducteurID))]
        public virtual Conducteur? Conducteur { get; set; }

    }
}
