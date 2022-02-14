using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FinanceReporter.Application.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PostOffice.Application.Contracts.Infrastructure;
using PostOffice.Application.Contracts.Persistence;
using PostOffice.Application.Dtos;
using PostOffice.Domain.Entities;

namespace PostOffice.Infrastructure
{
    public class ShipmentsDataService: IShipmentsDataService
    {
        private readonly IMapper _mapper;
        private readonly IPostOfficeEfUow _uow;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ShipmentsDataService(IMapper mapper, IPostOfficeEfUow uow, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _uow = uow;
            _httpContextAccessor = httpContextAccessor;

        }

        public List<ShipmentDto> GetShipments()
        {
            List<ShipmentDto> result;

            result = _uow.Shipments.AllChaining()
                .Include(p1 => p1.Bags)
                .ThenInclude(p2 => p2.Parcels).ToList()
                .Select(p => new ShipmentDto()
                {
                    Id = p.Id,
                    ShipmentNumber = p.ShipmentNumber,
                    Airport = p.Airport,
                    FlightNumber = p.FlightNumber,
                    FlightDate = p.FlightDate,
                    IsFinalized = p.IsFinalized,
                    Bags = p.Bags.Select(x => new BagDto()
                    {
                        BagNumber = x.BagNumber,
                        BagType = x.BagType,
                        ItemCount = x.ItemCount,
                        Price = x.Price,
                        Weight = x.Weight,
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

                    }).ToList()
                }).ToList();

            return result;

        }


        public async Task<ShipmentDto> CreateShipment(ShipmentDto shipment)
        {
           var s = _mapper.Map<Shipment>(shipment);

            await _uow.Shipments.AddAsync(s);
            await _uow.SaveChangesAsync();

            return _mapper.Map<ShipmentDto>(s);
        }
        public async Task<ShipmentDto> FinalizeShipment(ShipmentDto shipment, int id)
        {
            if (id != shipment.Id)
            {
                throw new BadRequestException("parameters do not match!");
            }

            var existingShipment = _uow.Shipments.SearchForChaining(x => x.Id == shipment.Id)
                .SingleOrDefault();

            if (existingShipment == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Shipment), id);
            }

            existingShipment.IsFinalized = shipment.IsFinalized;

            await _uow.SaveChangesAsync();

            return _mapper.Map<ShipmentDto>(existingShipment);
        }
        public async Task<BagDto> CreateBag(BagDto bag)
        {
            var s = _mapper.Map<Bag>(bag);

            await _uow.Bags.AddAsync(s);
            await _uow.SaveChangesAsync();

            return _mapper.Map<BagDto>(s);
        }
        public async Task<ParcelDto> CreateParcel(ParcelDto parcel)
        {
            var s = _mapper.Map<Parcel>(parcel);

            await _uow.Parcels.AddAsync(s);
            await _uow.SaveChangesAsync();

            return _mapper.Map<ParcelDto>(s);
        }
        public async Task<BagDto> UpdateBag(BagDto bag, int id)
        {
            if (id != bag.Id)
            {
                throw new BadRequestException("parameters do not match!");
            }

            var existingBag = _uow.Bags.SearchForChaining(x => x.Id == bag.Id)
                .SingleOrDefault();

            if (existingBag == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Bag), id);
            }

            existingBag.ItemCount = bag.ItemCount;
            
            await _uow.SaveChangesAsync();

            return _mapper.Map<BagDto>(existingBag);
        }

        public async Task<ModelValidateDto> ValidateShipmentNumber(string number)
        {
            var result = await _uow.Shipments.SearchForAsync(p => p.ShipmentNumber == number);

            var s1 = result.FirstOrDefault();

            var r = new ModelValidateDto
            {
                IsValidated = false
            };

            if (s1 == null)
            {
                r.IsValidated = true;
                return r;
            }

            return r;

        }
        public async Task<ShipmentDto> GetShipmentById(int id)
        {
            
            List<ShipmentDto> result;

            result = _uow.Shipments.SearchForChaining(p => p.Id == id)
                .Include(p1 => p1.Bags)
                .ThenInclude(p2 => p2.Parcels).ToList()
                .Select(p => new ShipmentDto()
                {
                    Id = p.Id,
                    ShipmentNumber = p.ShipmentNumber,
                    Airport = p.Airport,
                    FlightNumber = p.FlightNumber,
                    FlightDate = p.FlightDate,
                    IsFinalized = p.IsFinalized,
                    Bags = p.Bags.Select(x => new BagDto()
                    {
                        BagNumber = x.BagNumber,
                        BagType = x.BagType,
                        ItemCount = x.ItemCount,
                        Price = x.Price,
                        Weight = x.Weight,
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

                    }).ToList()
                }).ToList();


            if (result == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Shipment), id);
            }

            return result.First();
        }
    }
}
