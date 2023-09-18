using FluentValidation;
using FluentValidation.AspNetCore;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace TP1.WebSite.Models.Identité.Account
{
    public class RegisterVM
    {
        [Display(Name = "Nom d'utilisateur")]
        public string UserName { get; set; }

        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Password confirmation")]
        [Compare(nameof(Password))]
        public string PasswordConfirmation { get; set; }

        [Display(Name = "Role")]
        public Guid RoleId { get; set; }

    }

    public class RegisterVMValidator : AbstractValidator<RegisterVM>
    {
        public RegisterVMValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Le nom d'utilisateur est obligatoire.")
                .Matches("^[^\\s]{5,20}$").WithMessage("Le nom d'utilisateur doit contenir entre 5 et 20 caractères sans espaces.");
        }
    }
}
