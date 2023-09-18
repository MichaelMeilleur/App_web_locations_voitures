using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP1.Core.Domain.Entities.Locations
{
    // 1 à plusieurs voitures pour une succursale
    public class Voiture
    {
        public Guid Id { get; set; }
        public Guid SuccursaleId { get; set; }
        public Guid? LocationId { get; set; }
        public string Surnom { get; set; }
        public bool Disponible { get; set; }
        public StatutV statut { get; set; }
        public enum StatutV
        {
            Activé,
            Désactivé,
            Archivé
        }

        public Etat etat { get; set; }
        public enum Etat
        {
            Neuf,
            Usagé
        }
        public string NumeroSerie { get; set; }
        public string NumeroImmatriculation { get; set; }
        public string Marque { get; set; }
        public string Modele { get; set; }
        public int Annee { get; set; }
        public string Couleur { get; set; }
        public int? Kilometrage { get; set; }
        public decimal? ValeurEstimee { get; set; }

        [ForeignKey(nameof(SuccursaleId))]
        public virtual Succursale? Succursale { get; set; }
        [ForeignKey(nameof(LocationId))]
        public virtual Location? Location { get; set; }
        public List<Note>? Note { get; set; } = new();
    }
}
