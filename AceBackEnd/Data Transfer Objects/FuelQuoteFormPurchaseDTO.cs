namespace AceBackEnd.Data_Transfer_Objects
{
    public class FuelQuoteFormPurchaseDTO
    {
        public int gallonsRequested { get; set; }
        public int dateYear { get; set; }
        public int dateMonth { get; set; }
        public int dateDay { get; set; }
        public double pricePerGallon { get; set; }
        public string deliveryAddress { get; set; }
        public double fuelQuoteTotal { get; set; }
        public double amount { get; set; }
        public int clientId { get; set; }
    }
}
