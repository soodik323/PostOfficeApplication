using System.ComponentModel.DataAnnotations;

namespace PostOffice.WebApp.Models
{
    
    public class LetterBagModel:BaseBag
    {
        public int Id { get; set; }
        public int ShipmentId { get; set; }
        [Required]
        [MaxLength(15)]
        [RegularExpression("^[a-zA-Z0-9]{15}$", ErrorMessage = "Max length 15 characters, no special symbols allowed")]
        public string BagNumber { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than 0")]
        public int ItemCount{ get; set; }
        [RegularExpression(@"^(\d+)\.(\d{1,3})$", ErrorMessage = "Max 3 decimals allowed after comma ")]
        [Required]
        public decimal Weight { get; set; }
        [RegularExpression(@"^(\d+)\.(\d{1,2})$", ErrorMessage = "Max 2 decimals allowed after comma ")]
        [Required]
        public decimal Price { get; set; }

    }
}
