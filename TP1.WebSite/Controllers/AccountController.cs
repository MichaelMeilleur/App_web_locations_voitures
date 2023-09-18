using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using TP1.Core;
using TP1.Core.Domain.Entities.Identité;
using TP1.WebSite.Models.Identité.Account;
using TP1.WebSite.Models.Identité.Utilisateur;

namespace TP1.WebSite.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly SignInManager<Utilisateur> signInManager;
        private readonly UserManager<Utilisateur> userManager;
        private readonly RoleManager<IdentityRole<Guid>> roleManager;
        private readonly SystemeReservationDbContext context;

        public AccountController(SignInManager<Utilisateur> signInManager, 
            UserManager<Utilisateur> userManager, RoleManager<IdentityRole<Guid>> 
            roleManager, SystemeReservationDbContext context)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.context = context;
        }

        [AllowAnonymous]
        public IActionResult LogIn(string? returnUrl = "")
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> LogIn(LogInVM vm, string? returnUrl = "")
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ReturnUrl = returnUrl;
                return View(vm);
            }

            try
            {
                var result = await signInManager.PasswordSignInAsync(
                    vm.NomUtilisateur, vm.MotDePasse, false, false);

                if (!result.Succeeded)
                {
                    ModelState.AddModelError(string.Empty, "Mauvais nom d'utilisateur ou mot de passe!");
                    ViewBag.ReturnUrl = returnUrl;
                    return View(vm);
                }

            }
            catch
            {
                ModelState.AddModelError(string.Empty, "SVP veuillez essayer de nouveau!");
                ViewBag.ReturnUrl = returnUrl;
                return View(vm);
            }

            var idUser = context.Users.Where(u => u.UserName == vm.NomUtilisateur).Select(u => u.Id).FirstOrDefault();
            var roleId = context.UserRoles.Where(r => r.UserId == idUser).Select(r => r.RoleId).FirstOrDefault();
            var nomRole = context.Roles.Where(n => n.Id == roleId).Select(n => n.Name).FirstOrDefault();


            if (nomRole == "Administrateur")
                return RedirectToAction("Manage", "Utilisateur");
            else if (nomRole == "Gérant")
                return RedirectToAction("Manage", "Location");
            if (nomRole == "Commis")
                return RedirectToAction("Manage", "Voiture");
            else
                return RedirectToAction("Index", "Home");

            //return Redirect(returnUrl ?? Url.Action("Manage", "Utilisateur"));
        }

        [Authorize(Roles = "Administrateur")]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        [Authorize(Roles = "Administrateur")]
        public async Task<IActionResult> Register(RegisterVM vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var role = await roleManager.FindByIdAsync(vm.RoleId.ToString());

            var newUser = new Utilisateur(vm.UserName);
            var result = await userManager.CreateAsync(newUser, vm.Password);

            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Erreur");
                return View(vm);
            }

            //CORRIGER
            result = await userManager.AddToRoleAsync(newUser, role!.Name!);

            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, $"Erreur en ajoutant un role {role.Name}.");
                return View(vm);
            }

            return RedirectToAction("Manage", "Utilisateur");
        }

        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
