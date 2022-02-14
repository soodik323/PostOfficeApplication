using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PostOffice.Application.Dtos;

namespace PostOffice.WebApp.Models
{
    public class ParcelBagModel:BaseBag
    {
        public int Id { get; set; }
        public int ShipmentId { get; set; }
        [Required]
        [MaxLength(15)]
        [RegularExpression("^[a-zA-Z0-9]{15}$", ErrorMessage = "Max length 15 characters, no special symbols allowed")]
        public string BagNumber { get; set; }
        public int ItemCount { get; set; }
        public decimal Weight { get; set; }
        public decimal Price { get; set; }
        public List<ParcelDto> Parcels { get; set; }

    }
}
