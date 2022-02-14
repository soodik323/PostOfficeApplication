using System.ComponentModel.DataAnnotations;

namespace PostOffice.WebApp.Models
{
    public class ParcelModel
    {
        public int Id { get; set; }
        public int BagId { get; set; }
        [Required]
        [MaxLength(10)]
        [MinLength(10)]
        [RegularExpression("^[a-zA-Z]{2}[0-9]{6}[a-zA-Z]{2}$", ErrorMessage = "Format “LLNNNNNNLL”, where L – letter, N – digit")]
        public string ParcelNumber { get; set; }
        [MaxLength(100)]
        [Required]
        public string RecipientName { get; set; }
        [RegularExpression(@"\b(EE|LV|FI)\b", ErrorMessage = "2-letters code, e.g. “EE”, “LV”, “FI”")]
        [Required]
        public string DestinationCountry { get; set; }
        [RegularExpression(@"^(\d+)\.(\d{1,3})$", ErrorMessage = "Max 3 decimals allowed after comma ")]
        [Required]
        public decimal Weight { get; set; }
        [RegularExpression(@"^(\d+)\.(\d{1,2})$", ErrorMessage = "Max 2 decimals allowed after comma ")]
        [Required]
        public decimal Price { get; set; }

    }
}
