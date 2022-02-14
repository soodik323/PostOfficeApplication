using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PostOffice.Domain.Entities
{
    [Table("Shipment", Schema = "dbo")]
    public class Shipment
    {
        public int Id { get; set; }
        public string ShipmentNumber { get; set; }
        public Airport Airport { get; set; }
        public string FlightNumber { get; set; }
        public DateTime FlightDate { get; set; }
        public bool IsFinalized { get; set; }
        public virtual ICollection<Bag> Bags { get; set; }

    }
}
