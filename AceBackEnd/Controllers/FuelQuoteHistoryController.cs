using AceBackEnd.Data_Transfer_Objects;
using AceBackEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace AceBackEnd.Data_Transfer_Objects
{
    public class FuelQuoteRequestDTO
    {
        public int ClientId { get; set; }
        public string Location { get; set; }
        public int GallonsRequested { get; set; }
    }
}

namespace AceBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuelQuoteHistoryController : ControllerBase
    {
        private readonly AceDbContext _dbContext;
        private readonly PricingService _pricingService;
        public FuelQuoteHistoryController(AceDbContext dbContext,PricingService pricingService)
        {
            _dbContext = dbContext;
            _pricingService = pricingService;
        }

        [HttpGet("{currentUserId}")]
        public async Task<ActionResult<IEnumerable<FuelQuoteHistoryDTO>>> GetFuelQuoteHistory(int currentUserId)
        {
            try
            {
                var fuelQuoteHistoryDTOs = await (from fq in _dbContext.FuelQuoteForms
                                                  where fq.ClientId == currentUserId
                                                  select new FuelQuoteHistoryDTO
                                                  {
                                                      Id = fq.Id,
                                                      GallonsRequested = fq.GallonsRequested,
                                                      DeliveryAddress = fq.DeliveryAddress,
                                                      DeliveryDate = fq.DeliveryDate,
                                                      SuggestedPrice = fq.PricePerGallon,
                                                      TotalAmountDue = fq.Amount,
                                                      ClientId = fq.ClientId
                                                  }).ToListAsync();
                return Ok(fuelQuoteHistoryDTOs);
            }

            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("CalculateFuelQuote")]
        public async Task<ActionResult> CalculateFuelQuote([FromQuery] FuelQuoteRequestDTO request)
        {
            if(request.ClientId == null || request.Location == null || request.GallonsRequested <= 0)
            {
                return BadRequest("Invalid input parameters.");
            }

            try
            {
                var prices = await _pricingService.CalculatePrice(request.ClientId, request.Location, request.GallonsRequested);

                var result = new 
                {
                    totalAmount = prices[0],
                    suggestedPrice = prices[1]
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("NewHistory")]
        public IActionResult NewHistory([FromBody] FuelQuoteFormPurchaseDTO dtoObject) {

            
            try {

                decimal amount = (decimal) (dtoObject.amount);
                decimal pricePerGallon = (decimal) (dtoObject.pricePerGallon);

                // Retrieve the latest PurchaseId from the FuelQuoteForm table (assumes Id is an auto-incrementing primary key)
                int? latestHistoryId = _dbContext.FuelQuoteHistories.OrderByDescending(h => h.Id).Select(h => h.Id).FirstOrDefault();

                // Increment the latestPurchaseId by one to get the new PurchaseId for the new purchase
                int nextHistoryId = (latestHistoryId.HasValue ? latestHistoryId.Value : 0) + 1;
                
                FuelQuoteHistory myFuelQuoteHistory = new FuelQuoteHistory {
                    Id = nextHistoryId,
                    GallonsRequested = dtoObject.gallonsRequested,
                    DeliveryAddress = dtoObject.deliveryAddress.ToString(),
                    DeliveryDate = new DateTime(dtoObject.dateYear, dtoObject.dateMonth, dtoObject.dateDay),
                    SuggestedPrice = pricePerGallon,
                    TotalAmountDue = amount,
                    ClientId = dtoObject.clientId
                };

                
                _dbContext.FuelQuoteHistories.Add(myFuelQuoteHistory);
                _dbContext.SaveChanges();

                return Ok(myFuelQuoteHistory);
            }
            catch (Exception)
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }
            
        }

    }
}
