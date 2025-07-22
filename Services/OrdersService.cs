using Microsoft.EntityFrameworkCore;
using Radzen;
using VerstaTestTask.Data;
using VerstaTestTask.Models;
using VerstaTestTask.Extensions;
using VerstaTestTask.Services;

namespace VerstaTestTask
{
    /// <summary>
    /// Сервис для получения данных из БД (использует сервис)
    /// </summary>
    public class OrdersService : IOrdersService
    {
        private ILogger<OrdersService> Logger { get; }
        /// <summary>
        /// // Контекст - для связи объектной модели и БД (напрямую с БД не работаю)
        /// </summary>
        private Func<VerstaDbContext> ContextProvider { get; }

        public OrdersService(Func<VerstaDbContext> provider, ILogger<OrdersService> logger)
        {
            ContextProvider = provider;
            Logger = logger;
        }

        /// <summary>
        /// Поиск заказов
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<ResultWrapper<ODataServiceResult<Order>>> GetOrders(Query query)
        {
            try
            {
                await using var context = ContextProvider();

                var result = context.Orders.Include(o => o.SenderCity).Include(o => o.RecipientCity).AsQueryable();

                return ResultWrapper<ODataServiceResult<Order>>.CreateFromResult(await result.GetDataAsync(query, "Id asc"));
            }
            catch (Exception ex)
            {
                return ResultWrapper<ODataServiceResult<Order>>.CreateFromException(ex);
            }

            
        }

        /// <summary>
        /// Поиск городов
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<ResultWrapper<ODataServiceResult<City>>> GetCities(Query query)
        {
            await using var context = ContextProvider();

            try
            {
                var result = context.Cities.AsQueryable();

                return ResultWrapper<ODataServiceResult<City>>.CreateFromResult(await result.GetDataAsync(query, "Id asc"));
            }
            catch (Exception ex)
            {
                return ResultWrapper<ODataServiceResult<City>>.CreateFromException(ex);
            }

        }

        /// <summary>
        /// Создание заказа
        /// </summary>
        /// <param name="request"></param>
        public ResultWrapper<Order> CreateOrder(CreateOrderRequest request)
        {
            using var context = ContextProvider();

            try
            {
                var newOrder = new Order
                {
                    OrderDate = request.OrderDate,
                    SenderCityId = request.SenderCityId,
                    RecipientCityId = request.RecipientCityId,
                    SenderAddress = request.SenderAddress,
                    RecipientAddress = request.RecipientAddress,
                    CargoWeight = request.CargoWeight
                };

                context.Orders.Add(newOrder);

                context.SaveChanges();
                return ResultWrapper<Order>.CreateFromResult(newOrder);
            }
            catch (Exception ex)
            {
                return ResultWrapper<Order>.CreateFromException(ex);
            }
        }

        /// <summary>
        /// Поиск заказа по идентификатору
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public async Task<ResultWrapper<Order>> GetOrderByIdAsync(long orderId)
        {
            await using var context = ContextProvider();

            try
            {
                var result = await context.Orders.Include(o => o.SenderCity).Include(o => o.RecipientCity).FirstOrDefaultAsync(o => o.Id == orderId);

                return ResultWrapper<Order>.CreateFromResult(result);
            }
            catch (Exception ex)
            {
                return ResultWrapper<Order>.CreateFromException(ex);
            }
        }
    }
}