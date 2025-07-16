using Microsoft.AspNetCore.Components;
using Radzen;
using VerstaTestTask.Models;

namespace VerstaTestTask.Components
{
    public partial class ViewOrder
    {
        [Inject]
        protected DialogService DialogService { get; set; }

        [Parameter]
        public Order OrderData { get; set; }

        private void OnSubmit()
        {
            DialogService.Close();
        }
    }
}
