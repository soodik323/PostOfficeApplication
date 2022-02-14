using System;
using Microsoft.AspNetCore.Components;
using PostOffice.Application.Dtos;
using PostOffice.WebApp.Contracts;
using PostOffice.WebApp.Models;
using PostOffice.WebApp.Services;
using PostOffice.WebApp.Services.Base;

namespace PostOffice.WebApp.Pages.Shipment
{
    public partial class Create
    {
        [Inject]
        protected IShipmentDataService ShipmentDataDataService{ get; set; }
        
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public ShipmentModel ShipmentModel { get; set; }
        public string Message { get; set; }

        protected override void OnInitialized()
        {
            ShipmentModel = new ShipmentModel()
            {
                ShipmentNumber = SerialCodeService.GetShipmentSerial(),
                FlightNumber = SerialCodeService.GetFlightNumberSerial(),
                FlightDate = DateTime.Now
            };
        }
        protected async void HandleValidSubmit()
        {
            var response = await ShipmentDataDataService.CreateShipment(ShipmentModel);
            HandleResponse(response);
            if (Message == null)
                NavigationManager.NavigateTo("/");
        }
        private void HandleResponse(ApiResponse<ShipmentDto> response)
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
      
    }
}
