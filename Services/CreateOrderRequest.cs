namespace VerstaTestTask.Services
{
    /// <summary>
    /// Запрос на создание заказа
    /// </summary>
    public class CreateOrderRequest
    {
        public DateOnly OrderDate {  get; set; }

        public long SenderCityId { get; set; }
        public string SenderAddress { get; set; }

        public long RecipientCityId { get; set; }
        public string RecipientAddress { get; set; }

        public int CargoWeight { get; set; }
    }
}
