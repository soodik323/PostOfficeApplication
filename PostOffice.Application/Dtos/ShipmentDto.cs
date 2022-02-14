using System;
using System.Collections.Generic;
using PostOffice.Domain;

namespace PostOffice.Application.Dtos
{
    public class ShipmentDto
    {
        public int Id { get; set; }
        public string ShipmentNumber { get; set; }
        public Airport Airport { get; set; }
        public string FlightNumber { get; set; }
        public DateTime FlightDate { get; set; }
        public List<BagDto> Bags { get; set; }
        public bool IsFinalized { get; set; }

    }
}
