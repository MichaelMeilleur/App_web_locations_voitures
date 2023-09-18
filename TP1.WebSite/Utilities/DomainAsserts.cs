using TP1.Core.Domain.Entities;

using Microsoft.AspNetCore.Identity;

using System.Security.Claims;
using TP1.Core.Domain.Entities.Identité;

namespace TP1.WebSite.Utilities;

public class DomainAsserts {
    private readonly UserManager<Utilisateur> userManager;

    public DomainAsserts(UserManager<Utilisateur> userManager) {
        this.userManager = userManager;
    }

    public void Exists(object entity, string errorMessage = "The resource cannot be found.") {
        if (entity is null) {
            throw new ArgumentNullException(errorMessage);
        }
    }

    public void IsOwnedByCurrentUser(object entity, ClaimsPrincipal user,
                string errorMessage = "You must own the resource.") {
        var userId = userManager.GetUserId(user);

        var ownerIdProp = entity.GetType().GetProperty("OwnerId");

        if (ownerIdProp is null) {
            throw new UnauthorizedAccessException(errorMessage);
        }

        var ownerIdValue = ownerIdProp.GetValue(entity);

        if (Guid.Equals(ownerIdValue, userId)) {
            throw new UnauthorizedAccessException(errorMessage);
        }
    }
}
