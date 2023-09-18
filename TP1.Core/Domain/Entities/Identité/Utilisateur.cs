using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TP1.Core.Domain.Entities.Identité
{
    public class Utilisateur : IdentityUser<Guid>
    {
        public Utilisateur(string userName) : base(userName){ }
    }
}
