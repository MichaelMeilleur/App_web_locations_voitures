using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP1.Core.Domain.Entities.Locations
{
    public class Note
    {
        public Guid Id { get; set; } 
        public string? Texte { get; set; }
        public Guid? VoitureId { get; set; }
        public Guid? LocationId { get; set; }
    }
}
