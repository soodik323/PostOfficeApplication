using PostOffice.Application.Dtos;
using PostOffice.Domain.Entities;

namespace PostOffice.Application.Contracts.Persistence
{
    public interface IPostOfficeEfUow : IUnitOfWork
    {
        IRepository<Shipment> Shipments { get; }
        IRepository<Bag> Bags { get; }
        IRepository<Parcel> Parcels { get; }
       

    }
}
