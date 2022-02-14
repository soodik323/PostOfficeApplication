using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PostOffice.Domain.Entities
{
    [Table("Bag", Schema = "dbo")]
    public class Bag
    {
        public int Id { get; set; }
        public int ShipmentId { get; set; }
        public string BagNumber { get; set; }
        public int ItemCount { get; set; }
        public decimal Weight { get; set; }
        public decimal Price { get; set; }
        public BagType BagType { get; set; }
        public virtual Shipment Shipment { get; set; }
        public virtual ICollection<Parcel> Parcels { get; set; }
    }
}
