using System.Collections.Generic;
using PostOffice.Domain;
using PostOffice.Domain.Entities;

namespace PostOffice.Application.Dtos
{
    public class BagDto
    {
        public int Id { get; set; }
        public int ShipmentId { get; set; }
        public string BagNumber { get; set; }
        public int ItemCount { get; set; }
        public decimal Weight { get; set; }
        public decimal Price { get; set; }
        public BagType BagType { get; set; }
        public List<ParcelDto> Parcels { get; set; }
    }
}
