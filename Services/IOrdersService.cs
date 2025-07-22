using Radzen;
using VerstaTestTask.Models;

namespace VerstaTestTask.Services
{
    public interface IOrdersService
    {
        Task<ODataServiceResult<Order>> GetOrders(Query query);
        Task<ODataServiceResult<City>> GetCities(Query query);
        void CreateOrder(CreateOrderRequest request);
        Task<Order> GetOrderByIdAsync(long orderId);
    }
}
