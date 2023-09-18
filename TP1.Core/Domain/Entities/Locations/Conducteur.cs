using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TP1.Core.Domain.Entities.Locations
{
    public class Conducteur
    {
        public Guid Id { get; set; }
        public string Prenom { get; set; }
        public string Nom { get; set; }
        public string Courriel { get; set; }
        public string Telephone { get; set; }
        public string PermisDeConduire { get; set; }
        public bool InformationsValides { get; set; }

        //Voiture
        public Guid? VoitureId { get; set; }
        [ForeignKey(nameof(VoitureId))]
        public Voiture Voiture { get; set; }

        //Adresse
        public Adresse? Adresse { get; set; }

    }
}
