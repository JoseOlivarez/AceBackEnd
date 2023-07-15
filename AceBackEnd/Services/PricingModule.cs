public class PricingService
{
    private const double CurrentPricePerGallon = 1.50;
    private const double CompanyProfitFactor = 0.10;

    public PricingService()
    {
        // empty for now. don't know if needed
    }
    public double CalculatePrice(string clientId, string location, int gallonsRequested)
    {
        double locationFactor = GetLocationFactor(location);
        double rateHistoryFactor = GetRateHistoryFactor(clientId);
        double gallonsRequestedFactor = GetGallonsRequestedFactor(gallonsRequested);

        double margin = CurrentPricePerGallon * (locationFactor - rateHistoryFactor + gallonsRequestedFactor + CompanyProfitFactor);

        double suggestedPricePerGallon = CurrentPricePerGallon + margin;
        double totalAmountDue = gallonsRequested * suggestedPricePerGallon;

        return totalAmountDue; // return suggestPricePerGallon too?
    }

    private double GetLocationFactor(string location)
    {
        return isTexas(location) ? 0.02 : 0.04;
    }

    private bool isTexas(string location) {
        string lowerInput = location.ToLower();
        
        return (lowerInput.Contains("texas") || lowerInput.Contains("tx"));
    }

    private double GetRateHistoryFactor(string clientId)
    {
        // TODO: query this from a database with maybe a hasHistory(clientId) method
        bool hasHistory = false;
        return hasHistory ? 0.01 : 0;
    }

    private double GetGallonsRequestedFactor(int gallonsRequested)
    {
        return gallonsRequested > 1000 ? 0.02 : 0.03;
    }
}
