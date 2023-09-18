using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TP1.Core.Domain.Entities.Locations;
using static TP1.Core.Domain.Entities.Locations.Voiture;

namespace TP1.WebSite.Models.Locations.Voiture
{
    // 1 à plusieurs voitures pour une succursale
    public class VoitureDetailM
    {
        [Display(Name = "Succursale")]
        public Guid SuccursaleId { get; set; }
        public Guid Id { get; set; }
        public string Surnom { get; set; }

        public StatutV Statutv { get; set; }
        public bool Disponible { get; set; }
        public Etat Etat { get; set; }
        public string? NumeroSerie { get; set; }
        public string? NumeroImmatriculation { get; set; }
        public string? Marque { get; set; }
        public string? Modele { get; set; }
        public int?Annee { get; set; }
        public string? Couleur { get; set; }
        public int? Kilometrage { get; set; }
        public decimal? ValeurEstimee { get; set; }
        public List<Note> liste { get; set; } = new List<Note>();

        public void RetrieveNotes(DbContext context)
        {
            liste = context.Set<Note>()
                .Where(n => n.VoitureId == Id)
                .ToList();
        }

    }
}
