using PostOffice.Domain.Entities;

namespace PostOffice.Application.Dtos
{
    public class ParcelDto
    {
        public int Id { get; set; }
        public int BagId { get; set; }
        public string ParcelNumber { get; set; }
        public string RecipientName { get; set; }
        public string DestinationCountry { get; set; }
        public decimal Weight { get; set; }
        public decimal Price { get; set; }

    }
}
