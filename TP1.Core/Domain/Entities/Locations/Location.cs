//using Microsoft.EntityFrameworkCore.Query.Internal;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP1.Core.Domain.Entities.Locations
{
    public class Location
    {
        public Guid Id { get; set; }
        public Statut statut { get; set; }
        public enum Statut { ouvert, fermé }
        public DateTime OuverturePlanifie { get; set; }
        public DateTime FermeturePlanifie { get; set; }
        public DateTime? OuvertureReel { get; set; }
        public DateTime? FermetureReel { get; set; }
        public List<Note>? Note { get; set; }

        public Guid? SuccursaleId { get; set; }
        [ForeignKey(nameof(SuccursaleId))]
        public virtual Succursale? Succursale { get; set; }
        public Conducteur Conducteur { get; set; }
        public Voiture voiture { get; set; }

    }
}
