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
        // inject - ��� ��������� �����������(dependency injection) ��� ����������� asp.net core
        // ����� ������ �� ��������� ��������, � ����� asp.net core ��� �� ����������
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
        protected IOrdersService OrdersService { get; set; }

        [Inject]
        protected ILogger<Index> Logger { get; set; }

        protected RadzenDataGrid<Order> grid;
        protected bool isLoading;

        protected IEnumerable<Order> orders;
        protected int count;

        protected int pageSize = 20;
        protected readonly IEnumerable<int> pageSizeOptions = [10, 20, 50];

        /// <summary>
        /// ��������� ��������� � �����/�������
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        protected async Task LoadData(LoadDataArgs args)
        {
            isLoading = true;

            var result = await OrdersService.GetOrders(new Query
            {
                Skip = args.Skip,
                Top = args.Top,
                Filter = args.Filter,
                OrderBy = args.OrderBy
            });

            if (result.IsOk)
            {
                count = result.Data.Count;
                orders = [.. result.Data.Value];
            }
            else
            {
                Logger.LogError(result.Exception, $"������ ��� ��������� ������� ������� (filter={args.Filter})");
                count = 0;
                orders = [];
            }         

            isLoading = false;
            StateHasChanged();
        }

        /// <summary>
        /// ���������� ������ ������ ���� ��������
        /// </summary>
        /// <returns></returns>
        protected async Task ResetFiltersAsync()
        {
            foreach (var c in grid.ColumnsCollection)
            {
                c.ClearFilters();
            }

            await grid.Reload();
        }

        /// <summary>
        /// ��������� ��������� ��� ���������� ��������
        /// </summary>
        /// <param name="elementReference"></param>
        /// <param name="text"></param>
        /// <param name="options"></param>
        protected void ShowTooltip(ElementReference elementReference, string text, TooltipOptions options = null)
        {
            TooltipService.Open(elementReference, text, options);
        }

        /// <summary>
        /// ����� ����������� ���� ��� �������� �������
        /// </summary>
        /// <returns></returns>
        protected async Task CreateOrder()
        {
            if (await DialogService.OpenAsync<CreateOrder>(
                "���������� ������",
                new Dictionary<string, object> {  },
                new DialogOptions
                {
                    Resizable = true,
                    Draggable = true,
                    Width = "700px",
                    Height = "700px",
                    Style = "min-width:700px; min-height:700px;",
                    CloseDialogOnOverlayClick = false
                }
            ) is CreateOrderRequest createRequest)
            {
                var result = OrdersService.CreateOrder(createRequest);

                if (result.IsOk)
                {
                    NotificationService.Notify(NotificationSeverity.Success, "�������", "����� ��������");
                }
                else
                {
                    Logger.LogError("������ ��� ���������� ������", result.Exception);
                    NotificationService.Notify(NotificationSeverity.Error, "����������", "����� �� ��������");
                }

                await grid.Reload();
            }
        }

        /// <summary>
        /// ����� ����������� ���� �� �������� ������� ��� ��������� ������
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private async Task OnRowDoubleClick(DataGridRowMouseEventArgs<Order> args)
        {
            await DialogService.OpenAsync<ViewOrder>(
                $"�������� ������ � {args.Data.Id}",
                new Dictionary<string, object> { { "OrderData", args.Data } },
                new DialogOptions
                {
                    Resizable = false,
                    Draggable = true,
                    Width = "700px",
                    Height = "700px",
                    Style = "min-width:700px; min-height:700px;",
                    CloseDialogOnOverlayClick = false
                }
            );
        }
    }
}