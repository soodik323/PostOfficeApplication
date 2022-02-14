using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PostOffice.Application.Dtos;

namespace PostOffice.Application.Contracts.Infrastructure
{
    public interface IShipmentsDataService
    {
        Task<ShipmentDto> CreateShipment(ShipmentDto shipment);
        Task<ShipmentDto> GetShipmentById(int id);
        Task<BagDto> CreateBag(BagDto bag);
        Task<BagDto> UpdateBag(BagDto bag, int id);
        Task<ParcelDto> CreateParcel(ParcelDto parcel);
        Task<ShipmentDto> FinalizeShipment(ShipmentDto shipment, int id);
        List<ShipmentDto> GetShipments();
        Task<ModelValidateDto> ValidateShipmentNumber(string number);
    }
}
