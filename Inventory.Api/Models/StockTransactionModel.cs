using Inventory.Api.Entity;

namespace Inventory.Api.Models
{
    public class StockTransactionModel
    {
        public int Id { get; set; }
        public int TenantId { get; set; }
        public int ProductId { get; set; }
        public string Type { get; set; } // IN / OUT
        public int Quantity { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;

        public Tenant Tenant { get; set; }
        public Products Product { get; set; }
    }
}
