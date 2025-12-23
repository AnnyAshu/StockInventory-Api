using System.ComponentModel.DataAnnotations;

namespace Inventory.Api.Entity
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        public int TenantId { get; set; }
        public string CategoryName { get; set; }


    }
}
