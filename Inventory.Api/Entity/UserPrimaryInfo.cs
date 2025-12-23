using System.ComponentModel.DataAnnotations;

namespace Inventory.Api.Entity
{
    public class UserPrimaryInfo
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
    }
}
