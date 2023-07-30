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
                var fuelQuoteHistoryDTOs = await (from fq in _dbContext.FuelQuoteHistories
                                                  where fq.ClientId == currentUserId
                                                  select new FuelQuoteHistoryDTO
                                                  {
                                                      Id = fq.Id,
                                                      GallonsRequested = fq.GallonsRequested,
                                                      DeliveryAddress = fq.DeliveryAddress,
                                                      DeliveryDate = fq.DeliveryDate,
                                                      SuggestedPrice = fq.SuggestedPrice,
                                                      TotalAmountDue = fq.TotalAmountDue,
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

    }
}
