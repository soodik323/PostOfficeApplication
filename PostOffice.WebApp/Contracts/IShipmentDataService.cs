using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PostOffice.Application.Dtos;
using PostOffice.WebApp.Models;
using PostOffice.WebApp.Services.Base;

namespace PostOffice.WebApp.Contracts
{
    public interface IShipmentDataService
    {
        Task<IEnumerable<ShipmentDto>> GetShipments();
        Task<ApiResponse<ShipmentDto>> CreateShipment(ShipmentModel shipment);
        Task<ApiResponse<ShipmentDto>> GetShipmentById(int id);
        Task<ApiResponse<BagDto>> CreateLetterBag(LetterBagModel bag);
        Task<ApiResponse<BagDto>> UpdateLetterBag(LetterBagModel bag);
        Task<ApiResponse<BagDto>> CreateParcelBag(ParcelBagModel bag);
        Task<ApiResponse<ParcelDto>> CreateParcel(ParcelModel parcel);
        Task<ApiResponse<ShipmentDto>> FinalizeShipment(ShipmentModel shipment);
        Task<ApiResponse<ModelValidateDto>> ValidateShipmentNumber(string number);


    }
}
