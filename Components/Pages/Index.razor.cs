using System.Net.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.JSInterop;
using Radzen;
using Radzen.Blazor;
using System.Net.Http;
using VerstaTestTask.Models;

namespace VerstaTestTask.Components.Pages
{
    public partial class Index
    {
        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected TooltipService TooltipService { get; set; }

        [Inject]
        protected ContextMenuService ContextMenuService { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }

        [Inject]
        protected OrdersService OrdersService { get; set; }

        [Inject]
        protected ILogger<Index> Logger { get; set; }

        protected RadzenDataGrid<Order> grid;
        protected bool isLoading;

        protected IEnumerable<Order> orders;
        protected int count;

        protected int pageSize = 20;
        protected readonly IEnumerable<int> pageSizeOptions = [10, 20, 50];

        protected async Task LoadData(LoadDataArgs args)
        {
            isLoading = true;

            try
            {
                var result = await OrdersService.GetOrders(new Query
                {
                    Skip = args.Skip,
                    Top = args.Top,
                    Filter = args.Filter,
                    OrderBy = args.OrderBy
                });

                count = result.Count;
                orders = [.. result.Value];
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"Ошибка при получении таблицы заказов (filter={args.Filter})");
                count = 0;
                orders = [];
            }


            isLoading = false;
            StateHasChanged();
        }

        protected async Task ResetFiltersAsync()
        {
            foreach (var c in grid.ColumnsCollection)
            {
                c.ClearFilters();
            }

            await grid.Reload();
        }

        protected void ShowTooltip(ElementReference elementReference, string text, TooltipOptions options = null)
        {
            TooltipService.Open(elementReference, text, options);
        }

        protected void CreateOrder()
        {
            //TODO
        }
    }
}