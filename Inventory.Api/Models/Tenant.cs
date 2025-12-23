using Inventory.Api.Entity;

namespace Inventory.Api.Models
{
    public class TenantModel
    {
        public int TenantId { get; set; }
        public string TenantName { get; set; }
        public string SubDomain { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public ICollection<User> Users { get; set; }
        public ICollection<Category> Categories { get; set; }
        public ICollection<Products> Products { get; set; }
    }
}
