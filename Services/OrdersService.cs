using Microsoft.EntityFrameworkCore;
using Radzen;
using VerstaTestTask.Data;
using VerstaTestTask.Models;
using VerstaTestTask.Extensions;
using VerstaTestTask.Services;

namespace VerstaTestTask
{
    /// <summary>
    /// ������ ��� ��������� ������ �� �� (���������� ������)
    /// </summary>
    public class OrdersService : IOrdersService
    {
        private ILogger<OrdersService> Logger { get; }
        /// <summary>
        /// // �������� - ��� ����� ��������� ������ � �� (�������� � �� �� �������)
        /// </summary>
        private Func<VerstaDbContext> ContextProvider { get; }

        public OrdersService(Func<VerstaDbContext> provider, ILogger<OrdersService> logger)
        {
            ContextProvider = provider;
            Logger = logger;
        }

        /// <summary>
        /// ����� �������
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<ODataServiceResult<Order>> GetOrders(Query query)
        {
            await using var context = ContextProvider();

            var result = context.Orders.Include(o => o.SenderCity).Include(o => o.RecipientCity).AsQueryable();

            return await result.GetDataAsync(query, "Id asc");
        }

        /// <summary>
        /// ����� �������
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<ODataServiceResult<City>> GetCities(Query query)
        {
            await using var context = ContextProvider();

            var result = context.Cities.AsQueryable();

            return await result.GetDataAsync(query, "Id asc");
        }

        /// <summary>
        /// �������� ������
        /// </summary>
        /// <param name="request"></param>
        public void CreateOrder(CreateOrderRequest request)
        {
            using var context = ContextProvider();

            context.Orders.Add(new Order
            {
                OrderDate = request.OrderDate,
                SenderCityId = request.SenderCityId,
                RecipientCityId = request.RecipientCityId,
                SenderAddress = request.SenderAddress,
                RecipientAddress = request.RecipientAddress,
                CargoWeight = request.CargoWeight
            });

            context.SaveChanges();
        }

        /// <summary>
        /// ����� ������ �� ��������������
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public async Task<Order> GetOrderByIdAsync(long orderId)
        {
            await using var context = ContextProvider();

            return await context.Orders.Include(o => o.SenderCity).Include(o => o.RecipientCity).FirstOrDefaultAsync(o => o.Id == orderId);
        }
    }
}