using static TP1.Core.Domain.Entities.Locations.Voiture;

namespace TP1.WebSite.Models.Locations.Voiture
{
    public class VoitureEdit
    {
        public string Surnom { get; set; }
        public int SuccursaleID { get; set; }
        public StatutV Statutv { get; set; }
        public bool Disponible { get; set; }
        public Etat Etat { get; set; }
        public string NumeroSerie { get; set; }
        public string NumeroImmatriculation { get; set; }
        public string Marque { get; set; }
        public string Modele { get; set; }
        public int Annee { get; set; }
        public string Couleur { get; set; }
        public int Kilometrage { get; set; }
        public decimal ValeurEstimee { get; set; }
    }
}
