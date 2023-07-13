using AceBackEnd.Data_Transfer_Objects;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.Runtime.InteropServices.ObjectiveC;
using System.Runtime.Serialization;

namespace AceBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuelQuoteHistoryController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<FuelQuoteHistoryDto>> GetFuelQuoteHistory()
        {
            try
            {
                var FuelQuoteHistoryDtos = new List<FuelQuoteHistoryDto>();

                for (int i = 1; i <= 15; i++)
                {
                    FuelQuoteHistoryDtos.Add(new FuelQuoteHistoryDto
                    {
                        Id = i,
                        GallonsRequested = 150 + i,
                        DeliveryAddress = $"123 Main St, Anywhere, USA {i}",
                        DeliveryDate = new DateTime(2023, 7, 1).AddDays(i),
                        SuggestedPrice = 2.50m,
                        TotalAmountDue = (150 + i) * 2.50m
                    });
                }

                return Ok(FuelQuoteHistoryDtos);
            }
            catch (Exception)
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }
        }
    }
}