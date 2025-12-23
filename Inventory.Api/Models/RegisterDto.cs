namespace Inventory.Api.Models
{
    public class RegisterDto
    {
        public string? TenantName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
    public class GetAllProductsModel
    {
        public int ProductId { get; set; }
        public int TenantId { get; set; }
        public string Name { get; set; }
        public string SKU { get; set; }
        public int? CategoryId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

    }
}
