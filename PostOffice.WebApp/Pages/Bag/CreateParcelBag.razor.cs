using Microsoft.AspNetCore.Components;
using PostOffice.Application.Dtos;
using PostOffice.Domain;
using PostOffice.WebApp.Contracts;
using PostOffice.WebApp.Models;
using PostOffice.WebApp.Services;
using PostOffice.WebApp.Services.Base;

namespace PostOffice.WebApp.Pages.Bag
{
    public partial class CreateParcelBag
    {
        [Inject]
        protected IShipmentDataService ShipmentDataDataService{ get; set; }
        
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public ParcelBagModel Bag { get; set; }
        public string Message { get; set; }
        [Parameter]
        public int Id { get; set; }

        protected override void OnInitialized()
        {
            Bag = new ParcelBagModel
            {
                ShipmentId = Id,
                BagType = BagType.Parcel,
                BagNumber = SerialCodeService.GetParcialBagSerial()

            };
        }
        protected async void HandleValidSubmit()
        {
            var response = await ShipmentDataDataService.CreateParcelBag(Bag);
            HandleResponse(response);
            if (Message == null)
                NavigateToList();
        }
        private void HandleResponse(ApiResponse<BagDto> response)
        {
            if (!response.Success)
            {
                Message = "Failed";
            }
           
            // Note that the following line is necessary because otherwise
            // Blazor would not recognize the state change and refresh the UI
            InvokeAsync(() =>
            {
                StateHasChanged();
            });
        }

        protected async void NavigateToList()
        {
            NavigationManager.NavigateTo($"/bag/details/{Id}");
        }

    }
}
