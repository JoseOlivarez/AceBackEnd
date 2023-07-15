namespace AceBackEnd.Data_Transfer_Objects
{
    public class FuelQuoteFormDTO
    {
        public int gallonsRequested { get; set; }
        public DateOnly deliveryDate { get; set; }
        public double pricePerGallon { get; set; }
        public string deliveryAddress { get; set; }
        public double fuelQuoteTotal { get; set; }
        public double amount { get; set; }

    }
}
