using AutoMapper;
using PostOffice.Application.Dtos;
using PostOffice.Domain.Entities;

namespace PostOffice.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Shipment, ShipmentDto>().ReverseMap();
            CreateMap<Bag, BagDto>().ReverseMap();
            CreateMap<Parcel, ParcelDto>().ReverseMap();

        }
    }
}
