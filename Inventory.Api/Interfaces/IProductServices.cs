using Inventory.Api.Models;

namespace Inventory.Api.Interfaces
{
    public interface IProductServices
    {
        Task<List<GetAllProductsModel>> GetAllProductsList();
    }
}
