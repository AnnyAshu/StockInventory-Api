using System.ComponentModel.DataAnnotations;

namespace Inventory.Api.Entity
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public int TenantId { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; }
        public bool IsActive { get; set; }
        
    }
}
