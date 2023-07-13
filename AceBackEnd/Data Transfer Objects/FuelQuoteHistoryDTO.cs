namespace AceBackEnd.Controllers
{
    public class FuelQuoteHistoryDto
    {
        public int Id { get; set; }
        public int GallonsRequested { get; set; }
        public string? DeliveryAddress { get; set; }
        public DateTime DeliveryDate { get; set; }
        public decimal SuggestedPrice { get; set; }
        public decimal TotalAmountDue { get; set; }
    }

}