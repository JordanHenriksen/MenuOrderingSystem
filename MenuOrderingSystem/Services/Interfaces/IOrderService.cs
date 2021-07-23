using MenuOrderingSystem.Models;
using System.Threading.Tasks;

namespace MenuOrderingSystem.Services.Interfaces
{
    public interface IOrderService
    {
        Task<OrderResponse> CreateOrder(OrderRequest request);
    }
}
