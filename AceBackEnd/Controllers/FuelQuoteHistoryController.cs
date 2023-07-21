using AceBackEnd.Data_Transfer_Objects;
using AceBackEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AceBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuelQuoteHistoryController : ControllerBase
    {
        private readonly AceDbContext _dbContext;

        public FuelQuoteHistoryController(AceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FuelQuoteHistoryDTO>>> GetFuelQuoteHistory()
        {
            try
            {
                var fuelQuoteHistoryDTOs = await (from fq in _dbContext.FuelQuoteHistories
                                                   select new FuelQuoteHistoryDTO
                                                   {
                                                       Id = fq.Id,
                                                       GallonsRequested = fq.GallonsRequested,
                                                       DeliveryAddress = fq.DeliveryAddress,
                                                       DeliveryDate = fq.DeliveryDate,
                                                       SuggestedPrice = fq.SuggestedPrice,
                                                       TotalAmountDue = fq.TotalAmountDue
                                                   }).ToListAsync();

                return Ok(fuelQuoteHistoryDTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
