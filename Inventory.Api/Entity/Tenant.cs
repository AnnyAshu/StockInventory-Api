using System.ComponentModel.DataAnnotations;

namespace Inventory.Api.Entity
{
    public class Tenant
    {
        [Key]
        public int? TenantId { get; set; }
        public string? TenantName { get; set; }
    }
}
