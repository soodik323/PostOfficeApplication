using Fare;
using Microsoft.AspNetCore.Components;
using PostOffice.Application.Dtos;
using PostOffice.WebApp.Contracts;
using PostOffice.WebApp.Models;
using PostOffice.WebApp.Services;
using PostOffice.WebApp.Services.Base;

namespace PostOffice.WebApp.Pages.Parcel
{
    public partial class Create
    {
        [Inject]
        protected IShipmentDataService ShipmentDataDataService{ get; set; }
        
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public ParcelModel Parcel { get; set; }
        [Parameter]
        public int Id { get; set; }
        [Parameter]
        public int BagId { get; set; }
        public string Message { get; set; }

        protected override void OnInitialized()
        {
            Parcel = new ParcelModel
            {
                BagId = BagId,
                ParcelNumber = SerialCodeService.GetParcialSerial()

            };
        }
        protected async void HandleValidSubmit()
        {
            var response = await ShipmentDataDataService.CreateParcel(Parcel);
            HandleResponse(response);
            if(Message == null)
                NavigateToList();
        }
        private void HandleResponse(ApiResponse<ParcelDto> response)
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
