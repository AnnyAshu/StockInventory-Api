using Inventory.Api.Entity;

namespace Inventory.Api.Models
{
    public class CategoryModel
    {
        public int CategoryId { get; set; }
        public int TenantId { get; set; }
        public string CategoryName { get; set; }

        public Tenant Tenant { get; set; }
        public ICollection<Products> Products { get; set; }
    }
}
