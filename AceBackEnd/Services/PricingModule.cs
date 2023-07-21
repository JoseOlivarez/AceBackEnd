
using Microsoft.EntityFrameworkCore;

namespace AceBackEnd.Models
{
    public class PricingService
    {

        private readonly AceDbContext _dbContext;

        public PricingService(AceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private const double CurrentPricePerGallon = 1.50;
        private const double CompanyProfitFactor = 0.10;

        public async Task<double[]> CalculatePrice(int clientId, string location, int gallonsRequested)
        {
            double locationFactor = GetLocationFactor(location);
            double rateHistoryFactor = await GetRateHistoryFactor(clientId);
            double gallonsRequestedFactor = GetGallonsRequestedFactor(gallonsRequested);

            double margin = CurrentPricePerGallon * (locationFactor - rateHistoryFactor + gallonsRequestedFactor + CompanyProfitFactor);

            double suggestedPricePerGallon = CurrentPricePerGallon + margin;
            double totalAmountDue = gallonsRequested * suggestedPricePerGallon;
            double[] result = new double[2];
            result[0] = suggestedPricePerGallon;
            result[1] = totalAmountDue;

            return result;
        }

        private double GetLocationFactor(string location)
        {
            return isTexas(location) ? 0.02 : 0.04;
        }

        private bool isTexas(string location)
        {
            string lowerInput = location.ToLower();

            return (lowerInput.Contains("texas") || lowerInput.Contains("tx"));
        }

        private async Task<double> GetRateHistoryFactor(int currentUserId)
        {
            return await UserHasHistory(currentUserId) ? 0.01 : 0;
        }

        private double GetGallonsRequestedFactor(int gallonsRequested)
        {
            return gallonsRequested > 1000 ? 0.02 : 0.03;
        }

        public async Task<bool> UserHasHistory(int currentUserId)
        {
            try
            {
                var hasHistory = await _dbContext.FuelQuoteHistories
                    .AnyAsync(fq => fq.ClientId == currentUserId);

                return hasHistory;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}