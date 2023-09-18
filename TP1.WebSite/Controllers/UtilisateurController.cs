using Microsoft.AspNetCore.Mvc;
using TP1.Core;
using TP1.WebSite.Models.Locations.Succursale;
using TP1.WebSite.Models.Locations;
using TP1.WebSite.Models.Identité.Utilisateur;
using static TP1.Core.Domain.Entities.Identité.Utilisateur;
using Microsoft.Build.ObjectModelRemoting;
using TP1.Core.Domain.Entities.Locations;
using TP1.Core.Domain.Entities.Identité;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using NuGet.ContentModel;
using TP1.WebSite.Utilities;

namespace TP1.WebSite.Controllers
{
    [Authorize(Roles = "Administrateur")]
    public class UtilisateurController : Controller
    {
        private readonly UserManager<Utilisateur> userManager;
        private readonly RoleManager<IdentityRole<Guid>> roleManager;
        //private readonly SystemeReservationDbContext context;
        private readonly DomainAsserts asserts;


        public UtilisateurController(SystemeReservationDbContext context, UserManager<Utilisateur> userManager,
        RoleManager<IdentityRole<Guid>> roleManager, DomainAsserts asserts)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            //this.context = context;
            this.asserts = asserts;
        }

        public async Task<IActionResult> Manage()
        {
            var vm = new List<UtilisateurManageM>();

            foreach (var user in userManager.Users)
            {
                var userRoles = await userManager.GetRolesAsync(user);
                vm.Add(new UtilisateurManageM
                {
                    ID = user.Id,
                    NomUtilisateur = user.UserName
                });
            }

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(Guid id)
        {
            var user = await userManager.FindByIdAsync(id.ToString());

            asserts.Exists(user, "User not found.");

            var token = await userManager.GeneratePasswordResetTokenAsync(user);

            var newPassword = PasswordGenerator.Generate();

            var result = await userManager.ResetPasswordAsync(user, token, newPassword);

            if (!result.Succeeded)
            {
                throw new Exception("Unable to reset password.");
            }

            return View(new ResetPasswordM
            {
                UserName = user.UserName,
                NewPassword = newPassword,
            });
        }

        [HttpPost]
        public async Task<IActionResult> Remove(Guid id)
        {
            var user = await userManager.FindByIdAsync(id.ToString());
            asserts.Exists(user, "User not found.");

            var result = await userManager.DeleteAsync(user!);

            if (!result.Succeeded)
            {
                throw new Exception("Unable to remove the user.");
            }

            return RedirectToAction(nameof(Manage));
        }
    }
}
