using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PostOffice.Domain;
using PostOffice.WebApp.Helpers;

namespace PostOffice.WebApp.Models
{
    public class ShipmentModel
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(10)]
        [MinLength(10)]
        [RegularExpression(@"^[a-zA-Z0-9]{3}\-[a-zA-Z0-9]{6}$", ErrorMessage = "Format “XXX-XXXXXX”, where X – letter or digit")]
        public string ShipmentNumber { get; set; }
        public Airport Airport { get; set; }
        [Required]
        [MaxLength(6)]
        [MinLength(6)]
        [RegularExpression(@"^[a-zA-Z]{2}[0-9]{4}$", ErrorMessage = "Format “LLNNNN”, where L – letter, N – digit")]
        public string FlightNumber { get; set; }
        [DateMoreThanOrEqualToToday]
        public DateTime FlightDate { get; set; }
        public List<BaseBag> Bags { get; set; }
        public bool IsFinalized { get; set; }

    }
}
