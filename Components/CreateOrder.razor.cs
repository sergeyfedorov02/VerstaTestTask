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
        protected OrdersService OrdersService { get; set; }

        [Inject]
        protected ILogger<CreateOrder> Logger { get; set; }

        private DateOnly? date;

        private IEnumerable<City> cities;
        private long? senderCityId;
        private long? recipientCityId;
        private int citiesCount;

        async Task LoadCities(LoadDataArgs args)
        {
            try
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

                citiesCount = result.Count;
                cities = [.. result.Value];
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"Ошибка при получении списка городов (filter={args.Filter})");
                citiesCount = 0;
                cities = [];
            }

            StateHasChanged();
        }

        private string senderAddress;
        private string recipientAddress;

        private int? cargoWeight;

        private bool CanAdd()
        {
            return senderCityId != null && recipientCityId != null && cargoWeight != null && date != null
                && !string.IsNullOrWhiteSpace(senderAddress) && !string.IsNullOrWhiteSpace(recipientAddress);
        }

        private CreateOrderRequest GetCreateRequest()
        {
            return new CreateOrderRequest
            {
                OrderDate = date.GetValueOrDefault(),
                SenderCityId = senderCityId ?? 0,
                RecipientCityId =recipientCityId ?? 0,
                SenderAddress = senderAddress,
                RecipientAddress = recipientAddress,
                CargoWeight = cargoWeight ?? 0
            };
        }
    }
}
