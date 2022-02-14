using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PostOffice.Application.Dtos;
using PostOffice.Domain;
using PostOffice.WebApp.Contracts;
using PostOffice.WebApp.Models;
using PostOffice.WebApp.Services.Base;

namespace PostOffice.WebApp.Services
{
    public class ShipmentDataService : IShipmentDataService
    {
        private readonly HttpClient _httpClient;

        public ShipmentDataService(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }
        public async Task<IEnumerable<ShipmentDto>> GetShipments()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<ShipmentDto>>("api/shipment/getAll");
        }
        public async Task<ApiResponse<ModelValidateDto>> ValidateShipmentNumber(string number)
        {
            var result = await Get<ModelValidateDto>($"api/shipment/ValidateShipmentNumber/{number}");
            return result;
        }
        public async Task<ApiResponse<ShipmentDto>> CreateShipment(ShipmentModel shipment)
        {
            var dto = new ShipmentDto()
            {
                Id = shipment.Id,
                FlightNumber = shipment.FlightNumber,
                Airport = shipment.Airport,
                FlightDate = shipment.FlightDate,
                ShipmentNumber = shipment.ShipmentNumber,
                IsFinalized = shipment.IsFinalized
            };

            var result = await Post<ShipmentDto>("api/shipment/create", dto);
            return result;
        }
        public async Task<ApiResponse<ShipmentDto>> FinalizeShipment(ShipmentModel shipment)
        {
            var dto = new ShipmentDto()
            {
                Id = shipment.Id,
                IsFinalized = shipment.IsFinalized,
                Airport = shipment.Airport,
                FlightDate = shipment.FlightDate,
                FlightNumber = shipment.FlightNumber,
                ShipmentNumber = shipment.ShipmentNumber
                
            };

            var result = await Put<ShipmentDto>($"api/shipment/FinalizeShipment/{dto.Id}", dto);
            return result;
        }
        public async Task<ApiResponse<ParcelDto>> CreateParcel(ParcelModel parcel)
        {
            var dto = new ParcelDto()
            {
                BagId = parcel.BagId,
                ParcelNumber = parcel.ParcelNumber,
                RecipientName = parcel.RecipientName,
                DestinationCountry = parcel.DestinationCountry,
                Price = parcel.Price,
                Weight = parcel.Weight
            };

            var result = await Post<ParcelDto>("api/parcel/create", dto);
            return result;
        }
        public async Task<ApiResponse<BagDto>> CreateLetterBag(LetterBagModel bag)
        {
            var dto = new BagDto()
            {
               
                BagType = bag.BagType,
                BagNumber = bag.BagNumber,
                ItemCount = bag.ItemCount,
                ShipmentId = bag.ShipmentId,
                Price = bag.Price,
                Weight = bag.Weight
            };

            var result = await Post<BagDto>("api/bag/create", dto);
            return result;
        }
        public async Task<ApiResponse<BagDto>> CreateParcelBag(ParcelBagModel bag)
        {
            var dto = new BagDto()
            {

                BagType = bag.BagType,
                BagNumber = bag.BagNumber,
                ItemCount = bag.ItemCount,
                ShipmentId = bag.ShipmentId

            };

            var result = await Post<BagDto>("api/bag/create", dto);
            return result;
        }
        public async Task<ApiResponse<BagDto>> UpdateLetterBag(LetterBagModel bag)
        {
            var dto = new BagDto()
            {
                Id = bag.Id,
                BagType = bag.BagType,
                BagNumber = bag.BagNumber,
                ItemCount = bag.ItemCount,
                ShipmentId = bag.ShipmentId,
                Price = bag.Price,
                Weight = bag.Weight
            };

            var result = await Put<BagDto>($"api/bag/update/{dto.Id}", dto);
            return result;
        }
        public async Task<ApiResponse<ShipmentDto>> GetShipmentById(int id)
        {
            var result = await Get<ShipmentDto>($"api/shipment/GetShipmentById/{id}");
            return result;
        }

        public async Task<ApiResponse<T>> Post<T>(string resourceUrl, T data)
        {
            ApiResponse<T> apiResponse = new ApiResponse<T>();
            var jsonContent = Newtonsoft.Json.JsonConvert.SerializeObject(data);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(resourceUrl, httpContent);
            if (!response.IsSuccessStatusCode)
            {
                var errorResult = await response.Content.ReadAsStringAsync();
                apiResponse.Success = false;
                apiResponse.Message = "Failed";

            }
            else
            {
                apiResponse.Success = true;
                apiResponse.Message = "Completed";
                var contentResult = await response.Content.ReadAsStringAsync();
                apiResponse.Result = JsonConvert.DeserializeObject<T>(contentResult);
            }

            return apiResponse;
        }
        public async Task<ApiResponse<T>> Put<T>(string resourceUrl, T data)
        {
            ApiResponse<T> apiResponse = new ApiResponse<T>();
            var jsonContent = Newtonsoft.Json.JsonConvert.SerializeObject(data);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync(resourceUrl, httpContent);
            if (!response.IsSuccessStatusCode)
            {
                var errorResult = await response.Content.ReadAsStringAsync();
                apiResponse.Success = false;
                apiResponse.Message = "Failed";

            }
            else
            {
                apiResponse.Success = true;
                apiResponse.Message = "Completed";
                var contentResult = await response.Content.ReadAsStringAsync();
                apiResponse.Result = JsonConvert.DeserializeObject<T>(contentResult);
            }

            return apiResponse;
        }

        public async Task<ApiResponse<T>> Get<T>(string resourceUrl) where T : class
        {
            ApiResponse<T> apiResponse = new ApiResponse<T>();

            var response = await _httpClient.GetAsync($"{resourceUrl}");

        
                if (!response.IsSuccessStatusCode)
                {
                    var errorResult = await response.Content.ReadAsStringAsync();
                    apiResponse.Success = false;
                    apiResponse.Message = "Failed";

                }
                else
                {
                    apiResponse.Success = true;
                    apiResponse.Message = "Completed";
                    var contentResult = await response.Content.ReadAsStringAsync();
                    apiResponse.Result = JsonConvert.DeserializeObject<T>(contentResult);
                }

            return apiResponse;
        }

    }
}
