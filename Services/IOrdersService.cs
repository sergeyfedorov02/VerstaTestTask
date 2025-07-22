using Radzen;
using VerstaTestTask.Extensions;
using VerstaTestTask.Models;

namespace VerstaTestTask.Services
{
    public interface IOrdersService
    {
        Task<ResultWrapper<ODataServiceResult<Order>>> GetOrders(Query query);
        Task<ResultWrapper<ODataServiceResult<City>>> GetCities(Query query);
        ResultWrapper<Order> CreateOrder(CreateOrderRequest request);
        Task<ResultWrapper<Order>> GetOrderByIdAsync(long orderId);
    }
}
