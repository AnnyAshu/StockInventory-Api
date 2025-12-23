using System.ComponentModel.DataAnnotations;
namespace Inventory.Api.Entity
{
    public class Products
    {
        [Key]
        public int ProductId { get; set; }
        public int TenantId { get; set; }
        public int? CategoryId { get; set; }
        public string ProductName { get; set; }
        public string SKU { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public DateTime? CreatedAt { get; set; }

    }
}
