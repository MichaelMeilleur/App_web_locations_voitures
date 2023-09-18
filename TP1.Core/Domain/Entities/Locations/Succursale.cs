using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static TP1.Core.Domain.Entities.Locations.Voiture;

namespace TP1.Core.Domain.Entities.Locations
{
    public class Succursale
    {
        public Guid Id { get; set; }
        public string Nom { get; set; }
        public bool Statut { get; set; }
        public Adresse adresse { get; set; }
    }
}
