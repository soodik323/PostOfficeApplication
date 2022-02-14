using System;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using PostOffice.Application.Dtos;
using PostOffice.Domain;
using PostOffice.WebApp.Contracts;
using PostOffice.WebApp.Models;
using PostOffice.WebApp.Services.Base;

namespace PostOffice.WebApp.Pages.Bag
{
    public partial class Details
    {
        [Inject]
        protected IShipmentDataService ShipmentDataDataService{ get; set; }
        [Parameter]
        public int Id { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public string Message { get; set; }

        public ShipmentModel Shipment { get; set; }

        protected async override Task OnInitializedAsync()
        {

            var result = (await ShipmentDataDataService.GetShipmentById(Id)).Result;

            var k = new ShipmentModel()
            {
                Id = result.Id,
                ShipmentNumber = result.ShipmentNumber,
                Airport = result.Airport,
                FlightNumber = result.FlightNumber,
                FlightDate = result.FlightDate,
                IsFinalized = result.IsFinalized,
                Bags = result.Bags.Where(x => x.BagType == BagType.Letter).Select(x => new LetterBagModel()
                    {
                        BagNumber = x.BagNumber,
                        BagType = x.BagType,
                        Price = x.Price,
                        Weight = x.Weight,
                        ItemCount = x.ItemCount,
                        Id = x.Id,
                        ShipmentId = x.ShipmentId

                    }).ToList()
                    .Union(result.Bags.Where(x => x.BagType == BagType.Parcel).Select(x => new ParcelBagModel()
                    {
                        BagNumber = x.BagNumber,
                        BagType = x.BagType,
                        ItemCount = x.Parcels.Count(),
                        Price = x.Parcels.Sum(s1 => s1.Price),
                        Weight = x.Parcels.Sum(s1 => s1.Weight),
                        Id = x.Id,
                        ShipmentId = x.ShipmentId,
                        Parcels = x.Parcels.Select(x1 => new ParcelDto()
                        {
                            Id = x1.Id,
                            BagId = x1.BagId,
                            ParcelNumber = x1.ParcelNumber,
                            DestinationCountry = x1.DestinationCountry,
                            RecipientName = x1.RecipientName,
                            Price = x1.Price,
                            Weight = x1.Weight
                        }).ToList()

                    }).Cast<BaseBag>()).ToList()
            };

            Shipment = k;


        }

        protected async void SendLetter(LetterBagModel bag)
        {
            bag.ItemCount++;
            var response = await ShipmentDataDataService.UpdateLetterBag(bag);
            HandleResponse(response);
            //NavigationManager.NavigateTo("/players");
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

    }
}
