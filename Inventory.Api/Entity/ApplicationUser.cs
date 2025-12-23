using Microsoft.AspNetCore.Identity;

namespace Inventory.Api.Entity;

public class ApplicationUser : IdentityUser
{
    public int TenantId { get; set; }
}
