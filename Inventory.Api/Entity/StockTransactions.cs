using System.ComponentModel.DataAnnotations;

namespace Inventory.Api.Entity
{
    public class StockTransactions
    {
        [Key]
        public int Id { get; set; }
        public int TenantId { get; set; }
        public int ProductId { get; set; }
        public string Type { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; }
    }
}
