using Inventory.Api.Entity;

namespace Inventory.Api.Models
{
    public class UserModel
    {
       
            public int UserId { get; set; }
            public int TenantId { get; set; }
            public string Email { get; set; }
            public string PasswordHash { get; set; }
            public string Role { get; set; } // Admin / Manager / Staff
            public bool IsActive { get; set; } = true;

            public Tenant Tenant { get; set; }
        
    }
}
