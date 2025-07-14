using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Radzen;
using Radzen.Blazor;
using VerstaTestTask.Models;
using VerstaTestTask.Services;

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

        protected async Task CreateOrder()
        {
            if (await DialogService.OpenAsync<CreateOrder>(
                "Добавление заказа",
                new Dictionary<string, object> { },
                new DialogOptions
                {
                    Resizable = true,
                    Draggable = true
                }
            ) is CreateOrderRequest createRequest)
            {
                OrdersService.CreateOrder(createRequest);

                await grid.Reload();
            }
        }
    }
}