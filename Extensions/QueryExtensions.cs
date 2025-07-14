﻿using Microsoft.EntityFrameworkCore;
using Radzen;
using System.Linq.Dynamic.Core;

namespace VerstaTestTask.Extensions
{
    public static class QueryExtensions
    {

        public static async Task<ODataServiceResult<T>> GetDataAsync<T>(this IQueryable<T> items, Query query, string defaultOrder = null)
        {
            if (!string.IsNullOrEmpty(query.Filter))
            {
                items = items.Where(query.Filter);
            }

            if (!string.IsNullOrEmpty(query.OrderBy))
            {
                items = items.OrderBy(query.OrderBy);
            }
            else if (!string.IsNullOrEmpty(defaultOrder))
            {
                items = items.OrderBy(defaultOrder);
            }

            var count = await items.CountAsync();

            if (query.Skip != null)
            {
                items = items.Skip(query.Skip.Value);
            }

            if (query.Top != null)
            {
                items = items.Take(query.Top.Value);
            }

            return new ODataServiceResult<T>
            {
                Count = count,
                Value = await items.ToListAsync()
            };
        }
    }
}
