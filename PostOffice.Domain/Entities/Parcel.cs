using System.ComponentModel.DataAnnotations.Schema;

namespace PostOffice.Domain.Entities
{
    [Table("Parcel", Schema = "dbo")]
    public class Parcel
    {
        public int Id { get; set; }
        public int BagId { get; set; }
        public string ParcelNumber { get; set; }
        public string RecipientName { get; set; }
        public string DestinationCountry { get; set; }
        public decimal Weight { get; set; }
        public decimal Price { get; set; }
        public virtual Bag Bag { get; set; }

    }
}
