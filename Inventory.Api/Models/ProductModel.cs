using Inventory.Api.Entity;

namespace Inventory.Api.Models
{
    public class ProductModel
    {
        public int ProductId { get; set; }
        public int TenantId { get; set; }
        public int? CategoryId { get; set; }
        public string ProductName { get; set; }
        public string SKU { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public Tenant Tenant { get; set; }
        public Category Category { get; set; }
    }
}
