using FluentValidation;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace TP1.WebSite.Models.Identité.Account
{
    public class LogInVM
    {
        [Display(Name = "Nom d'utilisateur")]
        public string NomUtilisateur { get; set; }
        [Display(Name = "Mot de Passe")]
        [DataType(DataType.Password)]
        public string MotDePasse { get; set; }
    }
}
