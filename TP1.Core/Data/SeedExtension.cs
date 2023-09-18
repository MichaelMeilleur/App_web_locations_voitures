using TP1.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using TP1.Core.Domain.Entities.Identité;
using TP1.Core.Domain.Entities.Locations;
using Microsoft.AspNetCore.Identity;

namespace TP1.Core.Data
{
    public static class SeedExtension
    {
        public static readonly PasswordHasher<Utilisateur> PASSWORDHASHER = new();

        public static void Seed(this ModelBuilder builder)
        {
            var adminRole = AddRole(builder, "Administrateur");
             _ = AddRole(builder, "Gérant");
             _ = AddRole(builder, "Commis");
             _ = AddRole(builder, "Utilisateur");
            var adminUser = AddUser(builder, "admin", "Admin123!");
            addUserToRole(builder, adminUser, adminRole);
        }

        private static void addUserToRole(ModelBuilder builder, Utilisateur newUser, IdentityRole<Guid> adminRole)
        {
            builder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
            {
                UserId = newUser.Id,
                RoleId = adminRole.Id,
            });
        }

        private static IdentityRole<Guid> AddRole(ModelBuilder builder, string name)
        {
            var newRole = new IdentityRole<Guid>
            {
                Id = Guid.NewGuid(),
                Name = name,
                NormalizedName = name.ToUpper()
            };
            builder.Entity<IdentityRole<Guid>>().HasData(newRole);

            return newRole;
        }

        private static Utilisateur AddUser(ModelBuilder builder,
            string userName, string password)
        {
            var newUser = new Utilisateur(userName)
            {
                Id = Guid.NewGuid(),
                NormalizedUserName = userName.ToUpper(),
                SecurityStamp = Guid.NewGuid().ToString()
            };
            newUser.PasswordHash = PASSWORDHASHER.HashPassword(newUser, password);
            builder.Entity<Utilisateur>().HasData(newUser);

            return newUser;
        }
    }
}
