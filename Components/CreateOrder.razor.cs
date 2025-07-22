using Microsoft.AspNetCore.Components;
using Radzen;
using VerstaTestTask.Models;
using VerstaTestTask.Services;

namespace VerstaTestTask.Components
{
    public partial class CreateOrder
    {
        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected IOrdersService OrdersService { get; set; }

        [Inject]
        protected ILogger<CreateOrder> Logger { get; set; }

        private IEnumerable<City> cities;
        private int citiesCount;

        /// <summary>
        /// Подгрузка городов из БД
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        async Task LoadCities(LoadDataArgs args)
        {   
            var filterValue = args.Filter;

            if (!string.IsNullOrEmpty(filterValue))
            {
                filterValue = $"x => x.CityName.ToLower().Contains(\"{filterValue}\".ToLower())";
            }

            var result = await OrdersService.GetCities(new Query
            {
                Skip = args.Skip,
                Top = args.Top,
                Filter = filterValue,
                OrderBy = args.OrderBy
            });

            if (result.IsOk)
            {
                citiesCount = result.Data.Count;
                cities = [.. result.Data.Value];
            }
            else
            {
                Logger.LogError(result.Exception, $"Ошибка при получении списка городов (filter={args.Filter})");
                citiesCount = 0;
                cities = [];
            }

            StateHasChanged();
        }

        /// <summary>
        /// Создание запроса к сервису/OrderService на создание нового заказа
        /// </summary>
        /// <returns></returns>
        private CreateOrderRequest GetCreateRequest()
        {
            return new CreateOrderRequest
            {
                OrderDate = model.OrderDate.GetValueOrDefault(),
                SenderCityId = model.SenderCityId ?? 0,
                SenderAddress = model.SenderAddress,
                RecipientCityId = model.RecipientCityId ?? 0,
                RecipientAddress = model.RecipientAddress,
                CargoWeight = model.CargoWeight ?? 0
            };
        }

        private EditOrderModel model = new();

        /// <summary>
        /// Модель для биндинга в форме (в запросе все поля уже заняты)
        /// </summary>
        private class EditOrderModel
        {
            public DateOnly? OrderDate {  get; set; }
            public long? SenderCityId { get; set; }
            public string SenderAddress { get; set; }
            public long? RecipientCityId { get; set; }
            public string RecipientAddress { get; set; }
            public int? CargoWeight { get; set; }
        }
        
        [Inject]
        protected NotificationService NotificationService { get; set; }

        private void OnSubmit()
        {
            DialogService.Close(GetCreateRequest());
        }

        private void OnInvalidSubmit()
        {
            NotificationService.Notify(NotificationSeverity.Error, "Ошибка", "Ошибки при заполнении формы");
        }
    }
}
