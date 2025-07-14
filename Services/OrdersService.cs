using System;
using System.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Radzen;

using VerstaTestTask.Data;
using VerstaTestTask.Models;
using VerstaTestTask.Extensions;

namespace VerstaTestTask
{
    public class OrdersService
    {
        private ILogger<OrdersService> Logger { get; }
        private Func<VerstaDbContext> ContextProvider { get; }

        public OrdersService(Func<VerstaDbContext> provider, ILogger<OrdersService> logger)
        {
            ContextProvider = provider;
            Logger = logger;
        }

        public async Task<ODataServiceResult<Order>> GetOrders(Query query)
        {
            await using var context = ContextProvider();

            var result = context.Orders.Include(o => o.SenderCity).Include(o => o.RecipientCity).AsQueryable();

            return await result.GetDataAsync(query, "Id asc");
        }
    }
}